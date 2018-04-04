using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Panel
{
    public class FindoutPanel : PanelBase
    {
        private Button ok_btn;
        private Button close_btn;
        private InputField acc_field;
        private InputField code_field;

        private MessageDistribution _msgDistri = MessageDistribution.GetInstance();

        private bool isFindout = false;

        private void Awake()
        {
            _msgDistri.AddOnceListenner(NamesOfProtocol.FindPassword, OnFindPasswordBack);
        }

        public override void Init(params object[] args)
        {
            base.Init(args);
            SkinPath = "FindoutPanel";
            Layer = PanelLayer.Panels;
        }

        public override void OnShowing()
        {
            base.OnShowing();
            Transform skinTrans = Skin.transform;
            ok_btn = skinTrans.Find("info_panel/ok_btn").GetComponent<Button>();
            close_btn = skinTrans.Find("close_btn").GetComponent<Button>();
            acc_field = skinTrans.Find("info_panel/account_inputfield").GetComponent<InputField>();
            code_field = skinTrans.Find("info_panel/code_inputfield").GetComponent<InputField>();
        }

        public override void OnShowed()
        {
            base.OnShowed();
            ok_btn.onClick.AddListener(OnFindPasswordClick);
            close_btn.onClick.AddListener(OnCloseClick);
        }


        private void OnFindPasswordClick()
        {
            if (string.IsNullOrEmpty(acc_field.text) || string.IsNullOrEmpty(code_field.text))
            {
                Debug.Log("[Warn] input nothing.");
                object[] obj = new object[2];
                obj[0] = "错误提示";
                obj[1] = "账号或口令空，请输入您的信息。";
                PanelManager._instance.OpenPanel<MentionPanel>("", obj);
                return;
            }

            if (NetworkManager.ConnClient.status != NetworkStatus.Connected)
            {
                string host = Root.IP;
                int port = 1234;
                NetworkManager.ConnClient.Connect(host, port);
            }

            if (!isFindout) isFindout = true;

            ProtocolByte proto = new ProtocolByte();
            proto.AddInfo<string>(NamesOfProtocol.FindPassword);
            proto.AddInfo<string>(acc_field.text);
            proto.AddInfo<string>(code_field.text);
            NetworkManager.ConnClient.Send(proto);
        }

        private void OnFindPasswordBack(ProtocolBase protocol)
        {
            if (isFindout) isFindout = false;

            ProtocolByte proto = protocol as ProtocolByte;
            string num = proto.GetString(1);
            if (num == "1")
            {
                Debug.Log("验证成功");
                base.Close();
                PanelManager._instance.OpenPanel<ChangePWPanel>("", null);
            }
            else
            {
                Debug.Log("注册失败--password is error");
                object[] obj = new object[2];
                obj[0] = "错误提示";
                obj[1] = "您输入的账号不存在或者口令错误,请检查后重新操作。";
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
