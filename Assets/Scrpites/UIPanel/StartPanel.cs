using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartPanel : BasePanel {

    private Button loginButton;

    private Animator btnAnimator;

    public override void OnEnter()
    {
        base.OnEnter();
        loginButton = transform.Find("LoginButton").GetComponent<Button>();
        loginButton.onClick.AddListener(OnLoginClick);

        btnAnimator = loginButton.GetComponent<Animator>();
    }

    private void OnLoginClick()
    {
        uiManager.PushPanel(UIPanelType.Login);
    }

    public override void OnPause()
    {
        base.OnPause();
        //btnAnimator.enabled = false;
        loginButton.transform.DOScale(0, 0.4f).OnComplete(() => loginButton.gameObject.SetActive(false));
    }

    public override void OnResume()
    {
        base.OnResume();
        loginButton.gameObject.SetActive(true);
        loginButton.transform.DOScale(1, 0.2f);//.OnComplete(() => btnAnimator.enabled = true);

    }
}
