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

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float Distance_forward;
    public float Distance_up;
    private Vector3 pos = Vector3.zero;
    public float smooth = 0.8f;

    private bool isFace = false;



    void Start()
    {

    }

    void Update()
    {

        LookAtTarget();
        Zoom();
    }

    void LookAtTarget()
    {
        if (target == null) return;
        if (Input.GetKeyDown(KeyCode.V)) isFace = !isFace;

        if (isFace)
        {
            float horValue = Input.GetAxis("Mouse X");
            pos = target.GetChild(2).GetComponent<Transform>().position;
        }
        else
        {
            pos = target.transform.position + Vector3.up * Distance_up - Distance_forward * target.forward;
        }

        this.transform.position = Vector3.Lerp(this.transform.position, pos, smooth * Time.deltaTime);
        transform.LookAt(target);
    }

    void Zoom()
    {
        float scollValue = Input.GetAxis("Mouse ScrollWheel");
        Distance_forward -= scollValue;
        Distance_up -= scollValue;
        Distance_forward = Mathf.Clamp(Distance_forward, 4.6f, 10.0f);
        Distance_up = Mathf.Clamp(Distance_up, 0.6f, 5.0f);
    }
}
