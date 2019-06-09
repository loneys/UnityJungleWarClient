using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class BaseRequest : MonoBehaviour {

    protected RequestCode requestCode = RequestCode.None;
    protected ActionCode actionCode = ActionCode.None;
    protected GameFacade facade;
    public void setFacade(GameFacade facade)
    {
        this.facade = facade;
    }

	// Use this for initialization
    public virtual void Awake()
    {
        GameFacade.Instance.AddRequest(actionCode, this);
        facade = GameFacade.Instance;
    }

	void Start () {
		
	}

    protected void SendRequest(string data)
    {
        facade.SendRequest(requestCode, actionCode, data);
    }

    //发送消息
    public virtual void SendRequest()
    {

    }

    //接收消息
    public virtual void OnResponse(string data)
    {
        
    }
	
    public virtual void OnDestroy()
    {
        GameFacade.Instance.RemoveRequest(actionCode);
    }
}
