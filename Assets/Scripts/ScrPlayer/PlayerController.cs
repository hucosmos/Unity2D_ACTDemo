using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private CharacterController cc;
    public float moveSpeed = 5.0f;
    public float jumpHeight = 1f;   //跳跃高度
    private float gravityValue = -9.81f;
    private Vector2 jumpVelocity; //角色移动方向，判断跳跃
    private Vector2 moveDirection; //输入的方向
    public bool isGround;
    void Start()
    {
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }


    private void Update()
    {
        BasicMove();
        //Jump();
        AnimChange();
        
        //地面检测
        if (cc.isGrounded)
        {
            print("Player is grounded");
        }
    }
    private void FixedUpdate()
    {
        
    }

    private void BasicMove()    //基础移动
    {
        //获得键盘输入的方向
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        cc.Move(moveDirection * Time.deltaTime * moveSpeed);
    }

    private void Jump()     //跳跃
    {
        if (Input.GetButtonDown("Jump"))
            jumpVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        jumpVelocity.y += gravityValue * Time.deltaTime;
        cc.Move(jumpVelocity * Time.deltaTime);
    }

    private void AnimChange()
    {
        
    }
}
