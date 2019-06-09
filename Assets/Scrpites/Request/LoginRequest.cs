using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class LoginRequest : BaseRequest {

    private LoginPanel loginPanel;

    public override void Awake()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.Login;
        loginPanel = GetComponent<LoginPanel>();
        base.Awake();
    }

    // Use this for initialization
    void Start () {
        //requestCode = RequestCode.None;
        //actionCode = ActionCode.None;
        //loginPanel = GetComponent<LoginPanel>();
    }
	
    public void SendRequest(string username,string password)
    {
        string data = username + "," + password;
        base.SendRequest(data);
    }

    public override void OnResponse(string data)
    {

        ReturnCode returnCode = (ReturnCode)int.Parse(data);
        loginPanel.OnLoginResponse(returnCode);
    }
}
