using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class _ControlSky : MonoBehaviour
{
    Time _day;
    Time _night;
    public Material dayMat;
    public Material nightMat;
    public GameObject dayLight;
    public GameObject nightLight;
    public Color dayFog;
    public Color nightFog;


    void Awake()
    {
        //SceneManager.LoadScene(1);

    }

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 0.8f);
    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(5, 5, 80, 20), "Day"))
        {
            RenderSettings.skybox = dayMat;
            RenderSettings.fogColor = dayFog;
            dayLight.SetActive(true);
            nightLight.SetActive(false);
        }

        if (GUI.Button(new Rect(5, 35, 80, 20), "Night"))
        {
            RenderSettings.skybox = nightMat;
            RenderSettings.fogColor = nightFog;
            dayLight.SetActive(false);
            nightLight.SetActive(true);
        }
    }
}
