using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Scene과 Popup의 Sorting을 다루는 변수
    int _order = 0;

    // Popup으 경우 Stack으로 다룬다.
    Stack<PopupUI> _popupStack = new Stack<PopupUI>();

    SceneUI _sceneUI = null;

    private GameObject _root;
    public GameObject Root
    {
        get
        {
            if(_root == null)
            {
                GameObject root = GameObject.Find("----------[UI]");
                if (root == null)
                    _root = new GameObject { name = "----------[UI]" };

                _root = root;
            }
            return _root;
        }
    }

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;


        if (sort)
        {
            _order++;
            canvas.sortingOrder = _order;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public T ShowPopupUI<T>(string name = null) where T : PopupUI
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");
        T popup = Util.GetOrAddComponent<T>(go);
        _popupStack.Push(popup);

        go.transform.SetParent(Root.transform);

        return popup;
    }

    public T ShowSceneUI<T>(string name = null) where T : SceneUI
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}/{name}");

        T sceneUI = go.GetOrAddComponent<T>();
        _sceneUI = sceneUI;
        go.transform.SetParent(Root.transform);

        return sceneUI;
    }

    public void ClosePopupUI(PopupUI popup)
    {
        if (_popupStack.Count == 0)
            return;
        if (_popupStack.Peek() != popup)
        {
            if (_popupStack.Contains(popup))
            {
                for (int i = 0; i < _popupStack.Count; i++)
                {
                    _popupStack.Pop();
                }
            }
        }
        ClosePopupUI();
    }
    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;
        PopupUI popup = _popupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
        popup = null;
        _order--;
    }

    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }

    public void Clear()
    {
        CloseAllPopupUI();
        _sceneUI = null;
    }
}