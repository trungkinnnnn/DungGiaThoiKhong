using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHoldY : MonoBehaviour
{
    [SerializeField] Transform _pointCameraTarget;
    private float _originalY = 2.602764f;
    void Start()
    {
        //_originalY = _pointCameraTarget.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        _pointCameraTarget.position = new Vector2(_pointCameraTarget.position.x, _originalY);
    }
}
