using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxObject : MonoBehaviour
{
    #region Variables
    [Tooltip(
        "The Parallax effect value defines how quickly the background moves relative to the camera:\n" +
        "-1: Background moves in the opposite direction of the camera.\n" +
        " 0: Background moves at the same speed as the camera.\n" +
        "+1: Background moves in sync with the camera."
    )]
    [Range(-1,1)]
    [SerializeField]
    private float _parallaxEffect;

    public float ParallaxEffect
    {
        get => _parallaxEffect;
    }

    private Vector2 _startPosition;

    public Vector2 StartPosition
    {
        get => _startPosition;
    }

    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        _startPosition = transform.position;
    }
}
