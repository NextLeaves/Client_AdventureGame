using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.Panel
{
    public enum PanelLayer
    {
        Panels,
        Hints
    }
    public class PanelBase : MonoBehaviour
    {
        public PanelBase()
        {

        }

        public string SkinPath
        {
            get;

            set;
        }
        public GameObject Skin { get; set; }
        public PanelLayer Layer { get; set; }
        public object[] Args { get; set; }

        public virtual void Close()
        {
            string name = this.GetType().ToString();
            PanelManager._instance.ClosePnael(name);
        }

        public virtual void Init(params object[] args)
        {
            this.Args = args;
        }

        public virtual void OnClosed()
        {
            //throw new System.NotImplementedException();
        }

        public virtual void OnClosing()
        {
            //throw new System.NotImplementedException();
        }

        public virtual void OnShowed()
        {
            //throw new System.NotImplementedException();
        }

        public virtual void OnShowing()
        {
            //throw new System.NotImplementedException();
        }
    }
}