/*
	Project : 	

	Author : NextLeaves

	Expression:

	Date:

	Repaird Record:

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.Fantasy.ObjectClass;

namespace Assets.Scripts.Fantasy.Props
{
    public enum PropCategory
    {
        None,
        Diamand,
        Star,
        Money,
        Coin
    }

    public class Prop : MonoBehaviour
    {
        private MyPlayer playerData = MyPlayer.GetInstance();

        public PropCategory _category = PropCategory.None;
        [Range(1, 10)]
        public int RewordRate = 1;
        public int PropReword = 10;    
        
        public void AddScore()
        {
            switch (_category)
            {
                case PropCategory.None:                    
                    break;                    
                case PropCategory.Coin:
                    playerData.Coin += PropReword * RewordRate;
                    break;                    
                case PropCategory.Diamand:
                    playerData.Diamand += PropReword * RewordRate;
                    break;
                case PropCategory.Star:
                    playerData.Star += PropReword * RewordRate;
                    break;
                case PropCategory.Money:
                    playerData.Money += PropReword * RewordRate;
                    break;
            }
        }
    }
}

