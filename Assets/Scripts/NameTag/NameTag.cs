using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class NameTag : MonoBehaviour
{
    private TextMeshProUGUI txt_Nickname;
    private void Start()
    {
        txt_Nickname = GetComponentInChildren<TextMeshProUGUI>();   
    }

    private void OnEnable()
    {
        Managers.Event.onSelectNickname += SetNickname;
    }

    private void OnDisable()
    {
        Managers.Event.onSelectNickname -= SetNickname;
    }

    private void SetNickname(string nick)
    {
        txt_Nickname.text = nick;
    }
}
