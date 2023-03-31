using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController_1: MonoBehaviour
{
    public GameObject mGraphic; //그래픽설정
    public GameObject mSound; //소리설정
    public GameObject mShortcut; //단축키설정
    public GameObject mEffect; //효과설정


    //그래픽 버튼
    public void OnClickGraphic()
    {
        mGraphic.SetActive(true);
    }
    public void OnClickGraphicSave()
    {
        mGraphic.SetActive(false);
    }
    public void OnClickGraphicClose()
    {
        mGraphic.SetActive(false);
    }

    //소리 버튼
    public void OnClickSound()
    {
        mSound.SetActive(true);
    }
    public void OnClickSoundSave()
    {
        mSound.SetActive(false);
    }
    public void OnClickSoundClose()
    {
        mSound.SetActive(false);
    }

    //단축키 버튼
    public void OnClickShortcut()
    {
        mShortcut.SetActive(true);
    }
    public void OnClickShortcutSave()
    {
        mShortcut.SetActive(false);
    }
    public void OnClickShortcutClose()
    {
        mShortcut.SetActive(false);
    }

    //이펙트 버튼
    public void OnClickEffect()
    {
        mEffect.SetActive(true);
    }
    public void OnClickEffectSave()
    {
        mEffect.SetActive(false);
    }
    public void OnClickEffectClose()
    {
        mEffect.SetActive(false);
    }
}
