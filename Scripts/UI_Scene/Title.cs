using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    public static Title instance;

    private SaveLoad theSaveLoad;

    //[SerializeField]
    //Image progressBar;

    private void Awake()
    {

        theSaveLoad = FindObjectOfType<SaveLoad>();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void ClickStart()
    {
        LoadingManager.LoadScene("Village");
        Destroy(this);
    }

    public void ClickLoad()
    {
        StartCoroutine(LoadSceneProgress());

    }
    IEnumerator LoadSceneProgress()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync("Village");
        op.allowSceneActivation = false;

        //float timer = 0f;
        while (!op.isDone)
        {
            yield return null;

            //if (op.progress < 0.9f)
            //{
            //    progressBar.fillAmount = op.progress;
            //}
            //else
            //{
            //    timer += Time.unscaledDeltaTime;
            //    progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
            //    if (progressBar.fillAmount >= 1f)
            //    {
            //        op.allowSceneActivation = true;
            //        yield break;
            //    }
            //}
        }
        theSaveLoad = FindObjectOfType<SaveLoad>();
        theSaveLoad.LoadData();
        Destroy(gameObject);
    }
    //IEnumerator LoadCoroutine()
    //{
    //    AsyncOperation operation = SceneManager.LoadSceneAsync("Village");

    //    while (!operation.isDone)
    //    {
    //        yield return null;
    //    }
    //    theSaveLoad = FindObjectOfType<SaveLoad>();
    //    theSaveLoad.LoadData();
    //    Destroy(gameObject);
    //}

    public void ClickExit()
    {

    }
}
