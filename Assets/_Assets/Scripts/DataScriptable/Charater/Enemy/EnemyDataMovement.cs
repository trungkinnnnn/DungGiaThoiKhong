using UnityEngine;

[CreateAssetMenu(menuName = ("Data/EnemyDataMovement"))]
public class EnemyDataMovement : ScriptableObject
{
    public float speedNormal = 3f;
    public float speedCombat = 4f;
    public float speedVerticalY = 2f;
    public float acceleration = 8f;
    public float timeStop = 3f;
    public float timeMove = 5f;
}
