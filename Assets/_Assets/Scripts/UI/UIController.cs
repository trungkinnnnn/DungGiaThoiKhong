using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField] GameObject _uiShadow;
    [SerializeField] GameObject _uiToTalk;
    [SerializeField] GameObject _uiToShop;

    [SerializeField] TextMeshProUGUI _textMeshPro;
    [SerializeField] float _delayChar;

    [SerializeField] Button _buttonCancel;


    private Coroutine _typeCoroutine;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        _uiShadow.SetActive(false);
        _uiToTalk.SetActive(false);
        _uiToShop.SetActive(false);

        _buttonCancel.onClick.AddListener(CancelNPC);
        _buttonCancel.gameObject.SetActive(false);
    }

    private void CancelNPC()
    {
        _buttonCancel.gameObject.SetActive(false);
        _uiShadow.SetActive(false);
        _uiToShop.SetActive(false);
        _uiToTalk.SetActive(false);

    }

    private IEnumerator TypeText(string s)
    {
        _textMeshPro.text = "";
        foreach(var val in s)
        {
            _textMeshPro.text += val;
            yield return new WaitForSeconds(_delayChar);
        }    
    }    



    // =============== Service ===============
    public void ToTalk(string text)
    {
        _buttonCancel.gameObject.SetActive(true);
        _uiShadow.SetActive(true);
        _uiToTalk.SetActive(true);
        _uiToShop.SetActive(false);
       
        if(_typeCoroutine != null)
            StopCoroutine( _typeCoroutine );

        _typeCoroutine = StartCoroutine( TypeText(text));

    }

    public void ToShop()
    {
        _buttonCancel.gameObject.SetActive(true);
        _uiShadow.SetActive(true);
        _uiToTalk.SetActive(false);
        _uiToShop.SetActive(true);
    }

}
