using System.Collections.Generic;
using UnityEngine;

public class AreaActivator : MonoBehaviour
{
    private static string _TAG_PLAYER = "Player";

    [SerializeField] GameObject[] _enemyObjs;
    private List<IComBat> _comBats = new();
    void Start()
    {
        SetupEnemy();
    }

    private void SetupEnemy()
    {
        foreach(var enemy in _enemyObjs)
        {
            if(enemy == null) continue;
            if(enemy.TryGetComponent<IComBat>(out IComBat comBat))
            {
                comBat.SetParaCombat(false);
                _comBats.Add(comBat);
            }
        }
    }

    private void SetValueCombatEnemy(bool value)
    {
        foreach(var combat in _comBats)combat.SetParaCombat(value);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(_TAG_PLAYER))
        {
            SetValueCombatEnemy(true);
            Debug.Log("Enter");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(_TAG_PLAYER))
        {
            SetValueCombatEnemy(false);
        }
    }

}
