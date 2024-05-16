using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class NameInputUI : PopupUI
{
    #region ========== Enums ==========
    enum TMPros
    {
        Txt_Alert,
        Txt_Name,
        Txt_Error
    }

    enum Buttons
    {
        Btn_Background,
        Btn_Join,
    }

    enum Images
    {
        Img_Profile,
        CharacterSelectBackground,
    }

    enum InputFields
    {
        InputField,
    }

    #endregion


    private TextMeshProUGUI txt_Name = null;
    private TextMeshProUGUI txt_Erorr = null;

    private Button btn_Join = null;
    private Button btn_Background = null;
    private Image img_Profile = null;
    private Image img_CharacterSelectBackground = null;

    private TMP_InputField inputField = null;

    [SerializeField]
    private SpriteLibraryAsset characterAsset = null;
    private string nickName = string.Empty;

    public override void Init()
    {
        base.Init();

        characterAsset = Managers.Data.FindCharacter("Penguin");

        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(TMPros));
        Bind<Button>(typeof(Buttons));
        Bind<TMP_InputField>(typeof(InputFields));

        txt_Name = GetTMPro((int)TMPros.Txt_Name);
        txt_Erorr = GetTMPro((int)TMPros.Txt_Error);

        btn_Join = GetButton((int)Buttons.Btn_Join);
        btn_Background = GetButton((int)Buttons.Btn_Background);

        btn_Join.onClick.AddListener(Join);
        btn_Background.onClick.AddListener(ActiveCharacterSelector);
        
        img_Profile = GetImage((int)Images.Img_Profile);
        img_CharacterSelectBackground = GetImage((int)Images.CharacterSelectBackground);

        inputField = GetTMInputField((int)InputFields.InputField);

        SetCharacterProfile();

        img_CharacterSelectBackground.gameObject.SetActive(false);
    }


    public override void ClosePopupUI()
    {
        base.ClosePopupUI();

        // TODO : 캐릭터 정보 삭제
    }

    public void ActiveCharacterSelector()
    {
        img_CharacterSelectBackground.gameObject.SetActive(!img_CharacterSelectBackground.gameObject.activeSelf);
    }

    // 선택할 수 있는 캐릭터를 띄워주는 용도
    private void SetCharacterProfile()
    {
        for (int i = 0; i < Managers.Data.GetCharacterCount(); i++)
        {
            GameObject go = new GameObject { name = Managers.Data.GetCharacterNames()[i] };
            go.GetOrAddComponent<RectTransform>().sizeDelta = new Vector2(250f, 280f);
            go.GetOrAddComponent<Image>().sprite = Managers.Data.FindCharacter(go.name).GetSprite("Idle", "Idle_0");
            go.GetOrAddComponent<Button>().onClick.AddListener(() =>
            {
                characterAsset = Managers.Data.FindCharacter(go.name);
                img_Profile.sprite = characterAsset.GetSprite("Idle", "Idle_0");
                ActiveCharacterSelector();
            });
            go.transform.SetParent(img_CharacterSelectBackground.transform);
        }
    }

    private void Clear()
    {
        characterAsset = null;
    }


    // 최종 입력
    private void Join()
    {
        if(inputField.text == "")
        {
            txt_Erorr.text = "닉네임을 입력해주세요";
        }
        else
        {
            int count = Util.CheckString(inputField.text);
            if (count < 0)
            {
                txt_Erorr.text = "닉네임에 특수문자를 포함하고 있습니다.";
            }
            else if(count > 16)
            {
                txt_Erorr.text = "허용가능한 글자수를 넘었습니다.";
            }
            else
            {
                txt_Erorr.text = "닉네임이 등록되었습니다.";

                nickName = inputField.text;
                Managers.Event.SelectNickname(nickName);
                Managers.Event.SelectCharacter(characterAsset);
                Managers.Scene.ChangeState(Define.SceneState.Play);
            }
            inputField.text = "";
            txt_Erorr.text = "";
        }
    }
}
