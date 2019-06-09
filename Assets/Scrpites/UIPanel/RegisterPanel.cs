using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;

public class RegisterPanel : BasePanel {

    private InputField usernameIF;
    private InputField passwordIF;
    private InputField rePasswordIf;

    private RegisterRequest registerRequest;

    private void Start()
    {
        usernameIF = transform.Find("UsernamePanel/UsernameInput").GetComponent<InputField>();
        passwordIF = transform.Find("PasswordPanel/PasswordInput").GetComponent<InputField>();
        rePasswordIf = transform.Find("RePasswordButton/PasswordInput").GetComponent<InputField>();

        registerRequest = GetComponent<RegisterRequest>();

        transform.Find("RegisterButton").GetComponent<Button>().onClick.AddListener(OnRegisterClick);
        transform.Find("CloseButton").GetComponent<Button>().onClick.AddListener(OnCloseClick);
    }

    public override void OnEnter()
    {
        gameObject.SetActive(true);

        transform.localScale = Vector3.zero;
        transform.DOScale(1, 1);
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOLocalMove(Vector3.zero, 1);
    }

    private void OnRegisterClick()
    {
        Debug.LogWarning("--wangzhi--点击了注册按钮--");
        string msg = "";
        if(string.IsNullOrEmpty(usernameIF.text))
        {
            msg += "用户名不能为空";
        }
        if (string.IsNullOrEmpty(passwordIF.text))
        {
            msg += "密码不能为空";
        }
        if (passwordIF.text!=rePasswordIf.text)
        {
            msg += "密码不一致";
        }
        if (msg != "")
        {
            uiManager.ShowMessage(msg);
            return;
        }
        //进行注册，发送到服务端
        registerRequest.SendRequest(usernameIF.text, passwordIF.text);
    }

    public void OnRegisterResponse(ReturnCode returnCode)
    {
        if(returnCode==ReturnCode.Success)
        {
            uiManager.ShowMessageSync("注册成功");
        }
        else
        {
            uiManager.ShowMessageSync("用户名重复");
        }
    }

    private void OnCloseClick()
    {
        transform.DOScale(0, 1f);
        Tweener tweener = transform.DOLocalMove(new Vector3(1000, 0, 0), 0.2f);
        tweener.OnComplete(() => uiManager.PopPanel());
    }

    public override void OnExit()
    {
        base.OnExit();
        gameObject.SetActive(false);
    }
}
