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
            _msgDistri.AddOnceListenner(NamesOfProtocol.SendPlayerData, OnSendPlayerDataBack);
            Networks.DataManager.GetInstance().ReadLocalFile();
            _playerMgr.Init();
           // StartCoroutine(ReceivePlayerData());
        }        

        void Update()
        {

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

        IEnumerator SendPlayerData()
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
            proto.AddInfo<string>(NamesOfProtocol.SendPlayerData);
            proto.AddInfo<string>(Root.Account);
            proto.AddInfo<string>(Encoding.Default.GetString(DataManager.GetInstance().WriteRemoteFile()));

            NetworkManager.ConnClient.Send(proto);
        }

        void OnReceivePlayerDataBack(ProtocolBase protocol)
        {
            ProtocolByte proto = protocol as ProtocolByte;
            string number = proto.GetString(1);
            if (number !="-1")
            {
                //初始化客户端的游戏数据
                byte[] data = Encoding.Default.GetBytes(proto.GetString(1));
                using (Stream ms = new MemoryStream(data))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    MyPlayer temp_Player = bf.Deserialize(ms) as MyPlayer;
                    DataManager.GetInstance().playerData.Init(temp_Player.Coin, temp_Player.Money, temp_Player.Star, temp_Player.Diamand);
                }
                Debug.Log("数据获取成功");
            }
            else
            {                
                Debug.Log("未获取数据--正在初始化");
                StartCoroutine(SendPlayerData());
            }
        }

        void OnSendPlayerDataBack(ProtocolBase protocol)
        {
            ProtocolByte proto = protocol as ProtocolByte;
            string num = proto.GetString(1);
            if (num == "1")
            {
                Debug.Log("init data successful.");
                StartCoroutine(ReceivePlayerData());
            }
            else
            {
                Debug.Log("初始化数据失败");
                object[] obj = new object[2];
                obj[0] = "错误提示";
                obj[1] = "初始化数据失败，请重新登录！";
                PanelManager._instance.OpenPanel<MentionPanel>("", obj);
                SceneManager.LoadScene(0);
            }
        }
    }
}


