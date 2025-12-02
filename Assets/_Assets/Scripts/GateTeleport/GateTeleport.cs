using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTeleport : MonoBehaviour
{
    [SerializeField] Transform _positionTele;

    private static string _TAG_PLAYER = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(_TAG_PLAYER))
        {
            Transform root = collision.transform.root;
            root.position = _positionTele.position;
            Debug.Log("tele");
        }
    }
}
