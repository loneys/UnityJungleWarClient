using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Common;
using DG.Tweening;

public class GamePanel : BasePanel {

    private Text timer;
    private int time = -1;
    private Button successButton;
    private Button failButton;
    private Button exitButton;

    private QuitBattleRequest quitBattleRequest;

    private void Start()
    {
        timer = transform.Find("Timer").GetComponent<Text>();
        timer.gameObject.SetActive(false);
        successButton = transform.Find("SuccessButton").GetComponent<Button>();
        successButton.gameObject.SetActive(false);
        successButton.onClick.AddListener(OnResultClick);

        failButton = transform.Find("FailButton").GetComponent<Button>();
        failButton.gameObject.SetActive(false);
        failButton.onClick.AddListener(OnResultClick);

        exitButton = transform.Find("ExitButton").GetComponent<Button>();
        exitButton.gameObject.SetActive(false);
        exitButton.onClick.AddListener(OnExitClick);

        quitBattleRequest = GetComponent<QuitBattleRequest>();
    }

    public void OnExitClick()
    {
        quitBattleRequest.SendRequest();
    }

    public void OnExitResponse()
    {
        OnResultClick();
    }

    public override void OnExit()
    {
        successButton.gameObject.SetActive(false);
        failButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if(time>-1)
        {
            ShowTime(time);
            this.time = -1;
        }
    }

    private void OnResultClick()
    {
        uiManager.PopPanel();
        uiManager.PopPanel();
        facade.GameOver();
    }

    public void ShowTimeSync(int time)
    {
        this.time = time;
    }

    public void ShowTime(int time)
    {
        if (time == 3)
        {
            exitButton.gameObject.SetActive(true);
        }
        timer.gameObject.SetActive(true);
        timer.text = time.ToString();
        timer.transform.localScale = Vector3.one;
        Color tempColor = timer.color;
        tempColor.a = 1;
        timer.color = tempColor;

        timer.transform.DOScale(2, 0.3f).SetDelay(0.3f);
        timer.DOFade(0, 0.3f).SetDelay(0.3f).OnComplete(() => timer.gameObject.SetActive(false));
        facade.PlayNormalSound(AudioManager.Sound_Alert);
    }

    public void OnGameOverResponse(ReturnCode returnCode)
    {
        Button tempButton = null;
        switch(returnCode)
        {
            case ReturnCode.Success:
                tempButton = successButton;
                break;

            case ReturnCode.Fail:
                tempButton = failButton;
                break;
        }
        tempButton.gameObject.SetActive(true);
        tempButton.transform.localScale = Vector3.zero;
        tempButton.transform.DOScale(1, 0.5f);
    }

}
