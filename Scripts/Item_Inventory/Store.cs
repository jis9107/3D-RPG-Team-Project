using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Store : MonoBehaviour
{

    public static bool storeActivated = false;

    [SerializeField]
    private GameObject go_storeBase;
    [SerializeField]
    private GameObject go_slotParent;
    [SerializeField]
    private GameObject go_inventoryBase;

    public Item[] storeItems;
    public StoreSlot[] storeSlots;



    void Start()
    {
        storeSlots = go_slotParent.GetComponentsInChildren<StoreSlot>();
        for (int i = 0; i < storeSlots.Length; i++)
        {
                storeSlots[i].item = storeItems[i];
                storeSlots[i].itemName.text = storeItems[i].itemName;
                storeSlots[i].itemImage.sprite = storeItems[i].itemimage;
                storeSlots[i].itemPrice.text = storeItems[i].itemPrice.ToString();
        }
    }
    void Update()
    {
        //TryOpenStore();
    }

    // I버튼이 눌렸을 때 인벤토리 활성화
    private void TryOpenStore()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            storeActivated = !storeActivated; // ture면 false로 false면 true로
            Cursor.visible = true;
            if (storeActivated)
            {
                OpenStore();
            }
            else
            {
                CloseStore();
            }
        }
    }

    public void OpenStore()
    {
        go_storeBase.SetActive(true);
        go_inventoryBase.SetActive(true);
    }

    public void CloseStore()
    {
        go_storeBase.SetActive(false);
        go_inventoryBase.SetActive(false);
    }
}
