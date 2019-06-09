using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Linq;
using Common;

public class Message{

    private byte[] data = new byte[1024];
    //我们存储了多少个字节在数组里,处理一条就减少。这个值相当于长度。
    private int startIndex = 0;

    //public void AddCount(int count)
    //{
    //    startIndex += count;
    //}

    public byte[] Data
    {
        get
        {
            return data;
        }
    }

    public int StartIndex
    {
        get
        {
            return startIndex;
        }
    }
    public int RemainSize
    {
        get
        {
            return data.Length - startIndex;
        }
    }

    //解析数据，客户端解析数据没有actionCode，服务端只会传消息号和内容。
    public void ReadMessage(int newDataAmount, Action<ActionCode, string> processDataCallBack)
    {
        startIndex += newDataAmount;
        while (true)
        {
            if (startIndex <= 4)
            {
                return;
            }
            //解析出数据总长
            int count = BitConverter.ToInt32(data, 0);
            if ((startIndex - 4) >= count)
            {
                //解析出requesCode,这里的actionCode 类型是枚举类型，用 as 方式转型不行，采用强制转型的方式。
                ActionCode actionCode = (ActionCode)BitConverter.ToInt32(data, 4);
                //解析出数据
                string s = Encoding.UTF8.GetString(data, 8, count - 4);
                Console.WriteLine("解析出来一条数据:" + actionCode + s);
                //触发回调
                processDataCallBack(actionCode, s);
                Array.Copy(data, count + 4, data, 0, startIndex - 4 - count);
                startIndex -= (count + 4);
            }
            else
            {
                break;
            }
        }
    }

    //public static byte[] PackData(ActionCode actionCode, string data)
    //{
    //    //将消息号转换为字节数组
    //    byte[] actionCodeBytes = BitConverter.GetBytes((int)actionCode);
    //    //将消息内容转换为字节数组
    //    byte[] dataBytes = Encoding.UTF8.GetBytes(data);
    //    //得到消息号和消息内容的长度和
    //    int dataAmount = actionCodeBytes.Length + dataBytes.Length;
    //    byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);
    //    return dataAmountBytes.Concat(actionCodeBytes).ToArray<byte>().Concat(dataBytes).ToArray<byte>();
    //}

    public static byte[] PackData(RequestCode requestCode,ActionCode actionCode, string data)
    {
        Debug.LogWarning("发送一条消息:\n"+"类型:"+ requestCode + " 动作:" + actionCode + " 数据:" + data);
        //将消息号转换为字节数组
        byte[] requestCodeBytes = BitConverter.GetBytes((int)requestCode);
        //将消息动作转为字节数组
        byte[] actionCodeBytes = BitConverter.GetBytes((int)actionCode);
        //将消息内容转换为字节数组
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        //得到消息号和消息内容的长度和
        int dataAmount = requestCodeBytes.Length + actionCodeBytes.Length + dataBytes.Length;
        byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);
        return dataAmountBytes.Concat(requestCodeBytes).ToArray<byte>()
            .Concat(actionCodeBytes).ToArray<byte>()
            .Concat(dataBytes).ToArray<byte>();
    }
}
