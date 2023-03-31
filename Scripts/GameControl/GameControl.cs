using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public static GameControl gameControl;

    public SaveLoad theSaveLoad;
    public enum State
    {
        Start,
        Import,
        Dungeon,
    }
    public State _state;
    void Awake()
    {
        _state = State.Start;
        theSaveLoad = GetComponent<SaveLoad>();

        if (gameControl == null)
        {
            gameControl = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClickStart()
    {
        _state = State.Start;
        SceneManager.LoadScene(1);
    }

    public void ClickLoad()
    {
        _state = State.Import;
        SceneManager.LoadScene(1);
    }

    //IEnumerator ImportSceneProgress()
    //{
    //    AsyncOperation operation = SceneManager.LoadSceneAsync("Village");
    //    //operation.allowSceneActivation = false;

    //    //float timer = 0f;
    //    while (!operation.isDone)
    //    {
    //        yield return null;

    //        //if (operation.progress < 0.9f)
    //        //{
    //        //    progressBar.fillAmount = operation.progress;
    //        //}
    //        //else
    //        //{
    //        //    timer += Time.unscaledDeltaTime;
    //        //    progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
    //        //    if (progressBar.fillAmount >= 1f)
    //        //    {
    //        //        operation.allowSceneActivation = true;
    //        //        yield break;
    //        //    }
    //        //}
    //    }
    //    theSaveLoad.LoadData();
    //    Destroy(gameObject);
    //}
}
