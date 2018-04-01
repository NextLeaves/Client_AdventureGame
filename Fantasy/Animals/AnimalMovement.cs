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

namespace Assets.Scripts.Fantasy.Animals
{
    public class AnimalMovement : MonoBehaviour
    {
        public float SimpleSpeed = 2;
        public float CriticalSpeed = 5;
        private float _speed;
        private float _rotateSpeed = 40.0f;

        private Rigidbody rig;

        private void Awake()
        {
            if (rig == null) rig = GetComponent<Rigidbody>();
        }

        void Start()
        {

        }

        void Update()
        {
            SimpleMovement();
        }

        private void FixedUpdate()
        {
            Movement();
        }

        void SimpleMovement()
        {
            float h = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up, h * _rotateSpeed * Time.deltaTime);
        }

        void Movement()
        {
            if (rig == null) return;

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Vector3 moveDir = new Vector3(0f, 0f, v);
            moveDir = transform.TransformDirection(moveDir);
            _speed = Input.GetKeyDown(KeyCode.LeftShift) ? CriticalSpeed : SimpleSpeed;
            Debug.Log(_speed);
            rig.AddForce(moveDir * _speed, ForceMode.Impulse);

        }
    }

}

