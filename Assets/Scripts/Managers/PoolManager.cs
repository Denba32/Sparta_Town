using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    class Pool
    {
        // Pool에 쌓을 오리지널 오브젝트
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }

        // 풀링할 오브젝트를 저장할 공간
        public Stack<Poolable> _poolStack = new Stack<Poolable>();

        // 초기화 작업 : 어떤 오브젝트를 풀링할 것인지, 그 초기 수량은 몇기인지
        public void Init(GameObject original, int count = 5)
        {
            Original = original;
            // 새로운 오브젝트를 생성하여 해당 오브젝트를 Root로 취급
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
            // Original 오브젝트를 GameObject로서 생성
            GameObject go = Object.Instantiate<GameObject>(Original);
            go.name = Original.name;

            return go.GetOrAddComponent<Poolable>();
        }

        // 미사용의 풀링 오브젝트를 집어 넣는 기능
        public void Push(Poolable poolable)
        {
            if (poolable == null)
                return;

            // 풀링 오브젝트를 Pool Root의 자식 오브젝트로 둔다.
            poolable.transform.parent = Root;
            // 오브젝트 Active 비활성화
            poolable.gameObject.SetActive(false);
            // isUsing 을 False로 만들어 Pop시 이용 가능하게 만든다.
            poolable.isUsing = false;
            // 그 후 Stack에 넣는다.
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


    // Pool을 전체적으로 관리할 Dictionary
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
