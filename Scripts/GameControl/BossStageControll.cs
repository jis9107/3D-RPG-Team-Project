using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossStageControll : MonoBehaviour
{

    [SerializeField]
    Image bosshpBar;
    [SerializeField]
    GameObject winMassage;
    [SerializeField]
    GameObject hpBar;
    [SerializeField]
    GameObject moveSpot;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(bosshpBar.fillAmount == 0)
        {
            hpBar.SetActive(false);
            winMassage.SetActive(true);
            moveSpot.SetActive(true);
        }
    }
}
