using System;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public event Action<Vector2> onMove;
    public event Action<Vector2> onLook;

    protected virtual void Awake()
    {
        
    }

    public void CallMoveEvent(Vector2 direction)
    {
        onMove?.Invoke(direction);
    }

    public void CallLookEvent(Vector2 direction)
    {
        onLook?.Invoke(direction);
    }
}
