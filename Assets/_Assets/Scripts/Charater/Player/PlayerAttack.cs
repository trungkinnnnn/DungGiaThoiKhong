using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] PlayerDataAttack _dataAttack;

    private bool _canAttack = true;
    private bool _isAttack;

    private PlayerAnimationController _ani;
    private PlayerMovement _movement;
    private IPlayerInput _input;

    private void Start()
    {
        _movement = GetComponent<PlayerMovement>();
        _ani = GetComponent<PlayerAnimationController>();
        _input = _movement.GetPlayerInput();
    }

    private void Update()
    {
        if(_input.Block) return;
        HandleAttack();
    }

    private void HandleAttack()
    {
        if(_input.AttackPressed && _canAttack)
        {
            _ani.PlayAniAttack();
            StartCoroutine(Attack());
        }
    }    

    private IEnumerator Attack()
    {
        _canAttack = false;
        _movement.IsAttack(true);
        yield return null;  
        _input.Block = true;  

        yield return new WaitForSeconds(_dataAttack.timeAttack);
       
        _input.Block = false;
        _movement.IsAttack(false);
        _input.ResetAttack();

        yield return new WaitForSeconds(_dataAttack.timeDelayAttack);
        _canAttack = true;
    }

    // ================ Service ==================

}
