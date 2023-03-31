using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    [SerializeField]
    private float hp; //최대 체력
    public float currentHp; //현재 체력

    //필요한 이미지
    [SerializeField]
    private Material hp_gauge;

    private float playerHP;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GaugeUpdate();
    }

    public void GaugeUpdate()
    {
        hp_gauge.SetFloat("_FillLevel", (float)currentHp / hp);
    }
    public void IncreaseHP(int _count)
    {
        if (currentHp + _count < hp)
            currentHp += _count;
        else
            currentHp = hp;
    }

    public void DecreaseHP(int _count)
    {
        currentHp -= _count;

        //if (currentHp <= 0)
        //    Debug.Log("캐릭터 죽음");
    }

}
