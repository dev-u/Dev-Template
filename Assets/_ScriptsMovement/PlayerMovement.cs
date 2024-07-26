/*
    Script levemente modificado baseado no script original do canal @DawnosaurDev no youtube : www.youtube.com/@DawnosaurDev

    O script original foi modificado para se adaptar com o Novo Input System da Unity.

    Se tiver mais interesse em entender sobre o desenvolvimento do script, por favor assista aos vídeos do canal @DawnosaurDev no youtube.
*/

using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerData Data;

    #region Variables

    [SerializeField] private Rigidbody2D _rb;
    private Inputs _inputs;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private Vector2 _moveInput;

    public bool IsFacingRight { get; private set; }
    public bool IsJumping { get; private set; }
    public bool IsWallJumping { get; private set; }
    public bool IsSliding { get; private set; }

    //Timers
    public float LastOnGroundTime { get; private set; }

    public float LastOnWallTime { get; private set; }
    public float LastOnWallRightTime { get; private set; }
    public float LastOnWallLeftTime { get; private set; }

    //Jump
    private bool _isJumpCut;

    private bool _isJumpFalling;

    //Wall Jump
    private float _wallJumpStartTime;

    private int _lastWallJumpDirection;

    public float LastPressedJumpTime { get; private set; }

    [Header("Checks")]
    [SerializeField] private Transform _groundCheckPoint;

    [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.49f, 0.03f);

    [Space(5)]
    [SerializeField] private Transform _frontWallCheckPoint;

    [SerializeField] private Transform _backWallCheckPoint;
    [SerializeField] private Vector2 _wallCheckSize = new Vector2(0.5f, 1f);

    [Header("Layers & Tags")]
    [SerializeField] private LayerMask _groundLayer;

    #endregion Variables

    private void Awake()
    {
        _inputs = new Inputs();
        _inputs.Enable();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        SetGravityScale(Data.gravityScale);
        IsFacingRight = true;
    }

    private void OnEnable()
    {
        _moveAction = _inputs.PlayerInput.Movement;
        _jumpAction = _inputs.PlayerInput.Jump;

        _moveAction.performed += OnMove;
        _moveAction.canceled += OnMove;

        _jumpAction.performed += OnJump;
        _jumpAction.canceled += OnJumpUp;
    }

    private void OnDisable()
    {
        _moveAction.performed -= OnMove;
        _moveAction.canceled -= OnMove;
    }

    #region Input Callbacks

    public void OnMove(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        Debug.Log("Jump Pressed");

        LastPressedJumpTime = Data.jumpInputBufferTime;
        Debug.LogWarning(LastPressedJumpTime);
        Debug.LogWarning("Buffer " + Data.jumpInputBufferTime);
    }

    public void OnJumpUp(InputAction.CallbackContext ctx)
    {
        if (CanJumpCut() || CanWallJumpCut())
            _isJumpCut = true;
    }

    #endregion Input Callbacks

    // Update is called once per frame
    private void Update()
    {
        #region TIMERS

        LastOnGroundTime -= Time.deltaTime;
        LastOnWallTime -= Time.deltaTime;
        LastOnWallRightTime -= Time.deltaTime;
        LastOnWallLeftTime -= Time.deltaTime;

        LastPressedJumpTime -= Time.deltaTime;

        #endregion TIMERS

        if (_moveInput.x != 0)
        {
            CheckDirectionToFace(_moveInput.x > 0);
        }

        #region COLLISION CHECKS

        if (!IsJumping)
        {
            if (Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer) && !IsJumping)
            {
                Debug.Log("On Ground");
                LastOnGroundTime = Data.coyoteTime;
            }

            //Right Wall Check
            if (((Physics2D.OverlapBox(_frontWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && IsFacingRight)
                                        || (Physics2D.OverlapBox(_backWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && !IsFacingRight)) && !IsWallJumping)
                LastOnWallRightTime = Data.coyoteTime;
            //Left Wall Check
            if (((Physics2D.OverlapBox(_frontWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && !IsFacingRight)
                                                       || (Physics2D.OverlapBox(_backWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && IsFacingRight)) && !IsWallJumping)
                LastOnWallLeftTime = Data.coyoteTime;

            LastOnWallTime = Mathf.Max(LastOnWallLeftTime, LastOnWallRightTime);
        }

        #endregion COLLISION CHECKS

        #region JUMP CHECKS

        if (IsJumping && _rb.velocity.y < 0)
        {
            IsJumping = false;
            if (!IsWallJumping)
                _isJumpFalling = true;
        }

        if (IsWallJumping && Time.time - _wallJumpStartTime > Data.wallJumpTime)
        {
            IsWallJumping = false;
        }

        if (LastOnGroundTime > 0 && !IsJumping && !IsWallJumping)
        {
            _isJumpCut = false;

            if (!IsJumping)
                _isJumpFalling = false;
        }

        // Jump
        if (CanJump() && LastPressedJumpTime > 0)
        {
            Debug.Log("Can Jump");
            IsJumping = true;
            IsWallJumping = false;
            _isJumpCut = false;
            _isJumpFalling = false;
            Jump();
        }
        // Wall Jump
        else if (CanWallJump() && LastPressedJumpTime > 0)
        {
            IsWallJumping = true;
            IsJumping = false;
            _isJumpCut = false;
            _isJumpFalling = false;
            _wallJumpStartTime = Time.time;
            _lastWallJumpDirection = (LastOnWallRightTime > 0) ? -1 : 1;

            WallJump(_lastWallJumpDirection);
        }

        #endregion JUMP CHECKS

        #region SLIDE CHECKS

        if (CanSlide() && ((LastOnWallLeftTime > 0 && _moveInput.x < 0) || (LastOnWallRightTime > 0 && _moveInput.x > 0)))
        {
            IsSliding = true;
        }
        else
        {
            IsSliding = false;
        }

        #endregion SLIDE CHECKS

        #region GRAVITY

        // Higher gravity if release the jump input or is falling
        if (IsSliding)
        {
            SetGravityScale(0);
        }
        else if (_rb.velocity.y < 0 && _moveInput.y < 0)
        {
            SetGravityScale(Data.gravityScale * Data.fastFallGravityMult);

            _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Max(_rb.velocity.y, -Data.maxFastfallspeed));
        }
        else if (_isJumpCut)
        {
            SetGravityScale(Data.gravityScale * Data.jumpCutGravityMult);
            _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Max(_rb.velocity.y, -Data.maxfallspeed));
        }
        else if ((IsJumping || IsWallJumping || _isJumpFalling) && Mathf.Abs(_rb.velocity.y) < Data.jumpHangTimeThreshold)
        {
            SetGravityScale(Data.gravityScale * Data.jumpHangGravityMult);
        }
        else if (_rb.velocity.y < 0)
        {
            // Higher gravity if falling
            SetGravityScale(Data.gravityScale * Data.fallGravityMult);
            // Caps maximun fail speed, so when falling over large distances we don't accelerat to insanely speeds
            _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Max(_rb.velocity.y, -Data.maxfallspeed));
        }
        else
        {
            // Default gravity if stangin on a platform or moving upwards
            SetGravityScale(Data.gravityScale);
        }

        #endregion GRAVITY
    }

    private void FixedUpdate()
    {
        if (IsWallJumping)
            Move(Data.wallJumpRunLerp);
        else
            Move(1);

        if (IsSliding)
            Slide();
    }

    public void SetGravityScale(float scale)
    {
        _rb.gravityScale = scale;
    }

    #region Move Methods

    private void Move(float lerpAmount)
    {
        float targetSpeed = _moveInput.x * Data.moveMaxSpeed;
        targetSpeed = Mathf.Lerp(_rb.velocity.x, targetSpeed, lerpAmount);

        float accelRate;

        if (LastOnGroundTime > 0)
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.moveAccelAmount : Data.moveDeccelAmount;
        else
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.moveAccelAmount * Data.accelInAir : Data.moveDeccelAmount * Data.deccelInAir;

        //Add Bonus Jump Apex Acceleration
        if ((IsJumping || IsWallJumping || _isJumpFalling) && Mathf.Abs(_rb.velocity.y) < Data.jumpHangTimeThreshold)
        {
            accelRate *= Data.jumpHangAccelMult;
            targetSpeed *= Data.jumpHangMaxSpeedMult;
        }

        //Conserve Momentum
        if (Data.doConserveMomentum && Mathf.Abs(_rb.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(_rb.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && LastOnGroundTime < 0)
            accelRate = 0;

        float speedDiff = targetSpeed - _rb.velocity.x;
        float movement = speedDiff * accelRate;

        _rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
    }

    public void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != IsFacingRight)
            Turn();
    }

    private void Turn()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        IsFacingRight = !IsFacingRight;
    }

    #endregion Move Methods

    #region Jump Methods

    private void Jump()
    {
        LastPressedJumpTime = 0;
        LastOnGroundTime = 0;
        float force = Data.jumpForce;
        Debug.Log(Data.jumpForce);
        Debug.Log("Jumping");
        if (_rb.velocity.y < 0)
        {
            force -= _rb.velocity.y;
        }
        _rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }

    private void WallJump(int dir)
    {
        LastPressedJumpTime = 0;
        LastOnGroundTime = 0;
        LastOnWallRightTime = 0;

        Vector2 force = new Vector2(Data.wallJumpForce.x, Data.wallJumpForce.y);
        force.x *= dir;

        if (Mathf.Sign(_rb.velocity.x) != Mathf.Sign(force.x))
        {
            force.x += _rb.velocity.x;
        }
        if (_rb.velocity.y < 0)
        {
            force.y -= _rb.velocity.y;
        }
        _rb.AddForce(force, ForceMode2D.Impulse);
    }

    #endregion Jump Methods

    #region Slide Methods

    private void Slide()
    {
        if (_rb.velocity.y > 0)
        {
            _rb.AddForce(-_rb.velocity * Vector2.up, ForceMode2D.Impulse);
        }
        float speedDif = Data.slideSpeed - _rb.velocity.y;
        float movement = speedDif * Data.slideAccel;

        movement = Mathf.Clamp(movement, -Mathf.Abs(speedDif) * (1 / Time.fixedDeltaTime), Mathf.Abs(speedDif) * (1 / Time.fixedDeltaTime));
        _rb.AddForce(movement * Vector2.up);
    }

    #endregion Slide Methods

    #region Check Methods

    public bool CanJump()
    {
        return LastOnGroundTime > 0 && !IsJumping;
    }

    public bool CanWallJump()
    {
        return LastPressedJumpTime > 0 && LastOnWallTime > 0 && LastOnGroundTime <= 0;
    }

    public bool CanJumpCut()
    {
        return IsJumping && _rb.velocity.y > 0;
    }

    public bool CanWallJumpCut()
    {
        return IsWallJumping && _rb.velocity.y > 0;
    }

    public bool CanSlide()
    {
        return LastOnWallTime > 0 && !IsJumping && LastOnGroundTime <= 0;
    }

    #endregion Check Methods

    #region EDITOR METHODS

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_groundCheckPoint.position, _groundCheckSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_frontWallCheckPoint.position, _wallCheckSize);
        Gizmos.DrawWireCube(_backWallCheckPoint.position, _wallCheckSize);
    }

    #endregion EDITOR METHODS
}