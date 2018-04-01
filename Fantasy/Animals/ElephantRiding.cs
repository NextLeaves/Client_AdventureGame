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

namespace Assets.Scripts.Fantasy.Animals
{
    public class ElephantRiding : MonoBehaviour
    {
        private CameraFollow _camera;
        private bool isRiding = false;

        private void Awake()
        {
            _camera = GameObject.Find("Main Camera").GetComponent<CameraFollow>();

        }

        void Start()
        {

        }

        void Update()
        {
            DropRiding();
        }

        IEnumerator ChangeIsRidingState()
        {
            yield return new WaitForSeconds(2.0f);
            isRiding = !isRiding;
        }

        void DropRiding()
        {
            if (Input.GetKeyDown(KeyCode.F) && isRiding)
            {
                Transform trans = this.transform.GetChild(this.transform.childCount - 1);
                trans.parent = null;
                trans.GetComponent<PlayerMovement>().enabled = true;
                trans.GetComponent<CharacterController>().enabled = true;

                trans.localPosition = this.transform.position + Vector3.right;
                trans.localRotation = Quaternion.identity;

                this.GetComponent<AnimalMovement>().enabled = false;
                this.GetComponent<ElephantMovement>().enabled = false;

                _camera.target = trans;

                StartCoroutine(ChangeIsRidingState());

            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.transform.tag == Tag.Player)
            {
                Transform trans = other.transform.GetComponentInParent<Transform>();

                if (Input.GetKeyDown(KeyCode.F) && !isRiding)
                {
                    trans.GetComponent<PlayerMovement>().enabled = false;
                    trans.GetComponent<CharacterController>().enabled = false;
                    trans.SetParent(this.transform);
                    trans.localPosition = Vector3.zero;
                    trans.localRotation = Quaternion.identity;

                    this.GetComponent<AnimalMovement>().enabled = true;
                    this.GetComponent<ElephantMovement>().enabled = true;

                    _camera.target = this.transform;

                    StartCoroutine(ChangeIsRidingState());
                }

            }
        }
    }
}


