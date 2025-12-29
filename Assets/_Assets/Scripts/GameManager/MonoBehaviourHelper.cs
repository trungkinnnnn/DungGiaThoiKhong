using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourHelper : MonoBehaviour
{
    public static MonoBehaviourHelper Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }
        
    }
}
