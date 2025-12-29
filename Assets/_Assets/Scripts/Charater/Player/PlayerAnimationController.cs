using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private static int _HAS_ANI_BOOL_ISGROUND = Animator.StringToHash("isGround");
    private static int _HAS_ANI_BOOL_ISRUNNING = Animator.StringToHash("isRunning");
    private static int _HAS_ANI_TRIGGER_ISJUMPUP = Animator.StringToHash("isJumpUp");
    private static int _HAS_ANI_TRIGGER_ISJUMPDOWN = Animator.StringToHash("isJumpDown");
    private static int _HAS_ANI_TRIGGER_ISATTACK = Animator.StringToHash("isAttacking");
    private static int _HAS_ANI_TRIGGER_ISATTACK2 = Animator.StringToHash("isAttacking2");
    private static int _HAS_ANI_TRIGGER_ISATTACK3 = Animator.StringToHash("isAttacking3");

    private Animator _ani;

    private void Awake()
    {
        _ani = GetComponentInChildren<Animator>();
    }


    // ============== Service ================
    public void SetIsGround(bool isGround) => _ani.SetBool(_HAS_ANI_BOOL_ISGROUND, isGround);
    public void PlayAniRunning(bool value) => _ani.SetBool(_HAS_ANI_BOOL_ISRUNNING, value);  

    public void PlayAniJumpUp() => _ani.SetTrigger(_HAS_ANI_TRIGGER_ISJUMPUP);

    public void PlayAniJumDown() => _ani.SetTrigger(_HAS_ANI_TRIGGER_ISJUMPDOWN);

    public void PlayAniAttack() => _ani.SetTrigger(_HAS_ANI_TRIGGER_ISATTACK);
    public void PlayAniAttack2() => _ani.SetTrigger(_HAS_ANI_TRIGGER_ISATTACK2);
    public void PlayAniAttack3() => _ani.SetTrigger(_HAS_ANI_TRIGGER_ISATTACK3);


}
