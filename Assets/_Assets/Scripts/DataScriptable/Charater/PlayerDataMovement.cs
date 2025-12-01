using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/PlayerMovement")]
public class PlayerDataMovement : ScriptableObject
{
    public float speed;
    public float jumForce;
    public float dashForce;
    public float dashTime;
    public float timeDelayDash;
}
