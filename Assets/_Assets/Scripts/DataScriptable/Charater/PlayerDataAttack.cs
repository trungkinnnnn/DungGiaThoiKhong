using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/PlayerAttack")]
public class PlayerDataAttack : ScriptableObject
{
    public float damageAttack;
    public float timeAttack = 0.4f;
    public float timeDelayAttack;
}
