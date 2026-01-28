using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcToTalk : NpcController
{
    [SerializeField] string _text = "Hello Bro";
    protected override void HandleAction()
    {
        _uiController.ToTalk(_text);
    }
}
