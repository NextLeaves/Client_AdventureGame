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

public class DontDestroy : MonoBehaviour
{
    public List<GameObject> objs;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        if (objs != null)
            foreach (var item in objs)
            {
                DontDestroyOnLoad(item);
            }
    }

    void Update()
    {

    }
}
