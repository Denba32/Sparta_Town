using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    private void Awake()
    {
        Managers.Scene.InitializeScene(this);
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        onSceneChange += OnChange;
        // TODO : 데이터 존재 여부 판단.
    }

    public override void Exit()
    {
        base.Exit();

        onSceneChange -= OnChange;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    protected override void OnChange(Define.SceneState state)
    {
        switch(state)
        {
            case Define.SceneState.Initialize:
                Managers.UI.ShowPopupUI<NameInputUI>();
                break;
            case Define.SceneState.Play:
                Managers.UI.CloseAllPopupUI();
                // 플레이어 온
                break;
            case Define.SceneState.Pause:

                break;

        }
    }
}
