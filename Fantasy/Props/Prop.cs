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
using Assets.Scripts.Fantasy;

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
        private MyPlayer playerData = Networks.DataManager.GetInstance().playerData;

        public PropCategory _category = PropCategory.None;
        [Range(1, 10)]
        public int RewordRate = 1;
        public int PropReword = 10;

        private float rotateSpeed = 20.0f;

        private void Update()
        {
            RotatingSelf();
        }

        public void AddScore()
        {
            switch (_category)
            {
                case PropCategory.None:
                    Debug.Log("prop type is not right.");
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
            playerData.UpLoad();
        }

        public void RotatingSelf()
        {
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime, Space.World);
        }
    }
}

