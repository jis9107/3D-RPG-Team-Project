using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    // 인벤토리 활성화
    //public static bool inventoryActivated = false;


    // 필요한 컴포넌트
    [SerializeField]
    public GameObject go_inventoryBase;
    [SerializeField]
    private GameObject go_SlotsParent;
    [SerializeField]
    private GameObject go_quickSlotParent;
    [SerializeField]
    private QuickSlot quickSlots;

    public Text moneyText;

    // 슬롯에 있는 아이템 저장
    public Slot[] GetSlots() { return slots; }

    // 아이템 정보를 받아올 아이템 배열
    [SerializeField]
    private Item[] items;


    //슬롯들
    private Slot[] slots;
    private Item item;

    public void LoadToInven(int _arrayNum, string _itemName, int _itemNum)
    {        
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].itemName == _itemName)
            {
                slots[_arrayNum].AddItem(items[i], _itemNum);
            }                   
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        GameControl _gameCtrl = FindObjectOfType<GameControl>();
        if (_gameCtrl._state == GameControl.State.Import)
        {
            _gameCtrl.theSaveLoad.LoadData();
        }
        if (_gameCtrl._state == GameControl.State.Dungeon)
        {
            _gameCtrl.theSaveLoad.LoadData();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // I버튼이 눌렸을 때 인벤토리 활성화
    //private void TryOpenInventory()
    //{
    //    if (Input.GetKeyDown(KeyCode.I))
    //    {
    //        inventoryActivated = !inventoryActivated; // ture면 false로 false면 true로

    //        if (inventoryActivated)
    //        {
    //            OpenInventory();
    //        }
    //        else
    //            CloseInventory();
    //    }
    //}

    private void OpenInventory()
    {
        go_inventoryBase.SetActive(true);
    }

    private void CloseInventory()
    {
        go_inventoryBase.SetActive(false);
    }

    // 아이템 획득
    public void AcquireItem(Item _item, int _count = 1)
    {
        if(Item.ItemType.Equipment != _item.itemType)
        {
            for (int i = 0; i < quickSlots.useSlots.Length; i++)
            {
                if(quickSlots.useSlots[i].item != null)
                {
                    if (quickSlots.useSlots[i].item.itemName == _item.itemName)
                    {
                        quickSlots.useSlots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
            // 슬롯의 갯수만큼 반복시켜 이미 아이템이 존재한다면 카운트만 늘려준다
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }
        // 슬롯 빈자리가 있으면 아이템을 가져온다
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }

}