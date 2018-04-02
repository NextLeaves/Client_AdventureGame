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
using UnityEngine.UI;

using rand = UnityEngine.Random;

public class PlayerMovement : MonoBehaviour
{
    private float _speed = 100.0f;
    public float _speedMax = 300.0f;
    public float _speedSimple = 100.0f;
    private Slider ennergy_slider;
    private float ennergyRateUp = 0.01f;
    private float ennergyRateDown = 0.02f;

    #region DelayTime
    private float nowTime;
    private float delayTime = 1.0f;
    #endregion

    private CharacterController cc;
    private Animator anim;
    private float rotateSpeed = 100.0f;
    private float RotateDampTime = 1.0f;

    private void Awake()
    {
        nowTime = Time.time;
        rand.InitState(DateTime.Now.Second);
    }

    void Start()
    {
        if (cc == null)
            cc = GetComponent<CharacterController>();
        if (anim == null)
            anim = GetComponent<Animator>();
        if (ennergy_slider == null)
            ennergy_slider = GameObject.Find("Canvas/InfoPanels/PlayerInfoPanel/energe_slider").GetComponent<Slider>();
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
        if (Input.GetKeyDown(KeyCode.Space))
            anim.SetTrigger("Jump");
        cc.SimpleMove(movePos * _speed * Time.deltaTime);
        this.transform.Rotate(0f, h * rotateSpeed * RotateDampTime * Time.deltaTime, 0f);

        OnEnnergy();
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

    void OnEnnergy()
    {
        if (Input.GetKey(KeyCode.LeftShift) && ennergy_slider.value > 0)
        {
            _speed = _speedMax;
            if (Time.time - nowTime > delayTime)
            {
                ennergy_slider.value -= ennergyRateDown;
                nowTime = Time.time;
            }

        }
        else
        {
            _speed = _speedSimple;
            if (Time.time - nowTime > delayTime)
            {
                ennergy_slider.value += ennergyRateUp;
                nowTime = Time.time;
            }
        }
    }

    void OnBlood()
    {

    }

    void OnShield()
    {

    }
}
