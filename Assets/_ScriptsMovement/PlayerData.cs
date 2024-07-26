using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Gravity")]
    [HideInInspector] public float gravityStrength;

    [HideInInspector] public float gravityScale;

    [Space(5)]
    public float fallGravityMult;

    public float maxfallspeed;

    [Space(5)]
    public float fastFallGravityMult;

    public float maxFastfallspeed;

    [Space(20)]
    [Header("Move")]
    public float moveMaxSpeed;

    public float moveAccel;
    [HideInInspector] public float moveAccelAmount;
    public float moveDeccel;
    [HideInInspector] public float moveDeccelAmount;

    [Space(5)]
    [Range(0f, 1f)] public float accelInAir;

    [Range(0f, 1f)] public float deccelInAir;

    [Space(5)]
    public bool doConserveMomentum = true;

    [Space(20)]
    [Header("Jump")]
    public float jumpHeight;

    public float jumpTimeToApex;
    [HideInInspector] public float jumpForce;

    [Header("Both Jumps")]
    public float jumpCutGravityMult;

    [Range(0f, 1)] public float jumpHangGravityMult;
    public float jumpHangTimeThreshold;

    [Space(0.5f)]
    public float jumpHangAccelMult;

    public float jumpHangMaxSpeedMult;

    [Header("Wall Jump")]
    public Vector2 wallJumpForce;

    [Space(5)]
    [Range(0f, 1f)] public float wallJumpRunLerp;

    [Range(0f, 1.5f)] public float wallJumpTime;
    public bool doTurnOnWallJump;

    [Space(20)]
    [Header("Slide")]
    public float slideSpeed;

    public float slideAccel;

    [Header("Assists")]
    [Range(0.01f, 0.5f)] public float coyoteTime;

    [Range(0.01f, 0.5f)] public float jumpInputBufferTime;

    // Unity Callback,called when the inspector updates
    private void OnValidate()
    {
        gravityStrength = -(2 * jumpHeight) / (jumpTimeToApex * jumpTimeToApex);

        gravityScale = gravityStrength / Physics2D.gravity.y;

        // Calculate move acceleration & decceleration forces using formula : amount = ((1 / Time.fixedDeltaTime) * acceleration) / moveMaxSpeed
        moveAccelAmount = (50 * moveAccel) / moveMaxSpeed;
        moveDeccelAmount = (50 * moveDeccel) / moveMaxSpeed;

        jumpForce = Mathf.Abs(gravityStrength) * jumpTimeToApex;

        #region Variable Ranges

        moveAccel = Mathf.Clamp(moveAccel, 0.01f, moveMaxSpeed);
        moveDeccel = Mathf.Clamp(moveDeccel, 0.01f, moveMaxSpeed);

        #endregion Variable Ranges
    }
}