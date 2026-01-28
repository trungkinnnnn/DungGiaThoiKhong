using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleController : MonoBehaviour
{
    [SerializeField] GameObject _obj;
    [SerializeField] float _timeSpawn = 1f;
    private float _time = -Mathf.Infinity;
    private void Update()
    {
        if(_time < Time.time)
        {
            _time = Time.time + _timeSpawn;
            SpawnObj();
        }    
    }

    private void SpawnObj()
    {
        var obj = Instantiate(_obj, transform.position, Quaternion.identity);
        if(obj.TryGetComponent<RangerController>(out  RangerController controller))
        {
            controller.SetParaCombat(true);
        }
    }    


}
