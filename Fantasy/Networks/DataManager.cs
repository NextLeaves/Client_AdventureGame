using Assets.Scripts.Fantasy.ObjectClass;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts.Fantasy.Networks
{
    public class DataManager
    {
        public MyPlayer playerData = new MyPlayer();

        private static DataManager _instance = new DataManager();

        private DataManager()
        {
            playerData.AddListen(RemoteSaveData);
        }

        public static DataManager GetInstance()
        {
            return _instance;
        }

        /// <summary>
        /// 对文件读取的方式：
        /// 1. unity中生成对象
        /// 2. 针对不同的数据，修改对象的值（而不是直接引用复制）
        /// </summary>
        /// <returns></returns>
        public bool ReadLocalFile()
        {
            string dirPath = Application.dataPath + "/Data";
            string filePath = dirPath + "/sdata.dat";
            if (!Directory.Exists(dirPath) || !File.Exists(filePath))
            {
                playerData = new MyPlayer();
                return false;
            }
            IFormatter bf = new BinaryFormatter();
            using (Stream fs = new FileStream(filePath, FileMode.Open))
            {
                fs.Position = 0;
                MyPlayer data_seria = bf.Deserialize(fs) as MyPlayer;
                playerData.Init(data_seria.Coin, data_seria.Money, data_seria.Star, data_seria.Diamand);
                return true;
            }
        }

        public void WriteLocalFile()
        {
            string dirPath = Application.dataPath + "/Data";
            string filePath = dirPath + "/sdata.dat";
            if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
            IFormatter bf = new BinaryFormatter();
            Stream fs = new FileStream(filePath, FileMode.Truncate);
            try
            {
                bf.Serialize(fs, playerData);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                fs.Close();
            }
        }

        public byte[] WriteRemoteFile()
        {
            using (Stream ms = new MemoryStream())
            {
                IFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, playerData);
                byte[] ds = new byte[ms.Length];
                ms.Write(ds, 0, ds.Length);
                return ds;
            }
        }

        public void RemoteSaveData(object sender, EventArgs args)
        {
            ProtocolByte protoRet = new ProtocolByte();
            protoRet.AddInfo<string>(NamesOfProtocol.SendPlayerData);
            protoRet.AddInfo<string>(Root.Account);
            protoRet.AddInfo<int>(this.playerData.Coin);
            protoRet.AddInfo<int>(this.playerData.Money);
            protoRet.AddInfo<int>(this.playerData.Star);
            protoRet.AddInfo<int>(this.playerData.Diamand);

            NetworkManager.ConnClient.Send(protoRet);
        }

        public void Logout()
        {
            ProtocolByte protoRet = new ProtocolByte();
            protoRet.AddInfo<string>(NamesOfProtocol.Logout);
            protoRet.AddInfo<string>(Root.Account);

            NetworkManager.ConnClient.Send(protoRet);
        }
    }
}
