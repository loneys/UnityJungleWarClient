using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using Common;

public class LoginPanel : BasePanel {

    private Button closeButton;

    private InputField usernameIF;
    private InputField passwordIF;
    private LoginRequest loginRequest;
    //private Button loginButton;
    //private Button registerButton;

    private void Start()
    {
        usernameIF = transform.Find("UsernamePanel/UsernameInput").GetComponent<InputField>();
        passwordIF = transform.Find("PasswordPanel/PasswordInput").GetComponent<InputField>();
        loginRequest = GetComponent<LoginRequest>();

        closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(OnCloseClick);

        transform.Find("LoginButton").GetComponent<Button>().onClick.AddListener(OnLoginClick);
        transform.Find("RegisterButton").GetComponent<Button>().onClick.AddListener(OnRegisterButtonClick);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        EnterAnimation();
    }

    public override void OnPause()
    {
        HideAnimation();

    }

    public override void OnResume()
    {
        EnterAnimation();
    }

    private void OnCloseClick()
    {
        PlayClickSound();
        uiManager.PopPanel();
    }

    public override void OnExit()
    {
        HideAnimation();
    }

    private void OnLoginClick()
    {
        PlayClickSound();
        string msg = "";
        if(string.IsNullOrEmpty(usernameIF.text))
        {
            msg += "用户名不能为空";
        }
        if(string.IsNullOrEmpty(passwordIF.text))
        {
            msg += "密码不能为空";
        }
        if(msg!="")
        {
            uiManager.ShowMessage(msg);
            return;
        }
        //todo 发送到服务器进行验证
        loginRequest.SendRequest(usernameIF.text, passwordIF.text);
    }

    private void OnRegisterButtonClick()
    {
        PlayClickSound();
        uiManager.PushPanel(UIPanelType.Register);
    }

    public void OnLoginResponse(ReturnCode returnCode)
    {
        //Debug.LogWarning("登录返回结果"+ returnCode);
        if (returnCode == ReturnCode.Success)
        {
            uiManager.ShowMessageSync("登录成功");
            uiManager.PushPanelSync(UIPanelType.RoomList);
        }
        else
        {
            uiManager.ShowMessageSync("用户名或密码错误,无法登录,请重新输入");
        }
    }

    private void EnterAnimation()
    {
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 1);
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOLocalMove(Vector3.zero, 1);
    }

    private void HideAnimation()
    {

        transform.DOScale(0, 0.3f);

        transform.DOLocalMoveX(1000, 0.3f).OnComplete(() => gameObject.SetActive(false));
    }
}
