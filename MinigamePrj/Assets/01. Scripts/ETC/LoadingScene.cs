using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField]
    private Image loadingBar;

    public static string sceneName;

    void Start()
    {
        loadingBar.fillAmount = 0f;
        StartCoroutine(LoadAsyncScene());
    }

    public static void LoadScene()
    {
        SceneManager.LoadScene("--. Loading");
    }

    private IEnumerator LoadAsyncScene()
    {
        yield return null;

        float lTime = 0;

        AsyncOperation asyncScene = SceneManager.LoadSceneAsync(sceneName);
        asyncScene.allowSceneActivation = false;
        
        while(!asyncScene.isDone)
        {
            yield return null;

            lTime += Time.deltaTime;

            if(asyncScene.progress >= 0.9f)
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount * 0.0001f, 1, lTime);

                if(loadingBar.fillAmount == 1.0f)
                {
                    asyncScene.allowSceneActivation = true;
                }
            }
            else
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount * 0.0001f, asyncScene.progress, lTime);

                if(loadingBar.fillAmount >= asyncScene.progress)
                {
                    lTime = 0f;
                }
            }
        }     
    }
}
