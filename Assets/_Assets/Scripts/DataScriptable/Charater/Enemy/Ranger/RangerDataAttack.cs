using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/RangerDataAttack ")]
public class RangerDataAttack : ScriptableObject
{
    public float damage = 1f;
    public float timeAttackStart = 0.5f;
    public float timeAttackActive = 1f;
    public float timeAttackEnd = 0.5f;
    public float maxRanger = 5f;
    public float minRanger = 4f;

    public float cooldownAttack = 4f;
}
