using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class GameFacade : MonoBehaviour {

    private static GameFacade _instance;
    public static GameFacade Instance { get { return _instance; } }

    private UIManager uiManager;
    private CameraManager cameraManager;
    private AudioManager audioManager;
    private PlayerManager playerManager;
    private RequestManager requestManager;
    private ClientManager clientManager;

    private void Awake()
    {
        if(_instance !=null)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
    }

	// Use this for initialization
	void Start ()
    {
        InitManager();
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

}
