using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : TopDownController
{
    private Camera camera;

    protected override void Awake()
    {
        base.Awake();
        camera = Camera.main;

    }

    #region ========== Callback 함수 ==========

    // 움직임에 대한 방향 전처리
    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }

    // 화면상에서 마우스가 플레이어 캐릭터의 어느 방향에 있는지 알아내는 코드
    public void OnLook(InputValue value)
    {
        Vector2 newAim = value.Get<Vector2>();
        Vector2 worldPos = camera.ScreenToWorldPoint(newAim);
        newAim = (worldPos - (Vector2)transform.position).normalized;

        CallLookEvent(newAim);
    }


    #endregion
}
