using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private static int _HAS_ANI_BOOL_RUNNING = Animator.StringToHash("isRunning");
    private static int _HAS_ANI_TRIGGER_ATTACK = Animator.StringToHash("isAttack");
    private static int _HAS_ANI_TRIGGER_TAKEDAME = Animator.StringToHash("isTakeDame");

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    // =============== Service ===============

    public void SetBoolAniRunning(bool value) => _animator.SetBool(_HAS_ANI_BOOL_RUNNING, value);
    public void PlayAniAttack() => _animator.SetTrigger(_HAS_ANI_TRIGGER_ATTACK);
    public void PlayAniTakeDame() => _animator.SetTrigger(_HAS_ANI_TRIGGER_TAKEDAME);

}
