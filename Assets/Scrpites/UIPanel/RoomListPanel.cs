using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Common;

public class RoomListPanel : BasePanel
{
    private RectTransform battleRes;
    private RectTransform roomList;
    private VerticalLayoutGroup roomLayout;
    private GameObject roomItemPrefab;
    private ListRoomRequest listRoomRequest;
    private CreateRoomRequest createRoomRequest;
    private JoinRoomRequest joinRoomRequest;

    private UserData ud1 = null;
    private UserData ud2 = null;

    private List<UserData> udList = null;

    private void Start()
    {
        battleRes = transform.Find("BattleRes").GetComponent<RectTransform>();
        roomList = transform.Find("RoomList").GetComponent<RectTransform>();

        roomLayout = transform.Find("RoomList/ScrollRect/Layout").GetComponent<VerticalLayoutGroup>();
        roomItemPrefab = Resources.Load("UIPanel/RoomItem") as GameObject;

        transform.Find("RoomList/CloseButton").GetComponent<Button>().onClick.AddListener(OnCloseClick);

        transform.Find("RoomList/CreateRoomButton").GetComponent<Button>().onClick.AddListener(OnCreateRoomClick);

        transform.Find("RoomList/RefreshButton").GetComponent<Button>().onClick.AddListener(OnRefreshClick);

        listRoomRequest = GetComponent<ListRoomRequest>();

        createRoomRequest = GetComponent<CreateRoomRequest>();

        joinRoomRequest = GetComponent<JoinRoomRequest>();

        EnterAnim();
    }

    public override void OnEnter()
    {
        SetBattleRes();
        if (battleRes != null)
        {
            EnterAnim();
        }
        if (listRoomRequest == null)
        {
            listRoomRequest = GetComponent<ListRoomRequest>();
        }
        listRoomRequest.SendRequest();
    }

    public override void OnExit()
    {
        HideAnim();
    }

    public override void OnPause()
    {
        HideAnim();
    }


    public override void OnResume()
    {
        EnterAnim();
        listRoomRequest.SendRequest();
    }

    private void Update()
    {
        if(udList!=null)
        {
            LoadRoomItem(udList);
            udList = null;
        }
        if (ud1 != null && ud2 != null)
        {
            BasePanel panel = uiManager.PushPanel(UIPanelType.Room);
            (panel as RoomPanel).SetAllPlayerResSync(ud1, ud2);
            this.ud1 = null;
            this.ud2 = null;
        }
    }


    private void OnCloseClick()
    {
        PlayClickSound();
        uiManager.PopPanel();
    }

    private void OnCreateRoomClick()
    {
        Debug.LogWarning("这里为啥没加载出来啊");
        BasePanel panel = uiManager.PushPanel(UIPanelType.Room);
        createRoomRequest.SetPanel(panel);
        createRoomRequest.SendRequest();
    }

    private void OnRefreshClick()
    {
        listRoomRequest.SendRequest();
    }

    private void EnterAnim()
    {
        gameObject.SetActive(true);
        battleRes.localPosition = new Vector3(-1000, 0);
        battleRes.DOLocalMoveX(-400, 0.5f);

        roomList.localPosition = new Vector3(1000, 0);
        roomList.DOLocalMoveX(100, 0.5f);
    }

    private void HideAnim()
    {
        battleRes.DOLocalMoveX(-1000, 0.5f);

        roomList.DOLocalMoveX(1000, 0.5f).OnComplete(() => gameObject.SetActive(false));
    }

    private void SetBattleRes()
    {
        UserData ud = facade.GetUserData();
        transform.Find("BattleRes/Username").GetComponent<Text>().text = ud.Username;
        transform.Find("BattleRes/TotalCount").GetComponent<Text>().text = "总场数:"+ud.TotalCount.ToString();
        transform.Find("BattleRes/WinCount").GetComponent<Text>().text = "胜利:"+ud.WinCount.ToString();

    }

    public void LoadRoomItemSync(List<UserData> udList)
    {
        this.udList = udList;
    }

    private void LoadRoomItem(List<UserData> udList)
    {
        RoomItem[] riArray = roomLayout.GetComponentsInChildren<RoomItem>();
        foreach(RoomItem ri in riArray)
        {
            ri.DestroySelf();
        }

        int count = udList.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject roomItem = GameObject.Instantiate(roomItemPrefab);
            roomItem.transform.SetParent(roomLayout.transform);
            UserData ud = udList[i];
            roomItem.GetComponent<RoomItem>().SetRoomInfo(ud.Id, ud.Username, ud.TotalCount, ud.WinCount,this);
        }
        int roomCount = GetComponentsInChildren<RoomItem>().Length;
        Vector2 size = roomLayout.GetComponent<RectTransform>().sizeDelta;
        roomLayout.GetComponent<RectTransform>().sizeDelta = new Vector2(size.x, roomCount * roomItemPrefab.GetComponent<RectTransform>().sizeDelta.y+roomLayout.spacing);
    }

    public void OnJoinClick(int id)
    {
        joinRoomRequest.SendRequest(id);
    }

    public void OnJoinResponse(ReturnCode returnCode,UserData ud1,UserData ud2)
    {
        switch(returnCode)
        {
            case ReturnCode.NotFound:
                uiManager.ShowMessageSync("房间被销毁无法加入");
                break;
            case ReturnCode.Fail:
                uiManager.ShowMessageSync("房间已满，无法加入");
                break;
            case ReturnCode.Success:
                this.ud1 = ud1;
                this.ud2 = ud2;
                break;

        }
    }

    public void OnUpdateResultResponse(int totalCount,int winCount)
    {
        facade.UpdateResult(totalCount, winCount);
        SetBattleRes();
    }

}
