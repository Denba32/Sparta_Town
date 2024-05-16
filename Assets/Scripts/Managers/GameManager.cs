using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    public Transform Player { get; private set; }

    private void Awake()
    {
        Init();
    }
    private static void Init()
    {
        if (instance == null)
        {
            GameObject go = GameObject.Find("GameManager");
            if(go == null)
            {
                go = new GameObject { name = "GameManager" };
                go.AddComponent<GameManager>();
            }
            DontDestroyOnLoad(go);

            instance = go.GetComponent<GameManager>();

            Instance.Player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
}
