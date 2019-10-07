using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class GameOverRequest : BaseRequest {

    private GamePanel gamePanel;
    private bool isGmaeOver = false;
    private ReturnCode returnCode;

    public override void Awake()
    {
        requestCode = RequestCode.Game;
        actionCode = ActionCode.GameOver;
        gamePanel = GetComponent<GamePanel>();
        base.Awake();
    }

    private void Update()
    {
        if(isGmaeOver)
        {
            gamePanel.OnGameOverResponse(returnCode);
            isGmaeOver = false;
        }
    }

    public override void OnResponse(string data)
    {
        returnCode = (ReturnCode)int.Parse(data);
        isGmaeOver = true;

    }
}
