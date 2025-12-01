using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInput : MonoBehaviour
{
    [SerializeField] HoldButton _buttonLeft;
    [SerializeField] HoldButton _buttonRight;
    [SerializeField] Button _buttonJump;

    private string _tagNeedFind = "Player";
    private MoblieInput _moblieInput;

    private void Start()
    {
        _moblieInput = FindPlayerInput();
        SetUpButtonJump();
    }

    private MoblieInput FindPlayerInput()
    {
        var player = GameObject.FindGameObjectWithTag(_tagNeedFind);
        if(player.TryGetComponent<PlayerMovement>(out PlayerMovement movement))
        {
            IPlayerInput input = movement.GetPlayerInput();
            if(input is MoblieInput moblieInput) return moblieInput;    
        }    
        return null;
    }    

    private void SetUpButtonJump()
    {
        if(_moblieInput == null) return;
        _buttonJump.onClick.AddListener(_moblieInput.OnJumpButton);
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
