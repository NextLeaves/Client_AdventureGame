/*
	Project : 	

	Author : NextLeaves

	Expression:

	Date:

	Repaird Record:

*/


using System;
using UnityEngine;

namespace Assets.Scripts.Fantasy.ObjectClass
{

    [Serializable]
    public class MyPlayer 
    {
        public int Coin { get; set; }
        public int Money { get; set; }
        public int Star { get; set; }
        public int Diamand { get; set; }

        public MyPlayer() : this(100, 100, 100, 100)
        {

        }

        public MyPlayer(int coin, int money, int star) : this(coin, money, star, 100)
        {

        }

        public MyPlayer(int coin, int money, int star, int diamand)
        {
            Coin = coin;
            Money = money;
            Star = star;
            Diamand = diamand;
        }

        public void Init(int coin, int money, int star, int diamand)
        {
            Coin = coin;
            Money = money;
            Star = star;
            Diamand = diamand;
        }

    }
}


