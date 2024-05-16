using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class BaseScene : MonoBehaviour
{
    public event Action<Define.SceneState> onSceneChange;

    public float currentTime;


    public BaseScene()
    {

    }

    public virtual void DoChecks()
    {

    }
    public virtual void Enter()
    {
        DoChecks();
    }

    public virtual void Exit()
    {

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    public void ChangeScene(Define.SceneState state = Define.SceneState.Initialize)
    {
        onSceneChange?.Invoke(state);
    }

    protected virtual void OnChange(Define.SceneState state)
    {

    }
}
