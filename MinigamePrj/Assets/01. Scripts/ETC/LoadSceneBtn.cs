using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneBtn : MonoBehaviour
{
    public void SceneLoading(string _sceneName)
    {
        LoadingScene.sceneName = _sceneName;
        LoadingScene.LoadScene();
    }
}
