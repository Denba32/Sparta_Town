using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers instance;

    private DataManager data = new DataManager();
    private DialogueManager dialogue = new DialogueManager();
    private PoolManager pool = new PoolManager();
    private QuestManager quest = new QuestManager();
    private SceneManagerEX scene = new SceneManagerEX();
    private EventManager _event = new EventManager();
    private ResourceManager resource = new ResourceManager();
    private UIManager ui = new UIManager();
    private SoundManager sound = new SoundManager();
    

    public static Managers Instance
    {
        get
        {
            Init();
            return instance;
        }
    }

    public static DataManager Data { get => Instance.data; }
    public static DialogueManager Dialogue { get => Instance.dialogue; }
    public static PoolManager Pool { get => Instance.pool; }
    public static QuestManager Quest { get => Instance.quest; }
    public static EventManager Event { get => Instance._event; }
    public static SceneManagerEX Scene { get => Instance.scene; }
    public static UIManager UI { get => Instance.ui; }
    public static ResourceManager Resource { get => Instance.resource; }    
    public static SoundManager Sound { get => Instance.sound; }
    private static void Init()
    {
        if (instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if(go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);

            instance = go.GetComponent<Managers>();

            instance.data.Init();
            instance.scene.Init(instance.transform);
            instance.pool.Init(instance.transform);
        }
    }

    private static void Clear()
    {
        if(instance != null)
        {

        }
    }
}
