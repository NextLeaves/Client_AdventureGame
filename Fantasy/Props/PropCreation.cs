/*
	Project : 	

	Author : NextLeaves

	Expression:

	Date:

	Repaird Record:

*/

using Assets.Scripts.Fantasy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Rand = UnityEngine.Random;

public class PropCreation : MonoBehaviour
{
    private List<GameObject> props;

    private void Awake()
    {
        props = new List<GameObject>();
    }

    void Start()
    {
        if (props == null) this.enabled = false;
        Transform proptrans = GameObject.Find("SceneManagement/propManagement/propPrefab").transform;

        for (int i = 0; i < 11; i++)
        {
            props.Add(proptrans.GetChild(i).gameObject);
        }

        Rand.InitState(DateTime.Now.Second);

        CreateProps();
    }

    void Update()
    {

    }


    void CreateProps()
    {
        int kindsCount = Rand.Range(3, 6);
        int[] dirs = new int[2];
        for (int i = 0; i < dirs.Length; i++)
        {
            dirs[i] = Rand.Range(10, 20);
        }
        Vector3 pos = Vector3.zero;
        for (int i = 0; i < kindsCount; i++)
        {
            GameObject obj = Instantiate<GameObject>(props[Rand.Range(0, props.Count)]);
            pos.Set(Rand.insideUnitCircle.x * dirs[0], obj.transform.position.y, Rand.insideUnitCircle.y*dirs[1]);
            pos += this.transform.position;
            obj.transform.position = pos;
        }

    }
}
