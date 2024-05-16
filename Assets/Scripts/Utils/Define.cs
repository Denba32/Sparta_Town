using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum UIEvent
    {
        Click,
        Drag,
        PointerDown,
        PointerUp,

    }

    public enum StatsChangeType
    {
        Add, // 0
        Multiple, // 1
        Override // 2
    }

    public enum UIState
    {
        CharacterEdit,
        Menu,
        Inventory,
    }

    public enum SceneState
    {
        Initialize,
        Play,
        Pause,
    }
}
