using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SceneManagerRe : MonoBehaviour
{
    public enum State
    {
        Start,
        Village,
        Dungeon_Day
    }
    public State _state;

    [SerializeField]
    Image progressBar; //loading scene의 진행률을 보여주는 바

    static string nextScene;

    // pause
    public GameObject pBackToSavePoint; //세이브포인트로 이동
    public GameObject pExit; //게임종료

    // scene이동
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("Loading");
    }

    // scene fade in & out
    public Image FadePanel;
    float time = 0f;
    float F_time = 1f;

    // scene : start to village
    bool isLoad;
    float interval;
    public Animator _anim;

    // sound
    public AudioSource BackgroundSource;


    void Awake()
    {
        //Cursor.visible = false;

        if (SceneManager.GetActiveScene().name.Contains("Start"))
        {
            _state = State.Start;
        }

        if (SceneManager.GetActiveScene().name.Contains("Village"))
        {
            _state = State.Village;
        }

        else if (SceneManager.GetActiveScene().name.Contains("Dungeon"))
        {
            _state = State.Dungeon_Day;
        }
    }

    void Start()
    {
        if (_state == State.Start)
        {
            Cursor.visible = true;
            StartCoroutine(LoadSceneProgress());
        }

        if (_state == State.Village)
        {
            Cursor.visible = false;
        }

        if (_state == State.Dungeon_Day)
        {
            Cursor.visible = false;
        }
    }

    IEnumerator LoadSceneProgress()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0f;
        while (!op.isDone)
        {
            yield return null;

            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if (progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }


    void Update()
    {
        if (_state == State.Start)
        {
            if ( isLoad )
            {
                interval += Time.deltaTime;
                if (interval > 0.5f)
                {
                    SceneManager.LoadScene("Village");
                }
            }
        }
        
    }

    public void LoadScene()
    {
        isLoad = true;
        _anim = transform.parent.GetComponent<Animator>();
        _anim.SetTrigger("enterStartButton");
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("Exit");
    }

    void OnApplicationQuit()
    {
        Application.Quit();
    }

    // 세이브포인트 돌아가기 버튼
    public void OnClickBackToSavePoint()
    {
        pBackToSavePoint.SetActive(true);
    }
    public void OnClickBackToSavePointYes()
    {
        pBackToSavePoint.SetActive(false);
    }
    public void OnClickBackToSavePointNo()
    {
        pBackToSavePoint.SetActive(false);
    }

    // 종료 버튼
    public void OnClickExit()
    {
        pExit.SetActive(true);
    }
    public void OnClickExitYes()
    {
        pExit.SetActive(false);
    }
    public void OnClickExitNo()
    {
        pExit.SetActive(false);
    }

    // Fade in & out
    public void Fade()
    {
        StartCoroutine(FadeFlow());
    }

    IEnumerator FadeFlow()
    {
        FadePanel.gameObject.SetActive(true);
        time = 0f;
        Color alpha = FadePanel.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            FadePanel.color = alpha;
            yield return null;
        }
        time = 0f;

        yield return new WaitForSeconds(0.2f);

        while (alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            FadePanel.color = alpha;
            yield return null;
        }
        FadePanel.gameObject.SetActive(false);
        yield return null;
    }

    // sound
    public void SetMusicVolume(float volume)
    {
        BackgroundSource.volume = volume;
    }
}
