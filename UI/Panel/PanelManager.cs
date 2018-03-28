using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.UI.Panel
{
    public class PanelManager : MonoBehaviour
    {
        public static PanelManager _instance;
        private GameObject _canvas;
        public Dictionary<string, PanelBase> panels;
        public Dictionary<PanelLayer, Transform> panelLayers;

        private PanelManager() { }

        private void Awake()
        {
            _instance = this;
            panels = new Dictionary<string, PanelBase>();
            panelLayers = new Dictionary<PanelLayer, Transform>();
            InitLayer();
        }

        private void InitLayer()
        {
            _canvas = GameObject.Find("Canvas");
            if (_canvas == null) Debug.LogError("[Error] PanelManager class 's InitLayer method is error.");
            foreach (PanelLayer layer in Enum.GetValues(typeof(PanelLayer)))
            {
                Transform transform = _canvas.transform.Find(layer.ToString());
                panelLayers.Add(layer, transform);
            }
        }

        public void OpenPanel<T>(string skinPath, params object[] args) where T : PanelBase
        {
            string name = typeof(T).ToString();
            if (panels.ContainsKey(name)) return;
            PanelBase panel = _canvas.AddComponent<T>();
            panel.Init(args);
            panels.Add(name, panel);

            skinPath = string.IsNullOrEmpty(skinPath) ? panel.SkinPath : skinPath;
            GameObject skin = Resources.Load<GameObject>(skinPath);
            if (skin == null) Debug.LogError("[Error] PanelManager class 's OpenPanel method is error.");
            panel.Skin = Instantiate<GameObject>(skin);

            Transform skinTrans = panel.Skin.transform;
            PanelLayer layer = panel.Layer;
            skinTrans.SetParent(panelLayers[layer], false);

            panel.OnShowing();
            panel.OnShowed();
        }

        public void ClosePnael(string name)
        {
            PanelBase panel = panels[name];
            if (panel == null) return;

            panel.OnClosing();
            panels.Remove(name);
            panel.OnClosed();

            GameObject.Destroy(panel.Skin);
            Component.Destroy(panel as PanelBase);
        }
    }
}
