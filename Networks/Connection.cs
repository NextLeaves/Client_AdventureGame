#define DEBUG

using System.Collections;
using System.Collections.Generic;
using System;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using System.Linq;
using System.Runtime.InteropServices;

public enum NetworkStatus
{
    Disconnected,
    Connecting,
    Connected
}

public class Connection
{
    public const int BUFFER_SIZE = 1024;

    public Socket _socket;
    public byte[] BufferRead;
    public int BufferCount;
    public int BufferRemain
    {
        get { return BUFFER_SIZE - BufferCount; }
    }

    //粘包分包
    public byte[] lenBytes = new byte[sizeof(Int32)];
    public int lenMsg = 0;

    //协议
    public ProtocolBase proto = new ProtocolByte();

    //心跳
    public float lastTickTime = 0;
    public float heartBeatTime = 30;

    //连接状态
    public NetworkStatus status = NetworkStatus.Disconnected;

    //消息分发
    public MessageDistribution _msgDistri = new MessageDistribution();


    public Connection()
    {
        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        BufferRead = new byte[BUFFER_SIZE];
        BufferCount = 0;
    }

    public void Connect(string host, int port)
    {
        try
        {
            status = NetworkStatus.Connecting;
            _socket.Connect(host, port);
            status = NetworkStatus.Connected;
#if DEBUG
            Debug.Log("successfully connect.");
#endif
            _socket.BeginReceive(BufferRead, BufferCount, BufferRemain, SocketFlags.None, ReceiveCb, null);

        }
        catch (Exception ex)
        {
            Console.WriteLine("[Error] Connect Method is error.");
            Console.WriteLine(ex.Message);

        }


    }

    private void ReceiveCb(IAsyncResult ar)
    {
        try
        {
            int count = _socket.EndReceive(ar);
            BufferCount += count;
            ProcessData();
            _socket.BeginReceive(BufferRead, BufferCount, BufferRemain, SocketFlags.None, ReceiveCb, null);
        }
        catch (Exception ex)
        {
            Console.WriteLine("[Error] ReceiveCb Method is error.");
            Console.WriteLine(ex.Message);
        }
    }

    private void ProcessData()
    {
        if (BufferCount < sizeof(Int32)) return;
        Array.Copy(BufferRead, lenBytes, sizeof(Int32));
        lenMsg = BitConverter.ToInt32(lenBytes, 0);
        if (BufferCount < lenMsg + sizeof(Int32)) return;

        proto = proto.Decode(BufferRead, 0, BufferCount);
#if DEBUG
        Debug.Log("收到消息:" + proto.Expression);
#endif

        //消息处理
        lock (_msgDistri.ProtoList)
        {
            _msgDistri.ProtoList.Add(proto);
        }

        //清除已处理消息
        int count = BufferCount - sizeof(Int32) - lenMsg;
        Array.Copy(BufferRead, sizeof(Int32) + lenMsg, BufferRead, 0, count);
        BufferCount = count;
        if (BufferCount > 0) ProcessData();

    }

    public bool Send(ProtocolBase protocol)
    {
        if (status != NetworkStatus.Connected)
        {
            Debug.Log("[Error] Send Method is error.");
            return false;
        }

        _socket.Send(protocol.Encode());
        Debug.Log("发送消息:" + protocol.Expression);
        return true;
    }

    public bool Send(ProtocolBase protocol, Action<ProtocolBase> protoMethod, [Optional]string name)
    {
        if (status != NetworkStatus.Connected) return false;
        if (name == null) name = protocol.Name;
        _msgDistri.AddListener(name, protoMethod);
        return Send(proto);
    }

    public void Update()
    {
        _msgDistri.SolveMessage();
        if (status == NetworkStatus.Connected)
        {
            if (Time.time - lastTickTime > heartBeatTime)
            {
                ProtocolBase protocol = NetworkManager.GetHeartBeatProtocol();
                Send(protocol);
                lastTickTime = Time.time;
            }
        }
    }

    public bool Close()
    {
        try
        {
            if (status == NetworkStatus.Disconnected)
            {
                _socket.Close();
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine("[Error] Close Method is error.");
            Console.WriteLine(ex.Message);
            return false;
        }
    }

}
