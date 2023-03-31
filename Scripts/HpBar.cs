using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public GameObject Monster;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(Monster.transform.position + new Vector3(0, 1.8f, 0));
    }
    //몬스터 HpBar가 게임 시작 시 몬스터 머리 위로 이동하는 스크립트
}
