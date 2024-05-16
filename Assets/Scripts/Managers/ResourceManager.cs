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
        // 오브젝트를 가져오려는 경우
        if (typeof(T) == typeof(GameObject))
        {
            string name = path;

            // path의 위치가 Resources/Object/Coin.Prefab일 경우
            // '/'의 위치를 파악 후 해당 문자의 위치를 int로 반환
            int index = name.LastIndexOf('/');

            // 만약 0일 경우
            if (index >= 0)
                // 해당 위치의 한 칸 앞에서 부터 name을 삽입
                name = name.Substring(index + 1);

            // 풀링 오브젝트일 수 있으므로 확인
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
            Debug.Log($"오브젝트 생성 : {path}");
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
