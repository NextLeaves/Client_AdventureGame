using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.UI.Panel;

public class Root: MonoBehaviour
{
    void Start()
    {        
        PanelManager._instance.OpenPanel<LoginPanel>("", null);
    }

    void Update()
    {
        NetworkManager.Update();
    }
}
