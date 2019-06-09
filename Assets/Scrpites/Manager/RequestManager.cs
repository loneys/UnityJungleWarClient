using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class RequestManager : BaseManager
{

    private Dictionary<ActionCode, BaseRequest> requestDict = new Dictionary<ActionCode, BaseRequest>();

    public RequestManager(GameFacade facade) : base(facade) { }

    public void AddRequest(ActionCode actionCode,BaseRequest request)
    {
        requestDict.Add(actionCode, request);
    }

    public void RemoveRequest(ActionCode actionCode)
    {
        requestDict.Remove(actionCode);
    }

    public void HandleReponse(ActionCode actionCode,string data)
    {
        BaseRequest request = requestDict.TryGet<ActionCode,BaseRequest>(actionCode);
        Debug.LogWarning("接收一条消息:\n"+"动作:" + actionCode + " 数据:" + data);
        if (request == null)
        {
            Debug.LogWarning("无法得到ActionCode[" + actionCode + "]对应的方法");
            return;
        }
        request.OnResponse(data);
    }

}
