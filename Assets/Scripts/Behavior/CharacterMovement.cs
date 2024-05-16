using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{
    #region ========== Component ==========

    private TopDownController controller;

    private Rigidbody2D rigid = null;

    #endregion


    private Vector2 moveDirection = Vector2.zero;

    public float moveSpeed = 10f;

    private bool canMove = false;

    private void Awake()
    {
        controller = GetComponent<TopDownController>();
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        controller.onMove += Move;
        Managers.Scene.CurrentScene.onSceneChange += OnControl;
    }

    private void OnControl(Define.SceneState state)
    {
        if(state == Define.SceneState.Play)
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }
    }

    private void FixedUpdate()
    {
        if(canMove)
            ApplyMovement(moveDirection);
    }

    private void ApplyMovement(Vector2 moveDirection)
    {
        rigid.velocity = moveDirection * moveSpeed * Time.fixedDeltaTime;
    }

    private void Move(Vector2 direction)
    {
        moveDirection = direction;
    }

    
}
