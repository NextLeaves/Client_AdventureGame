using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI.Panel
{
    public class LoadingPanel : PanelBase
    {
        private Slider loadingLevel_slider;
        private Text loading_txt;

        private void Awake()
        {
        }

        public override void Init(params object[] args)
        {
            base.Init(args);
            SkinPath = "LoadingPanel";
            Layer = PanelLayer.Hints;
        }

        public override void OnShowing()
        {
            //base.OnShowing();
            Transform skinTrans = Skin.transform;
            loadingLevel_slider = skinTrans.Find("loadinglevel_slider").GetComponent<Slider>();
            loading_txt = skinTrans.Find("loadinglevel_slider/Text").GetComponent<Text>();
        }

        public override void OnShowed()
        {
            //base.OnShowed();
            int index = Convert.ToInt32(Args[0]);
            LoadLevel(index);
        }

        private void LoadLevel(int sceneIndex)
        {
            StartCoroutine(LoadAsynrounsly(sceneIndex));
        }

        private IEnumerator LoadAsynrounsly(int index)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(index);

            while (!operation.isDone)
            {
                float progress = operation.progress;
                loadingLevel_slider.value = progress;
                loading_txt.text = progress * 100f + "%";
                yield return null;
            }
        }


    }
}
