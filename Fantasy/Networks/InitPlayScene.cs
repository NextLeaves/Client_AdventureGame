/*
	Project : 	

	Author : NextLeaves

	Expression:

	Date:

	Repaird Record:

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Assets.Scripts.UI.Panel;


namespace Assets.Scripts.Fantasy.Networks
{
    public class InitPlayScene : MonoBehaviour
    {

        private MessageDistribution _msgDistri = MessageDistribution.GetInstance();

        void Start()
        {
            _msgDistri.AddOnceListenner(NamesOfProtocol.ReadPlayerData, OnReadPlayerDataBack);
            _msgDistri.AddOnceListenner(NamesOfProtocol.CreatePlayer, OnCreatePlayerDataBack);
            StartCoroutine(GetPlayerData());
        }

        void Update()
        {

        }

        IEnumerator GetPlayerData()
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
            proto.AddInfo<string>(NamesOfProtocol.ReadPlayerData);
            proto.AddInfo<string>(Root.Account);
            NetworkManager.ConnClient.Send(proto);
        }

        IEnumerator CreatePlayerData()
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
            proto.AddInfo<string>(NamesOfProtocol.CreatePlayer);
            proto.AddInfo<string>(Root.Account);
            NetworkManager.ConnClient.Send(proto);
        }

        void OnReadPlayerDataBack(ProtocolBase protocol)
        {
            ProtocolByte proto = protocol as ProtocolByte;
            string score = proto.GetString(1);
            if (score == "-1")
            {
                Debug.Log("未获取数据--正在初始化");
                StartCoroutine(CreatePlayerData());
            }
            else
            {
                Debug.Log("数据获取成功");
                //初始化客户端的游戏数据
                print(score);
            }
        }

        void OnCreatePlayerDataBack(ProtocolBase protocol)
        {
            ProtocolByte proto = protocol as ProtocolByte;
            string num = proto.GetString(1);
            if (num == "1")
            {
                Debug.Log("init data successful.");
                StartCoroutine(GetPlayerData());
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


