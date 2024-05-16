using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D.Animation;


public class CharacterAnimationController : AnimationController
{
    private static readonly int isWalking = Animator.StringToHash("isWalking");

    private readonly float magnitudeThreshold = 0.5f;

    private SpriteLibrary characterLibrary;

    private bool canMove;

    protected override void Awake()
    {
        base.Awake();

        characterLibrary = GetComponentInChildren<SpriteLibrary>();
    }

    private void Start()
    {
        controller.onMove += Move;

        Managers.Event.onSelectCharacter += SetCharacterAsset;

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

    private void Move(Vector2 vector)
    {
        if(canMove)
            animator.SetBool(isWalking, vector.magnitude > magnitudeThreshold);
    }

    private void SetCharacterAsset(SpriteLibraryAsset asset)
    {
        characterLibrary.spriteLibraryAsset = asset;
    }
}
