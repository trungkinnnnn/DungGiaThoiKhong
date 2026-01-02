using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingle : MonoBehaviour
{
    public static PlayerSingle Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }    
    }

}
