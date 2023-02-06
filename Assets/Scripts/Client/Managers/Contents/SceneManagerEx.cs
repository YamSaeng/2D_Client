using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public BaseScene CurrentScene
    {
        get
        {
            return GameObject.FindObjectOfType<BaseScene>();
        }
    }

    public void LoadScene(Define.en_Scene SceneType)
    {
        Managers.Clear();        
        SceneManager.LoadScene(GetSceneName(SceneType));
    }

    string GetSceneName(Define.en_Scene SceneType)
    {
        string SceneName = System.Enum.GetName(typeof(Define.en_Scene), SceneType);
        return SceneName;
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }
}
