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
using Assets.Scripts.Fantasy;

public class PlayerTechology : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        PickupProp();

    }

    /// <summary>
    /// 捡道具
    /// </summary>
    void PickupProp()
    {
        LayerMask layerMask = LayerMask.GetMask("Prop");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10.0f, layerMask))
        {
            if (hit.collider.gameObject.tag == Tag.Prop)
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
