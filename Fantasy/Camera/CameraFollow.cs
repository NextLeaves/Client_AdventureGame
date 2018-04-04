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

namespace Assets.Scripts.Fantasy
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;

        public float Distance_forward;
        public float Distance_up;
        private Vector3 pos = Vector3.zero;
        public float smooth = 0.8f;

        public Transform[] transPos;
        private int order = 0;

        void Start()
        {
            StartCoroutine(GetPlayerPos());
        }

        void Update()
        {
        }
        
        void LateUpdate()
        {
            if (target == null) return;
            LookAtTarget();
            Zoom();
        }

        IEnumerator GetPlayerPos()
        {
            yield return new WaitForSeconds(1.0f);

            target = GameObject.FindWithTag(Tag.Player).GetComponent<Transform>();
        }

        void LookAtTarget()
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                if (order > 2) order = -1;
                order++;
            }

            switch (order)
            {
                case 0:
                    smooth = 0.8f;
                    break;
                case 1:
                    Distance_forward = 2.5f;
                    Distance_up = 1.2f;
                    smooth = 1.5f;
                    break;
                case 2:
                    Distance_forward = -2.5f;
                    Distance_up = 1.2f;
                    smooth = 0.8f;
                    break;
            }
            pos = target.transform.position + Vector3.up * Distance_up - Distance_forward * target.forward;
            this.transform.position = Vector3.Lerp(this.transform.position, pos, smooth * Time.deltaTime);
            transform.LookAt(target);
        }

        void Zoom()
        {
            if (order == 2) return;
            float scollValue = Input.GetAxis("Mouse ScrollWheel");
            Distance_forward -= scollValue;
            Distance_up -= scollValue;
            Distance_forward = Mathf.Clamp(Distance_forward, 4.6f, 10.0f);
            Distance_up = Mathf.Clamp(Distance_up, 0.6f, 5.0f);
        }

        
    }

}

