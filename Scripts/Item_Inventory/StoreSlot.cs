using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreSlot : MonoBehaviour
{
    public Item item; // 상점 아이템.
    public Text itemName; // 아이템 이름
    public Image itemImage; // 상점 아이템의 이미지
    public Text itemPrice; // 상점 아이템의 가격
    private int money;

    private Inventory _inventory;
    
    //[SerializeField]
    //public Text _text;
    // Start is called before the first frame update
    // Update is called once per frame

    private void Start()
    {
        _inventory = FindObjectOfType<Inventory>();
    }
    void Update()
    {
        
    }

    public void OnStoreItemClick()
    {
        if (int.Parse(_inventory.moneyText.text) - int.Parse(itemPrice.text) >= 0)
        {
            money = int.Parse(_inventory.moneyText.text) - int.Parse(itemPrice.text);
            _inventory.moneyText.text = money.ToString();

            Debug.Log(item.itemName + "구입하였습니다.");
            _inventory.AcquireItem(item);
        }
        else
        {
            Debug.Log("돈이 부족합니다.");
        }
    }

}
