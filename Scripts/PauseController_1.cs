using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController_1: MonoBehaviour
{
    public GameObject pBackToSavePoint; //세이브포인트로 이동
    public GameObject pExit; //게임종료


    //세이브포인트 돌아가기 버튼
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

    //종료 버튼
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
}

public class PauseBackground : MonoBehaviour
{
    public float timeScale;

    public void InactiveOption()
    {
        Time.timeScale = timeScale;
    }

    public void ActiveOption()
    {
        Time.timeScale = 1.0f;
    }
}