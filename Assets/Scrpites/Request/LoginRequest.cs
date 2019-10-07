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
        string[] strs = data.Split(',');
        ReturnCode returnCode = (ReturnCode)int.Parse(strs[0]);
        loginPanel.OnLoginResponse(returnCode);
        if(returnCode == ReturnCode.Success)
        {
            int id = int.Parse(strs[0]);
            string username = strs[1];
            int totalCount = int.Parse(strs[2]);
            int winCount = int.Parse(strs[3]);
            UserData ud = new UserData(id, username, totalCount, winCount);
            facade.SetUserData(ud);
        }
    }
}
