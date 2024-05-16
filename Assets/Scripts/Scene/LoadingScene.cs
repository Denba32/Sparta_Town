using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScene : BaseScene
{
    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        Managers.UI.ShowSceneUI<LoadingUI>();
    }

    public override void Exit()
    {
        base.Exit();
    }

}
