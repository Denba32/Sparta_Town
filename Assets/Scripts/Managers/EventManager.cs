using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class EventManager : MonoBehaviour
{
    public event Action<SpriteLibraryAsset> onSelectCharacter;
    public event Action<string> onSelectNickname;

    public event Action onCreate;

    public void SelectCharacter(SpriteLibraryAsset asset)
    {
        onSelectCharacter?.Invoke(asset);
    }

    public void SelectNickname(string nick)
    {
        onSelectNickname?.Invoke(nick); 
    }
}
