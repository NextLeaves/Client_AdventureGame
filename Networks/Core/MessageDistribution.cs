using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MessageDistribution
{
    private static MessageDistribution _instance = new MessageDistribution();

    public int Efficiency { get; set; }
    public List<ProtocolBase> ProtoList { get; set; }

    private Dictionary<string, Action<ProtocolBase>> eventDict;
    private Dictionary<string, Action<ProtocolBase>> onceDict;

    private MessageDistribution()
    {
        Efficiency = 15;
        ProtoList = new List<ProtocolBase>();
        eventDict = new Dictionary<string, Action<ProtocolBase>>();
        onceDict = new Dictionary<string, Action<ProtocolBase>>();
    }

    public static MessageDistribution GetInstance()
    {
        if (_instance == null)
        {
            lock (_instance)
            {
                return _instance ?? new MessageDistribution();
            }
        }
        return _instance;
    }

    public void SolveMessage()
    {
        for (int i = 0; i < Efficiency; i++)
        {
            if (ProtoList.Count > 0)
            {
                DispatchMessageEvent(ProtoList[i]);
                lock (ProtoList)
                {
                    ProtoList.RemoveAt(i);
                }
            }
            else
            {
                break;
            }
        }
    }

    public void DispatchMessageEvent(ProtocolBase protocol)
    {
        string name = protocol.Name;
#if DEBUG
        Debug.Log("分发处理消息");
        Debug.Log(protocol.Name);
#endif
        if (eventDict.ContainsKey(name))
            eventDict[name](protocol);
        if (onceDict.ContainsKey(name))
        {
            onceDict[name](protocol);
            onceDict[name] = null;
            onceDict.Remove(name);
        }
    }

    public void AddListener(string name, Action<ProtocolBase> protoMethod)
    {
        if (eventDict.ContainsKey(name))
            eventDict[name] += protoMethod;
        else
            eventDict[name] = protoMethod;
    }

    public void AddOnceListenner(string name, Action<ProtocolBase> protoMethod)
    {
        if (onceDict.ContainsKey(name))
            onceDict[name] += protoMethod;
        else
            onceDict[name] = protoMethod;
    }

    public void DeleteListener(string name, Action<ProtocolBase> protoMethod)
    {
        if (eventDict.ContainsKey(name))
        {
            eventDict[name] -= protoMethod;
            if (eventDict[name] == null)
                eventDict.Remove(name);
        }
    }

    public void DeleteOnceListener(string name, Action<ProtocolBase> protoMethod)
    {
        if (onceDict.ContainsKey(name))
        {
            onceDict[name] -= protoMethod;
            if (onceDict[name] == null)
                onceDict.Remove(name);
        }
    }
}

