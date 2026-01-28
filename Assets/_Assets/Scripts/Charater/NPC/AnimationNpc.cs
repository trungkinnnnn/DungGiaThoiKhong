using UnityEngine;

public class AnimationNpc : MonoBehaviour
{
    private static int _HAS_ANI_TRIGGER_OPEN = Animator.StringToHash("isOpen");
    private static int _HAS_ANI_TRIGGER_CLOSE = Animator.StringToHash("isClose");

    private Animator _ani;
    private void Awake()
    {
        _ani = GetComponentInChildren<Animator>();
    }

    // =================== Service ================
    public void PlayAniOpen() => _ani.SetTrigger(_HAS_ANI_TRIGGER_OPEN);
    public void PlayAniClose() => _ani.SetTrigger(_HAS_ANI_TRIGGER_CLOSE);
}
