using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class UpdateResultRequest : BaseRequest {

    private RoomListPanel roomListPanel;
    private bool isUpdateResult = false;
    private int totalCount;
    private int winCount;

    public override void Awake()
    {
        actionCode = ActionCode.UpdateResult;

        roomListPanel = GetComponent<RoomListPanel>();

        base.Awake();
    }

    public override void OnResponse(string data)
    {
        string[] strs = data.Split(',');
        totalCount = int.Parse(strs[0]);
        winCount = int.Parse(strs[1]);
        isUpdateResult = true;

    }

    private void Update()
    {
        if(isUpdateResult)
        {
            roomListPanel.OnUpdateResultResponse(totalCount,winCount);
            isUpdateResult = false;
        }
    }
}
