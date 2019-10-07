using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
public class StartPlayRequest : BaseRequest {

    private bool isStartPlaying = false;

    public override void Awake()
    {
        actionCode = ActionCode.StartPlay;
        Debug.Log("--wangzhi--为什么说重复加载了--");
        base.Awake();
    }

    private void Update()
    {
        if(isStartPlaying)
        {
            facade.StartPlaying();
            isStartPlaying = false;
        }
        
    }

    public override void OnResponse(string data)
    {
        isStartPlaying = true;
    }

}
