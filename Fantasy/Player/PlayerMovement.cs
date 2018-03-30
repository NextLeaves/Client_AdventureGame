/*
	Project : 	

	Author : NextLeaves

	Expression:

	Date:

	Repaird Record:

*/

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

using rand = UnityEngine.Random;

public class PlayerMovement : MonoBehaviour
{
    public float _speed = 100.0f;

    private CharacterController cc;
    private Animator anim;
    private float RotateDampTime = 1.0f;

    private void Awake()
    {
        rand.InitState(DateTime.Now.Second);
    }

    void Start()
    {
        if (cc == null)
            cc = GetComponent<CharacterController>();
        if (anim == null)
            anim = GetComponent<Animator>();
    }

    void Update()
    {
        Movement();
        AnimationTrigger();
    }

    void Movement()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        Vector3 movePos = new Vector3(0f, 0f, v);
        movePos = transform.TransformDirection(movePos);
        if (v != 0 || h != 0)
            anim.SetBool("Run", true);
        else anim.SetBool("Run", false);
        if (Input.GetKey(KeyCode.LeftShift))
            _speed = 130.0f;
        if (Input.GetKeyDown(KeyCode.Space))
            anim.SetTrigger("Jump");
        cc.SimpleMove(movePos * _speed * Time.deltaTime);
        this.transform.Rotate(0f, h, 0f);
    }

    void AnimationTrigger()
    {
        AnimatorStateInfo currentInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (!currentInfo.IsName("Attack1") && Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack1");
            if (Input.GetMouseButtonDown(0))
                anim.SetTrigger("Attack1_2");
        }
               


    }
}
