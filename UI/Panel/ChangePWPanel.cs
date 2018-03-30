using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Panel
{
    public class ChangePWPanel : PanelBase
    {
        private Button ok_btn;
        private Button close_btn;
        private InputField acc_field;
        private InputField newpw_field;        

        private MessageDistribution _msgDistri = MessageDistribution.GetInstance();

        private void Awake()
        {
            _msgDistri.AddOnceListenner(NamesOfProtocol.ChangePassword, OnChangePasswordBack);
        }

        public override void Init(params object[] args)
        {
            base.Init(args);
            SkinPath = "ChangePWPanel";
            Layer = PanelLayer.Panels;
        }

        public override void OnShowing()
        {
            base.OnShowing();
            Transform skinTrans = Skin.transform;
            ok_btn = skinTrans.Find("info_panel/ok_btn").GetComponent<Button>();
            close_btn = skinTrans.Find("close_btn").GetComponent<Button>();
            acc_field = skinTrans.Find("info_panel/account_inputfield").GetComponent<InputField>();
            newpw_field = skinTrans.Find("info_panel/password_inputfield").GetComponent<InputField>();            
        }

        public override void OnShowed()
        {
            base.OnShowed();
            ok_btn.onClick.AddListener(OnFindPasswordClick);
            close_btn.onClick.AddListener(OnCloseClick);
        }


        private void OnFindPasswordClick()
        {
            if (string.IsNullOrEmpty(newpw_field.text)||string.IsNullOrEmpty(acc_field.text))
            {
                Debug.Log("[Warn] input nothing.");
                object[] obj = new object[2];
                obj[0] = "错误提示";
                obj[1] = "账号或密码为空，请输入您的信息。";
                PanelManager._instance.OpenPanel<MentionPanel>("", obj);
                return;
            }

            if (NetworkManager.ConnClient.status != NetworkStatus.Connected)
            {
                string host = Root.IP;
                int port = 1234;
                NetworkManager.ConnClient.Connect(host, port);
            }
            ProtocolByte proto = new ProtocolByte();
            proto.AddInfo<string>(NamesOfProtocol.ChangePassword);
            proto.AddInfo<string>(acc_field.text);
            proto.AddInfo<string>(newpw_field.text);
            NetworkManager.ConnClient.Send(proto);
        }

        private void OnChangePasswordBack(ProtocolBase protocol)
        {
            ProtocolByte proto = protocol as ProtocolByte;
            string num = proto.GetString(1);
            if (num == "1")
            {
                Debug.Log("修改密码成功");
                object[] obj = new object[2];
                obj[0] = "信息提示";
                obj[1] = "修改密码成功，请返回登录界面进行登录";
                PanelManager._instance.OpenPanel<MentionPanel>("", obj);
            }
            else
            {
                Debug.Log("修改密码失败");
                object[] obj = new object[2];
                obj[0] = "错误提示";
                obj[1] = "修改密码失败，请输入合法的新密码";
                PanelManager._instance.OpenPanel<MentionPanel>("", obj);
            }
        }

        private void OnCloseClick()
        {
            base.Close();
            PanelManager._instance.OpenPanel<LoginPanel>("", null);
        }
    }
}
