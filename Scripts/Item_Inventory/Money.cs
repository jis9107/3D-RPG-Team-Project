using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public int money;
    private Text playerMoney;

    //[SerializeField]
    //GameObject monetMessage;
    //[SerializeField]
    //Transform messageBox;

    

    // Start is called before the first frame update
    void Start()
    {
        playerMoney = FindObjectOfType<Inventory>().moneyText;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            AddMoney();
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // 랜덤으로 돈 획득
    public void AddMoney()
    {
        int _money = Random.Range(100, 150);
        money = int.Parse(playerMoney.text) + _money;
        playerMoney.text = money.ToString();
        Debug.Log(_money.ToString() + "획득");
    }
}
