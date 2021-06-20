using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ConfirmPopUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI PopUpText;

    private Action Callback;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void YesButton()
    {
        Callback();
        gameObject.SetActive(false);
    }

    public void NoButton()
    {
        gameObject.SetActive(false);
    }

    public void Setup(string text, Action Callback)
    {
        PopUpText.text = text;
        this.Callback = Callback;
    }


}
