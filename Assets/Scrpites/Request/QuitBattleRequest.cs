using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class QuitBattleRequest : BaseRequest
{
    private bool isQuitBattle = false;
    private GamePanel gamePanel;

    public override void Awake()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.QuitBattle;
        gamePanel = GetComponent<GamePanel>();
        base.Awake();
    }

    public override void SendRequest()
    {
        base.SendRequest("r");
    }

    public override void OnResponse(string data)
    {
        isQuitBattle = true;
    }

    public void Update()
    {
        if(isQuitBattle)
        {
            gamePanel.OnExitResponse();
            isQuitBattle = false;
        }
    }
}
