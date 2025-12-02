using UnityEngine;
using UnityEngine.UI;

public class UIInput : MonoBehaviour
{
    [SerializeField] HoldButton _buttonLeft;
    [SerializeField] HoldButton _buttonRight;
    [SerializeField] Button _buttonJump;
    [SerializeField] Button _buttonDash;
    [SerializeField] Button _buttonAttack;
    [SerializeField] Button _reset;

    private string _tagNeedFind = "Player";
    private MoblieInput _moblieInput;
    private PlayerMovement _player;
    private Vector3 _originalPosition;

    private void Start()
    {
        _moblieInput = FindPlayerInput();
        SetButtonJump();
        SetButtonDash();
        SetButtonAttack();
        SetButtonReset();
    }

    private MoblieInput FindPlayerInput()
    {
        var player = GameObject.FindGameObjectWithTag(_tagNeedFind);
        _originalPosition = player.transform.position;
        if (player.TryGetComponent<PlayerMovement>(out PlayerMovement movement))
        {
            _player = movement; 
            IPlayerInput input = movement.GetPlayerInput();
            if(input is MoblieInput moblieInput) return moblieInput;    
        }    
        return null;
    }    

    private void SetButtonReset()
    {
        _reset.onClick.AddListener(ResetPositionPlayer);
    }

    private void ResetPositionPlayer()
    {
        Debug.Log("Reset");
        _player.transform.position = _originalPosition;
    }

    private void SetButtonJump()
    {
        if(_moblieInput == null) return;
        _buttonJump.onClick.AddListener(_moblieInput.OnJumpButton);
    }    

    private void SetButtonDash()
    {
        if (_moblieInput == null) return;
        _buttonDash.onClick.AddListener(_moblieInput.OnDashButton);
    }

    private void SetButtonAttack()
    {
        if (_moblieInput == null) return;
        _buttonAttack.onClick.AddListener(_moblieInput.OnAttackButton);
    }
    private void Update()
    {
        if(_moblieInput == null) return;
        HandleInput();
    }

    private void HandleInput()
    {
        if (_buttonLeft.isHeld) _moblieInput.OnPressLeft();
        else if (_buttonRight.isHeld) _moblieInput.OnPressRight();
        else _moblieInput.OnRelease(); 
    }    

}
