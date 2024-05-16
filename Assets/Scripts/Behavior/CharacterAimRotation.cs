using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterAimRotation : MonoBehaviour
{
    private TopDownController controller;

    private SpriteRenderer characterRenderer;
    private bool canMove;

    private void Awake()
    {
        controller = GetComponent<TopDownController>();
        characterRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        controller.onLook += OnAim;

        Managers.Scene.CurrentScene.onSceneChange += OnControl;


    }

    private void OnControl(Define.SceneState state)
    {
        if (state == Define.SceneState.Play)
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }
    }

    private void OnAim(Vector2 direction)
    {
        if (canMove)
            RotateArm(direction);
    }

    private void RotateArm(Vector2 direction)
    {
        // 플레이어를 중심으로 마우스의 방향을 측정
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        characterRenderer.flipX = Mathf.Abs(rotZ) > 90f;

    }
}
