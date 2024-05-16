using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    // ¿øÇÏ´Â ÄÄÆ÷³ÍÆ®¸¦ °¡Á®¿À´Â ±â´É
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();

        if (component == null)
            component = go.AddComponent<T>();

        return component;
    }

    // Æ¯Á¤ °ÔÀÓ¿ÀºêÁ§Æ®¸¦ Ã£´Â ±â´É
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
            // Æ¯¼ö¹®ÀÚ ÆÇº°
            if (!char.IsLetterOrDigit(c))
            {
                Console.WriteLine("¿¡·¯: Æ¯¼ö¹®ÀÚ°¡ Æ÷ÇÔµÇ¾î ÀÖ½À´Ï´Ù.");
                return -1;
            }

            // ¿µ¾î ¾ËÆÄºª ¶Ç´Â ¼ýÀÚÀÏ °æ¿ì
            if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || char.IsDigit(c))
            {
                count += 1;
            }
            // ÇÑ±Û ¹®ÀÚ ÆÇº° (°¡: 0xAC00, ÆR: 0xD7A3)
            else if (c >= '°¡' && c <= 'ÆR')
            {
                count += 2;
            }
        }
        return count;
    }
}
