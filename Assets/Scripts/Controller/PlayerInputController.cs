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

    #region ========== Callback �Լ� ==========

    // �����ӿ� ���� ���� ��ó��
    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }

    // ȭ��󿡼� ���콺�� �÷��̾� ĳ������ ��� ���⿡ �ִ��� �˾Ƴ��� �ڵ�
    public void OnLook(InputValue value)
    {
        Vector2 newAim = value.Get<Vector2>();
        Vector2 worldPos = camera.ScreenToWorldPoint(newAim);
        newAim = (worldPos - (Vector2)transform.position).normalized;

        CallLookEvent(newAim);
    }


    #endregion
}
