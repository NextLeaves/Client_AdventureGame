﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class ProtocolByte : ProtocolBase
{
    private string _name;
    private string _expression;

    public byte[] Data { get; set; }
    public override string Name
    {
        get { return _name; }
        set { _name = value; }
    }
    public override string Expression
    {
        get { return _expression; }
        set { _expression = value; }
    }

    public ProtocolByte()
    {
        _name = "";
        _expression = "";
    }

    private void InitInfomation()
    {
        _name = GetString(0);
        _expression = GetString(-1);
    }

    public override ProtocolBase Decode(byte[] bufferRead, int start, int length)
    {
        this.Data = new byte[length];
        Array.Copy(bufferRead, 0, Data, 0, length);
        //刷新协议
        InitInfomation();
        return this;
    }



    public override byte[] Encode()
    {
        int lenMsg = Expression.Length;
        byte[] lenMsgBytes = BitConverter.GetBytes(lenMsg);
        byte[] MsgBytes = Encoding.Default.GetBytes(Expression);
        if (Data == null)
            Data = lenMsgBytes.Concat(MsgBytes).ToArray();
        else
            Data = Data.Concat(lenMsgBytes).Concat(MsgBytes).ToArray();

        return Data;
    }

    public void AddInfo<T>(T message)
    {
        Expression += message.ToString() + " ";
    }

    public string GetString(int indexof)
    {
        if (Data == null) return "Error Data In ProtocolByte";
        if (Data.Length < sizeof(Int32)) return "Error Data In ProtocolByte";
        int lenMsg = BitConverter.ToInt32(Data, 0);
        if (Data.Length < sizeof(Int32) + lenMsg) return "Error Data In ProtocolByte";

        string msgData = Encoding.UTF8.GetString(Data, sizeof(Int32), lenMsg);
        string[] indexs = msgData.Split(' ');
        if (indexof >= 0 && indexof < indexs.Length)
            return indexs[indexof];
        else if (indexof == -1)
            return msgData;
        else
            throw new IndexOutOfRangeException("indexof must be between 0 and indexs's length.");
    }

    /*   感觉存在问题，几乎不怎么用到，以字符串为主要解析方式
    public int GetInt(int start, ref int end)
    {
        if (Data == null) return 0;
        if (Data.Length < start + sizeof(int)) return 0;
        end = start + sizeof(int);
        return BitConverter.ToInt32(Data, start);
    }

    public int GetInt(int start)
    {
        int end = 0;
        return GetInt(start, ref end);
    }
    */
}

