using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using Common;

public class ClientManager:BaseManager
{
    private const string IP = "127.0.0.1";
    private const int PORT = 6688;

    private Socket clientSocket;
    private Message msg = new Message();

    public ClientManager(GameFacade facade) : base(facade) { }

    public override void OnInit()
    {
        base.OnInit();

        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            clientSocket.Connect(IP, PORT);
            Start();
        }
        catch(Exception e)
        {
            Debug.LogWarning("无法连接到服务器端，清检查你的网络！！" + e);

        }
    }

    public void SendRequest(RequestCode requestCode,ActionCode actionCode,string data)
    {
        byte[] bytes = Message.PackData(requestCode, actionCode, data);
        clientSocket.Send(bytes);
    }

    private void Start()
    {
        clientSocket.BeginReceive(msg.Data,msg.StartIndex,msg.RemainSize,SocketFlags.None, ReceiveCallback,null);
    }

    //这个不是在主线程中，需要访问unity的组件什么的，需要在主线程访问
    private void ReceiveCallback(IAsyncResult ar)
    {
        //Debug.LogWarning("接收到一条数据：");
        try
        {
            if(clientSocket==null ||clientSocket.Connected==false)
            {
                return;
            }
            int count = clientSocket.EndReceive(ar);

            msg.ReadMessage(count, OnProcessDataCallback);

            Start();

        }catch(Exception e)
        {
            Debug.Log(e);
        }
    }

    private void OnProcessDataCallback(ActionCode actionCode,string data)
    {
        facade.HandleReponse(actionCode, data);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        try
        {
            clientSocket.Close();
        }
        catch(Exception e)
        {
            Debug.LogWarning("无法关闭跟服务器的连接！！" + e);
        }
    }


}
