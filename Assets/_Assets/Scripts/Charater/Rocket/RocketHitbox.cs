using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketHitbox : MonoBehaviour
{
    private static string _TAG_PLAYER = "Player";
    private static string _TAG_GROUND = "Ground";

    [SerializeField] GameObject _effectExplosion;
    [SerializeField] GameObject _effectSmokeExplosion;

    [SerializeField] GameObject _visual;

    private RocketController _controller;
    private Rigidbody2D _rb;

    void OnEnable()
    {
        _effectExplosion.SetActive(false);
        _effectSmokeExplosion.SetActive(false);
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _controller = GetComponent<RocketController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(_TAG_GROUND))
        {
            Hit();
            _effectSmokeExplosion.SetActive(true);
            _effectSmokeExplosion.transform.rotation = Quaternion.identity;
            return;
        }   
        
        if(collision.CompareTag(_TAG_PLAYER))
        {
            Hit();
            _effectExplosion.SetActive(true) ;
            _effectSmokeExplosion.transform.rotation = Quaternion.identity;
            return;
        }    
    }

    private void Hit()
    {
        _controller.SetDetroy();
        _rb.velocity = Vector2.zero;
        _visual.SetActive(false);
        Destroy(gameObject, 1f);
        
    }    

}
