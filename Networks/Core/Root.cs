using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.UI.Panel;

public class Root: MonoBehaviour
{
    public static string Account;
    public static string IP = "192.168.1.105";

    void Start()
    {        
        PanelManager._instance.OpenPanel<LoginPanel>("", null);
    }

    void Update()
    {
        NetworkManager.Update();
    }
}
