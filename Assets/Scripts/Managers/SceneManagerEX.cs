using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneManagerEX : MonoBehaviour
{
    public BaseScene CurrentScene { get; private set; }

    public void Init(Transform root)
    {
        SceneManagerEX go = Util.FindChild<SceneManagerEX>(root.gameObject);

        if(go == null)
        {
            GameObject obj = new GameObject { name = "@SceneManager" };
            obj.AddComponent<SceneManagerEX>();
            go = obj.GetComponent<SceneManagerEX>();
        }
        go.transform.SetParent(root.transform);
    }


    public void InitializeScene(BaseScene scene)
    {
        if(CurrentScene == null)
        {
            CurrentScene = scene;
            CurrentScene.Enter();

            CurrentScene.ChangeScene(Define.SceneState.Initialize);

        }
    }

    public void ChangeState(Define.SceneState state)
    {
        CurrentScene.ChangeScene(state);
    }
    public void ChangeScene(BaseScene nextScene)
    {
        CurrentScene.Exit();
        CurrentScene = nextScene;
        CurrentScene.Enter();

        CurrentScene.ChangeScene(Define.SceneState.Initialize);
    }


    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadScene(int id)
    {
        SceneManager.LoadScene(id);
    }

    public IEnumerator LoadSceneAsync(int id)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(id);
        while (!operation.isDone)
        {
            yield return null;
            Debug.Log(operation.progress);
        }
    }

    public IEnumerator LoadSceneAsync(string name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        while(!operation.isDone)
        {
            yield return null;
            Debug.Log(operation.progress);
        }
    }
}