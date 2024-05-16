using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    // 원하는 컴포넌트를 가져오는 기능
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();

        if (component == null)
            component = go.AddComponent<T>();

        return component;
    }

    // 특정 게임오브젝트를 찾는 기능
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);

        if (transform == null)
            return null;

        return transform.gameObject;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;
        if (recursive == false)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {

                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }
        return null;
    }

    public static int CheckString(string str)
    {
        int count = 0;

        foreach(var c in str)
        {
            // 특수문자 판별
            if (!char.IsLetterOrDigit(c))
            {
                Console.WriteLine("에러: 특수문자가 포함되어 있습니다.");
                return -1;
            }

            // 영어 알파벳 또는 숫자일 경우
            if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || char.IsDigit(c))
            {
                count += 1;
            }
            // 한글 문자 판별 (가: 0xAC00, 힣: 0xD7A3)
            else if (c >= '가' && c <= '힣')
            {
                count += 2;
            }
        }
        return count;
    }
}
