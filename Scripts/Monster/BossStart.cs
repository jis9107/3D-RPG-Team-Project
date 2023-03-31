using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStart : MonoBehaviour
{
    [SerializeField]
    GameObject bossHPBar;
    [SerializeField]
    GameObject bossCall;
    [SerializeField]
    GameObject stageControll;

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            bossHPBar.SetActive(true);
            bossCall.SetActive(true);
            stageControll.SetActive(true);
        }
    }
}
