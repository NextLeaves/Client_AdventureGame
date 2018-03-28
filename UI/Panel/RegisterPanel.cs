using UnityEngine.UI;
using UnityEngine;

namespace Assets.Scripts.UI.Panel
{
    public class RegisterPanel : PanelBase
    {
        private Button register_btn;
        private Button close_btn;
        private Text code_txt;
        private InputField acc_field;
        private InputField pw_field;
        private InputField pwtwin_field;

        private MessageDistribution _msgDistri = MessageDistribution.GetInstance();

        private void Awake()
        {
            _msgDistri.AddOnceListenner(NamesOfProtocol.Register, OnRegisterBack);
        }

        public override void Init(params object[] args)
        {
            base.Init(args);
            SkinPath = "RegisterPanel";
            Layer = PanelLayer.Panels;
        }

        public override void OnShowing()
        {
            base.OnShowing();
            Transform skinTrans = Skin.transform;
            register_btn = skinTrans.Find("info_panel/ok_btn").GetComponent<Button>();
            close_btn = skinTrans.Find("close_btn").GetComponent<Button>();
            code_txt = skinTrans.Find("info_panel/codeshow_txt").GetComponent<Text>();
            acc_field = skinTrans.Find("info_panel/account_inputfield").GetComponent<InputField>();
            pw_field = skinTrans.Find("info_panel/password_inputfield").GetComponent<InputField>();
            pwtwin_field = skinTrans.Find("info_panel/checkpw_inputfield").GetComponent<InputField>();
        }

        public override void OnShowed()
        {
            base.OnShowed();
            register_btn.onClick.AddListener(OnRegisterClick);
            close_btn.onClick.AddListener(OnCloseClick);
        }


        private void OnRegisterClick()
        {
            if (string.IsNullOrEmpty(acc_field.text) || string.IsNullOrEmpty(pw_field.text) || string.IsNullOrEmpty(pwtwin_field.text))
            {
                Debug.Log("[Warn] input nothing.");
                object[] obj = new object[2];
                obj[0] = "错误提示";
                obj[1] = "账号或密码空，请输入您的信息。";
                PanelManager._instance.OpenPanel<MentionPanel>("", obj);
                return;
            }
            if (pw_field.text != pwtwin_field.text)
            {
                Debug.Log("[Warn] twice password is not the same.");
                object[] obj = new object[2];
                obj[0] = "错误提示";
                obj[1] = "你输入的两次密码不符合，请检查后重试。";
                PanelManager._instance.OpenPanel<MentionPanel>("", obj);
                return;
            }

            if (NetworkManager.ConnClient.status != NetworkStatus.Connected)
            {
                string host = "127.0.0.1";
                int port = 1234;
                NetworkManager.ConnClient.Connect(host, port);
            }
            ProtocolByte proto = new ProtocolByte();
            proto.AddInfo<string>(NamesOfProtocol.Register);
            proto.AddInfo<string>(acc_field.text);
            proto.AddInfo<string>(pw_field.text);
            NetworkManager.ConnClient.Send(proto);
        }

        private void OnRegisterBack(ProtocolBase protocol)
        {
            ProtocolByte proto = protocol as ProtocolByte;
            string num = proto.GetString(1);
            string code = proto.GetString(2);
            if (num == "1")
            {
                Debug.Log("注册成功--game start");
                code_txt.text = code;
                object[] obj = new object[2];
                obj[0] = "重要提示";
                obj[1] = string.Format("注册账号成功！你的口令是：[{0}]   请牢记你的密码，此口令用于修改密码操作。", code);
                PanelManager._instance.OpenPanel<MentionPanel>("", obj);
            }
            else
            {
                Debug.Log("注册失败--password is error");
                object[] obj = new object[2];
                obj[0] = "错误提示";
                obj[1] = "您输入的账号已经注册,请返回到登录界面进行登录。";
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
