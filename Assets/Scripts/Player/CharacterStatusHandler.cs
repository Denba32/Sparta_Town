using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatusHandler : MonoBehaviour
{
    public CharacterStatus CurrentStatus { get; private set; }

    public List<CharacterStatus> statusModifiers = new List<CharacterStatus>();

    private void Awake()
    {
        UpdateCharacterStat();
    }

    private void Start()
    {
        Managers.Event.onSelectNickname += SetNickname;
    }
    private void UpdateCharacterStat()
    {
        CurrentStatus = new CharacterStatus();
        CurrentStatus.statsChangeType = Define.StatsChangeType.Override;
        CurrentStatus.maxHealth = 100;
    }
    
    private void SetNickname(string nick)
    {
        CurrentStatus.nickName = nick;
    }
}
