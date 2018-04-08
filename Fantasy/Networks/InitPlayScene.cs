/*
	Project : 	

	Author : NextLeaves

	Expression:

	Date:

	Repaird Record:

*/

using System;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Assets.Scripts.UI.Panel;
using Assets.Scripts.Fantasy.ObjectClass;
using Assets.Scripts.Fantasy.Networks;
using System.Text;

namespace Assets.Scripts.Fantasy.Networks
{
    public class InitPlayScene : MonoBehaviour
    {
        private PlayerManager _playerMgr;
        private MessageDistribution _msgDistri = MessageDistribution.GetInstance();

        private void Awake()
        {
            _playerMgr = GetComponent<PlayerManager>();
        }

        void Start()
        {
            _msgDistri.AddOnceListenner(NamesOfProtocol.ReceivePlayerData, OnReceivePlayerDataBack);            
            
            _playerMgr.Init();
            StartCoroutine(ReceivePlayerData());
        }

        IEnumerator ReceivePlayerData()
        {
            yield return null;

            if (NetworkManager.ConnClient.status != NetworkStatus.Connected)
            {
                //string host = "127.0.0.1";
                string host = "192.168.1.105";
                int port = 1234;
                NetworkManager.ConnClient.Connect(host, port);
            }
            ProtocolByte proto = new ProtocolByte();
            proto.Expression = "";
            proto.AddInfo<string>(NamesOfProtocol.ReceivePlayerData);
            proto.AddInfo<string>(Root.Account);
            NetworkManager.ConnClient.Send(proto);
        }
        
        void OnReceivePlayerDataBack(ProtocolBase protocol)
        {
            ProtocolByte proto = protocol as ProtocolByte;
            string id = proto.GetString(1);
            if (id == Root.Account)
            {
                //初始化客户端的游戏数据
                int coin = Convert.ToInt32(proto.GetString(2));
                int money = Convert.ToInt32(proto.GetString(3));
                int star = Convert.ToInt32(proto.GetString(4));
                int diamand = Convert.ToInt32(proto.GetString(5));

                if (DataManager.GetInstance().playerData != null)
                    DataManager.GetInstance().playerData.Init(coin, money, star, diamand);
            }
            else
            {
                Debug.Log("[Error]获取数据失败...");
                object[] obj = new object[2];
                obj[0] = "错误提示";
                obj[1] = "初始化数据失败，请退出，重新操作";
                PanelManager._instance.OpenPanel<MentionPanel>("", obj);
            }
        }

       
    }
}


