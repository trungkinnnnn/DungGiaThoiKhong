using System.Collections;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    [SerializeField] float _speedRocket;
    [SerializeField] float _rotateSpeedFar = 180f;
    [SerializeField] float _rotateSpeedClose = 180f;
    [SerializeField] float _timeStart = 2f;
    [SerializeField] float _distanceMax = 10f;
    private Rigidbody2D _rb;
    private Collider2D _collider;
    private PlayerSingle _player;
    private bool _isHoming = false;
    private bool _isDestroy = false;
    private float _currentSpeedRotate;

    private void OnEnable()
    {
        transform.rotation = Quaternion.Euler(0, 0, 90f);
        _currentSpeedRotate = _rotateSpeedFar;
        _isDestroy = false ;
        if (_collider != null) StartCoroutine(OffColliderWithTime());
    }

    private IEnumerator OffColliderWithTime()
    {
        _collider.enabled = false;
        yield return new WaitForSeconds(1f);
        _collider.enabled = true;
    }    

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = PlayerSingle.Instance;
        _collider = GetComponentInChildren<Collider2D>();
        StartCoroutine(OffColliderWithTime());
        StartCoroutine(GoStraightWithTime(3f));
    }

    private IEnumerator GoStraightWithTime(float time)
    {
        _rb.velocity = transform.right * _speedRocket;
        yield return new WaitForSeconds(_timeStart);
        _isHoming = true;
    }

    private void Update()
    {
        if (!_isHoming && _isDestroy) return;
        CheckDistanceToPlayer();
    }

    private void CheckDistanceToPlayer()
    {
        var distance = Vector2.Distance(_player.transform.position, _rb.transform.position);
        if (distance < _distanceMax)
        {
            _currentSpeedRotate = _rotateSpeedClose;
        }else
        {
            _currentSpeedRotate = _rotateSpeedFar;
        }    
    }    

    private void FixedUpdate()
    {
        if (!_isHoming || _isDestroy) return;
        Vector2 toPlayer = ((Vector2)_player.transform.position - _rb.position).normalized;

        Quaternion currentRot = Quaternion.Euler(0, 0, _rb.rotation);
        Quaternion targetRot = Quaternion.FromToRotation(Vector2.right, toPlayer);

        Quaternion newRot = Quaternion.RotateTowards(currentRot, targetRot, _currentSpeedRotate * Time.fixedDeltaTime);

        _rb.rotation = newRot.eulerAngles.z;
        _rb.velocity = newRot * Vector2.right * _speedRocket;

    }

    // =============== Service ============
    public void SetDetroy() => _isDestroy = true;

}
