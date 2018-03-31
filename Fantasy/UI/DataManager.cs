
using UnityEngine;
using UnityEngine.UI;

using Assets.Scripts.Fantasy.ObjectClass;

namespace Assets.Scripts.Fantasy.UI
{
    public class DataManager : MonoBehaviour
    {        
        public Transform HeadSpace;

        private Text diamand_txt;
        private Text star_txt;
        private Text money_txt;
        private Text coin_txt;

        private MyPlayer playerData = MyPlayer.GetInstance();

        private void Awake()
        {
            if (HeadSpace != null)
            {
                diamand_txt = HeadSpace.Find("GemPanel/value_txt").GetComponent<Text>();
                star_txt = HeadSpace.Find("StarPanel/value_txt").GetComponent<Text>();
                money_txt = HeadSpace.Find("MoneyPanel/value_txt").GetComponent<Text>();
                coin_txt = HeadSpace.Find("CoinPanel/value_txt").GetComponent<Text>();

                if (playerData != null)
                {
                    diamand_txt.text = playerData.Diamand.ToString();
                    star_txt.text = playerData.Star.ToString();
                    money_txt.text = playerData.Money.ToString();
                    coin_txt.text = playerData.Star.ToString();
                }
            }
        }

        private void Start()
        {

        }

        private void Update()
        {
            if (playerData != null)
            {
                diamand_txt.text = playerData.Diamand.ToString();
                star_txt.text = playerData.Star.ToString();
                money_txt.text = playerData.Money.ToString();
                coin_txt.text = playerData.Coin.ToString();
            }
        }

        
    }
}
