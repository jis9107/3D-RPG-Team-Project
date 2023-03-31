using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoadingManager : MonoBehaviour
{
    static string nextScene;

    [SerializeField]
    Image progressBar;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("Loading");
    }

    void Start()
    {
        StartCoroutine(LoadSceneProgress());
    }

    IEnumerator LoadSceneProgress()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(2);
        op.allowSceneActivation = false;
        float timer = 0f;
        while(!op.isDone)
        {
            yield return null;

            //timer += Time.unscaledDeltaTime;
            //timer += Time.deltaTime;
            //if (op.progress < 0.9f)
            //{
            //    progressBar.fillAmount = op.progress;
            //}
            //else
            //{
            //    progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
            //    if(progressBar.fillAmount >= 1f)
            //    {
            //        op.allowSceneActivation = true;
            //        yield break;
            //    }
            //}

            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(op.progress, 1f, timer);
                if (progressBar.fillAmount >= op.progress)
                    timer = 0f;
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                if (progressBar.fillAmount >= 0.99f)
                {                  
                    op.allowSceneActivation = true;
                }
            }
        }        
    }

}
