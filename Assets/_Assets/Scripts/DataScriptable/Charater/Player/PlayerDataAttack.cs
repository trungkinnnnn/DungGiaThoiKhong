using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/PlayerAttack")]
public class PlayerDataAttack : ScriptableObject
{
    public AttackData attack1;
    public AttackData attack2;
    public AttackData attack3;

    public float comboCooldown = 1f;
    
}


[System.Serializable]
public class AttackData
{
    public float damage;

    [Header("Timing")]
    public float startup;
    public float active;

    [Header("TimeBlock")]
    public float timeBlock;
}