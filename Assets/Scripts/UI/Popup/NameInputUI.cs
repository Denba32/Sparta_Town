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

        // TODO : ĳ���� ���� ����
    }

    public void ActiveCharacterSelector()
    {
        img_CharacterSelectBackground.gameObject.SetActive(!img_CharacterSelectBackground.gameObject.activeSelf);
    }

    // ������ �� �ִ� ĳ���͸� ����ִ� �뵵
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


    // ���� �Է�
    private void Join()
    {
        if(inputField.text == "")
        {
            txt_Erorr.text = "�г����� �Է����ּ���";
        }
        else
        {
            int count = Util.CheckString(inputField.text);
            if (count < 0)
            {
                txt_Erorr.text = "�г��ӿ� Ư�����ڸ� �����ϰ� �ֽ��ϴ�.";
            }
            else if(count > 16)
            {
                txt_Erorr.text = "��밡���� ���ڼ��� �Ѿ����ϴ�.";
            }
            else
            {
                txt_Erorr.text = "�г����� ��ϵǾ����ϴ�.";

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
