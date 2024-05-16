using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    class Pool
    {
        // Pool�� ���� �������� ������Ʈ
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }

        // Ǯ���� ������Ʈ�� ������ ����
        public Stack<Poolable> _poolStack = new Stack<Poolable>();

        // �ʱ�ȭ �۾� : � ������Ʈ�� Ǯ���� ������, �� �ʱ� ������ �������
        public void Init(GameObject original, int count = 5)
        {
            Original = original;
            // ���ο� ������Ʈ�� �����Ͽ� �ش� ������Ʈ�� Root�� ���
            Root = new GameObject().transform;
            Root.name = $"{Original.name}_Root";

            for (int i = 0; i < count; i++)
            {
                Push(Create());
            }
        }

        // Create
        Poolable Create()
        {
            // Original ������Ʈ�� GameObject�μ� ����
            GameObject go = Object.Instantiate<GameObject>(Original);
            go.name = Original.name;

            return go.GetOrAddComponent<Poolable>();
        }

        // �̻���� Ǯ�� ������Ʈ�� ���� �ִ� ���
        public void Push(Poolable poolable)
        {
            if (poolable == null)
                return;

            // Ǯ�� ������Ʈ�� Pool Root�� �ڽ� ������Ʈ�� �д�.
            poolable.transform.parent = Root;
            // ������Ʈ Active ��Ȱ��ȭ
            poolable.gameObject.SetActive(false);
            // isUsing �� False�� ����� Pop�� �̿� �����ϰ� �����.
            poolable.isUsing = false;
            // �� �� Stack�� �ִ´�.
            _poolStack.Push(poolable);
        }

        // Pop

        public Poolable Pop(Transform parent)
        {
            Poolable poolable;

            if (_poolStack.Count > 0)
                poolable = _poolStack.Pop();

            else
                poolable = Create();

            poolable.gameObject.SetActive(true);

            if (parent == null)
            {
                poolable.transform.parent = Managers.Scene.CurrentScene.transform;
            }

            poolable.transform.parent = parent;
            poolable.isUsing = true;

            return poolable;

        }
    }


    // Pool�� ��ü������ ������ Dictionary
    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();

    Transform _root;

    public void Init(Transform root)
    {
        if (_root == null)
        {
            _root = new GameObject { name = "@Pool_Root" }.transform;
            Object.DontDestroyOnLoad(root);
        }
    }

    public void CreatePool(GameObject original, int count = 5)
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.parent = _root;

        _pool.Add(original.name, pool);
    }

    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name;
        if (_pool.ContainsKey(name) == false)
        {
            GameObject.Destroy(poolable.gameObject);
            return;
        }

        _pool[name].Push(poolable);
    }

    public Poolable Pop(GameObject original, Transform parent = null)
    {
        if (_pool.ContainsKey(original.name) == false)
        {
            CreatePool(original);
        }
        return _pool[original.name].Pop(parent);
    }

    public GameObject GetOriginal(string name)
    {
        if (_pool.ContainsKey(name) == false)
            return null;

        return _pool[name].Original;
    }

    public void Clear()
    {
        foreach (Transform child in _root)
        {
            GameObject.Destroy(child.gameObject);
        }
        _pool.Clear();
    }
}
