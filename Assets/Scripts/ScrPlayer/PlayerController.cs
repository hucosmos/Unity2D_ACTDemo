using System;
using System.Collections;
using System.Collections.Generic;
using Bolt;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //必须的组件
    private Animator anim;
    private CharacterController cc;
    private Collider coll; //启用碰撞
    private PlayerInput playerInput;
    private SpriteRenderer spr;
    
    public float moveSpeed = 5.0f;
    public float jumpHeight = 1f; //跳跃高度
    private float gravityValue = -9.81f;
    private Vector2 jumpVelocity; //角色移动方向，判断跳跃
    private Vector2 moveDirection; //输入的方向

    [Header("地面碰撞检测")] public Transform GroundCheck; //地面检测
    public float CheckRadius = 0.2f;
    public bool isGround;
    public LayerMask LayerMask;

    void Start()
    {
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        coll = GetComponent<Collider>();
        spr = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        BasicMove();
        //Jump();
        AnimChange();
        FlipSprite();

        //地面检测
        if (cc.isGrounded)
        {
            Debug.Log("Player is grounded");
        }

        //开启碰撞
        

    }

    private void BasicMove() //基础移动
    {
        //获得键盘输入的方向
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        cc.Move(moveDirection * Time.deltaTime * moveSpeed);
        //地面碰撞检测
        isGround = Physics.CheckSphere(GroundCheck.position, CheckRadius, LayerMask);
        if (isGround && jumpVelocity.y < 0)
            gravityValue = 0;
    }

    private void Jump() //跳跃
    {
        if (isGround && Input.GetButtonDown("Jump"))
            jumpVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        jumpVelocity.y += gravityValue * Time.deltaTime;
        cc.Move(jumpVelocity * Time.deltaTime);
    }

    private void AnimChange()
    {
        if (transform.position.x > 0)
        {
            anim.SetBool("isRun", true);
        }
        else
        {
            anim.SetBool("isRun", false);
        }
    }

    //根据输入方向，翻转角色
    private void FlipSprite()
    {
        if (moveDirection.x > 0)
            spr.flipX = true;
        else if (moveDirection.x < 0)
            spr.flipX = false;
    }
    

}
