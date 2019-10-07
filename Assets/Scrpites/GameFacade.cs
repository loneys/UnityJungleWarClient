using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class GameFacade : MonoBehaviour {

    private static GameFacade _instance;
    public static GameFacade Instance {
        get {
            if (_instance == null)
            {
                GameObject obj = GameObject.Find("GameFacade");
                if(obj==null)
                {
                    return null;
                }
                else
                {
                    _instance = obj.GetComponent<GameFacade>();
                }
                
            }
            return _instance;
        }
    }

    private UIManager uiManager;
    private CameraManager cameraManager;
    private AudioManager audioManager;
    private PlayerManager playerManager;
    private RequestManager requestManager;
    private ClientManager clientManager;

    private bool isEnterPlay = false;

    //同级的Awake用到GameFacade的时候可能为空
    //private void Awake()
    //{
    //    if(_instance !=null)
    //    {
    //        Destroy(this.gameObject);
    //        return;
    //    }
    //    _instance = this;
    //}

	// Use this for initialization

    private void Awake()
    {
        Screen.SetResolution(1280, 800, false);
    }

	void Start ()
    {
        InitManager();
    }

    void Update()
    {
        UpdateManager();
        if(isEnterPlay)
        {
            EnterPlaying();
            isEnterPlay = false;
        }
    }


    private void OnDestroy()
    {
        DestroyManager();
    }

    private void InitManager()
    {
        uiManager = new UIManager(this);
        cameraManager = new CameraManager(this);
        audioManager = new AudioManager(this);
        playerManager = new PlayerManager(this);
        requestManager = new RequestManager(this);
        clientManager = new ClientManager(this);

        uiManager.OnInit();
        cameraManager.OnInit();
        audioManager.OnInit();
        playerManager.OnInit();
        requestManager.OnInit();
        clientManager.OnInit();
    }

    private void DestroyManager()
    {
        uiManager.OnDestroy();
        cameraManager.OnDestroy();
        audioManager.OnDestroy();
        playerManager.OnDestroy();
        requestManager.OnDestroy();
        clientManager.OnDestroy();
    }

    private void UpdateManager()
    {
        uiManager.Update();
        cameraManager.Update();
        audioManager.Update();
        playerManager.Update();
        requestManager.Update();
        clientManager.Update();
    }

    public void AddRequest(ActionCode actionCode, BaseRequest request)
    {
        requestManager.AddRequest(actionCode, request);
    }

    public void RemoveRequest(ActionCode actionCode)
    {
        requestManager.RemoveRequest(actionCode);
    }

    public void HandleReponse(ActionCode actionCode, string data)
    {
        requestManager.HandleReponse(actionCode, data);
    }

    public void ShowMessage(string msg)
    {
        uiManager.ShowMessage(msg);
    }

    public void SendRequest(RequestCode requestCode,ActionCode actionCode,string data)
    {
        clientManager.SendRequest(requestCode, actionCode, data);
    }

    public void PlaySoundBg(string soundName)
    {
        audioManager.PlaySoundBg(soundName);
    }

    public void PlayNormalSound(string soundName)
    {
        audioManager.PlayNormalSound(soundName);
    }

    public void SetUserData(UserData ud)
    {
        playerManager.UserData = ud;
    }

    public UserData GetUserData()
    {
        return playerManager.UserData;
    }

    public void SetCurrentRoleType(RoleType rt)
    {
        playerManager.SetCurrentRoleType(rt);
    }

    public GameObject GetCurrentRoleGameObject()
    {
        return playerManager.GetCurrentRoleGameObject();
    }

    public void EnterPlayingSync()
    {
        this.isEnterPlay = true;
    }

    public void EnterPlaying()
    {
        playerManager.SpawnRoles();
        cameraManager.FollowRole();
    }

    public void StartPlaying()
    {
        playerManager.AddControlScript();
        playerManager.CreateSyncRequest();
    }

    public void SendAttack(int damage)
    {
        playerManager.SendAttack(damage);
    }

    public void GameOver()
    {
        cameraManager.WalkThroughScene();
        playerManager.GameOver();
    }


    public void UpdateResult(int totalCount, int winCount)
    {
        playerManager.UpdateResult(totalCount, winCount);
    }
}
