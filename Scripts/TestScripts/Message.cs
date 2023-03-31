using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    [SerializeField]
    Money coin;
    
    Text moneyMessage;

    int money;
    // Start is called before the first frame update
    void Start()
    {
        money = coin.money;
        moneyMessage = GetComponent<Text>();
        moneyMessage.text = money.ToString() + "을 획득하였습니다";
        Destroy(this.gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
