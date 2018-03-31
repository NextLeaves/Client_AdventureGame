/*
	Project : 	

	Author : NextLeaves

	Expression:

	Date:

	Repaird Record:

*/

using Assets.Scripts.Fantasy.Networks;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Fantasy.ObjectClass
{
    [Serializable]
    public class MyPlayer
    {
        public static MyPlayer _instance = new MyPlayer();

        public int Coin { get; set; }
        public int Money { get; set; }
        public int Star { get; set; }
        public int Diamand { get; set; }

        private MyPlayer()
        {
            Coin = 100;
            Money = 100;
            Star = 100;
            Diamand = 100;
        }

        public static MyPlayer GetInstance()
        {
            return _instance;
        }        


    }
}


