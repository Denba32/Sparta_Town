using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceManager
{
    public T[] LoadAll<T>(string path) where T : Object
    {
        return Resources.LoadAll<T>(path);
    }
    public T Load<T>(string path) where T : Object
    {
        // ������Ʈ�� ���������� ���
        if (typeof(T) == typeof(GameObject))
        {
            string name = path;

            // path�� ��ġ�� Resources/Object/Coin.Prefab�� ���
            // '/'�� ��ġ�� �ľ� �� �ش� ������ ��ġ�� int�� ��ȯ
            int index = name.LastIndexOf('/');

            // ���� 0�� ���
            if (index >= 0)
                // �ش� ��ġ�� �� ĭ �տ��� ���� name�� ����
                name = name.Substring(index + 1);

            // Ǯ�� ������Ʈ�� �� �����Ƿ� Ȯ��
            GameObject go = Managers.Pool.GetOriginal(name);

            if (go != null)
                return go as T;
        }

        return Resources.Load<T>(path);
    }

    // Load All

    // Instantiate
    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject original = Load<GameObject>($"Prefabs/{path}");

        if (original == null)
        {
            Debug.LogError($"Failed to load Prefab : {path}");
            return null;
        }

        if (original.GetComponent<Poolable>() != null)
        {
            Debug.Log($"������Ʈ ���� : {path}");
            return Managers.Pool.Pop(original, parent).gameObject;
        }

        GameObject go = Object.Instantiate(original, parent);
        go.name = original.name;

        return go;
    }

    // Destroy

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        Poolable poolable = go.GetComponent<Poolable>();
        if (poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }

        Object.Destroy(go);
    }
}
