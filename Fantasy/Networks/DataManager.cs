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
        private static DataManager _instance = new DataManager();
        private MyPlayer playerData = MyPlayer.GetInstance();


        public static DataManager GetInstance()
        {
            return _instance;
        }

        public bool ReadPlayerData()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream;
            try
            {
                Debug.Log("Read Data is processing...");
                string dirPath = Application.dataPath + "/LocalData";
                string filePath = dirPath + "/local.dat";
                if (!File.Exists(filePath))
                {
                    Debug.Log("Read Data is error.");
                    return false;
                }
                stream = new FileStream(filePath, FileMode.Open);
                MyPlayer localPlayer = formatter.Deserialize(stream) as MyPlayer;
                if (localPlayer == null)
                {
                    Debug.Log("Read Data is error.");
                    throw new IOException("localData is bad.");
                }

                MyPlayer._instance = localPlayer;
                Debug.Log("Read Data is successful.");
                return true;
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Debug.Log("Read Data is error.");
                return false;
            }
        }

        public void SavePlayerData()
        {
            string dirPath = Application.dataPath + "/LocalData";
            string filePath = dirPath + "/local.dat";
            if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);

            IFormatter bf = new BinaryFormatter();
            Stream fs = new FileStream(filePath, FileMode.OpenOrCreate);

            try
            {
                bf.Serialize(fs, playerData);
                fs.Close();
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
