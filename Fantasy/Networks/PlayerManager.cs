﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Fantasy.Networks
{
    public class PlayerManager : MonoBehaviour
    {
        public GameObject MainPlayer;
        public GameObject other;

        private Dictionary<string, Vector3> players = new Dictionary<string, Vector3>();
        private Dictionary<string, GameObject> playersModel = new Dictionary<string, GameObject>();

        private Vector3 originPos = new Vector3(276.0f, 3.0f, 185.0f);
        private MessageDistribution _msgDis = MessageDistribution.GetInstance();

        private void Start()
        {

            _msgDis.AddOnceListenner(NamesOfProtocol.SendOriginPos, OnSendOriginPosBack);
            _msgDis.AddListener(NamesOfProtocol.UpdatePosition, UpdateLocationBack);
        }

        public void Init()
        {
            CreatePlayer(Root.Account, originPos);
            SendOriginPos();
        }

        public void CreatePlayer(string id, Vector3 pos)
        {
            Instantiate<GameObject>(MainPlayer, pos, Quaternion.identity);
        }

        public void CreateOtherPlayers(string id, Vector3 pos)
        {
            GameObject obj = Instantiate<GameObject>(other, pos, Quaternion.identity);
            players.Add(id, pos);
            playersModel.Add(id, obj);
        }

        public void SendOriginPos()
        {

            if (NetworkManager.ConnClient.status != NetworkStatus.Connected)
            {
                //string host = "127.0.0.1";
                string host = "192.168.1.105";
                int port = 1234;
                NetworkManager.ConnClient.Connect(host, port);
            }

            ProtocolByte proto = new ProtocolByte();
            proto.AddInfo<string>(NamesOfProtocol.SendOriginPos);
            proto.AddInfo<string>(Root.Account);
            proto.AddInfo<float>(originPos.x);
            proto.AddInfo<float>(originPos.y);
            proto.AddInfo<float>(originPos.z);

            NetworkManager.ConnClient.Send(proto);
        }

        private void OnSendOriginPosBack(ProtocolBase protocol)
        {
            ProtocolByte proto = protocol as ProtocolByte;
            string protoName = proto.Name;
            string number = proto.GetString(1);


            if (number != "-1")
            {
                //初始化服务器中已存在的对象数据
                string id = proto.GetString(1);
                string x = proto.GetString(2);
                string y = proto.GetString(3);
                string z = proto.GetString(4);
                float x_f = Convert.ToSingle(x);
                float y_f = Convert.ToSingle(y);
                float z_f = Convert.ToSingle(z);
                Vector3 pos = new Vector3(x_f, y_f, z_f);
                CreatePlayer(id, pos);
            }
            else
            {
                //未正确发送服务器对象数据
            }
        }

        public void UpdateLocation(Vector3 postion)
        {
            ProtocolByte proto = new ProtocolByte();
            proto.AddInfo<string>(NamesOfProtocol.UpdatePosition);
            proto.AddInfo<string>(Root.Account);
            proto.AddInfo<float>(postion.x);
            proto.AddInfo<float>(postion.y);
            proto.AddInfo<float>(postion.z);

            NetworkManager.ConnClient.Send(proto);

        }

        public void UpdateLocationBack(ProtocolBase protocol)
        {
            ProtocolByte proto = protocol as ProtocolByte;
            string id = proto.GetString(1);
            string x = proto.GetString(2);
            string y = proto.GetString(3);
            string z = proto.GetString(4);
            float x_f = Convert.ToSingle(x);
            float y_f = Convert.ToSingle(y);
            float z_f = Convert.ToSingle(z);

            Vector3 pos = new Vector3(x_f, y_f, z_f);

            if (players.ContainsKey(id))
            {
                playersModel[id].transform.position = pos;
            }

            else
            {
                CreateOtherPlayers(id, pos);
            }

        }

    }
}
