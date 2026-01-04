using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainAnimationController : EnemyAnimationController
{
    private static int _HAS_ANI_TRIGGER_ATTACK2 = Animator.StringToHash("isAttack2");
    private static int _HAS_ANI_TRIGGER_ATTACK3 = Animator.StringToHash("isAttack3");
    private static int _HAS_ANI_BOOL_ATTACK3DONE = Animator.StringToHash("isAttack3Done");


    // =============== Service =============

    public void PlayAniAttack2() => _animator.SetTrigger(_HAS_ANI_TRIGGER_ATTACK2);
    public void PlayAniAttack3() => _animator.SetTrigger(_HAS_ANI_TRIGGER_ATTACK3);

    public void SetAniAttackDone(bool value) => _animator.SetBool(_HAS_ANI_BOOL_ATTACK3DONE, value);
}
    