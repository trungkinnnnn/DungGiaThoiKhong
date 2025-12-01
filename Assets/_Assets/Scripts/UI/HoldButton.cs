using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isHeld {  get; private set; }

    public void OnPointerDown(PointerEventData eventData) => isHeld = true;

    public void OnPointerUp(PointerEventData eventData) => isHeld = false;
}
