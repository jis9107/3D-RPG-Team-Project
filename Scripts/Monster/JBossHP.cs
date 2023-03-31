using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JBossHP : MonoBehaviour
{
    int bossHP;
    [SerializeField]
    Image bossHPBar;
    [SerializeField]
    Image bossHPBar_back;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HPUpdate();
    }
    
    void HPUpdate()
    {
        bossHP = GetComponent<JBoss>().curHealth;
        bossHPBar.fillAmount = bossHP/100f;
    }
}
