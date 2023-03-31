using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlot : MonoBehaviour
{
    public Slot[] useSlots;
    public Item[] items;

    public Item item; // 획득한 아이템.
    public int itemCount; // 획득한 아이템 개수.
    public Image itemImage; // 아이템의 이미지

    public Slot[] loadQuickSlots;

    public Slot[] GetUseSlots() { return loadQuickSlots; }

    public void LoadToQuick(int _arrayNum, string _itemName, int _itemNum)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].itemName == _itemName)
            {
                loadQuickSlots[_arrayNum].AddItem(items[i], _itemNum);
            }
        }
    }
    void Start()
    {
        loadQuickSlots = GetComponentsInChildren<Slot>();
    }

    // Update is called once per frame
    void Update()
    {
        UseItem();
    }

    void UseItem()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (useSlots[0].item != null)
            {
                useSlots[0].theitemEffectDatabase.UsePotion(useSlots[0].item);
                Debug.Log(useSlots[0].item.itemName + " 사용");
                useSlots[0].SetSlotCount(-1);       
            }
            else
                Debug.Log("사용가능한 아이템이 없습니다.");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (useSlots[1].item != null)
            {
                useSlots[1].theitemEffectDatabase.UsePotion(useSlots[1].item);
                Debug.Log(useSlots[1].item.itemName + " 사용");
                useSlots[1].SetSlotCount(-1);
            }
            else
                Debug.Log("사용가능한 아이템이 없습니다.");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (useSlots[2].item != null)
            {
                useSlots[2].theitemEffectDatabase.UsePotion(useSlots[2].item);
                Debug.Log(useSlots[2].item.itemName + " 사용");
                useSlots[2].SetSlotCount(-1);
            }
            else
                Debug.Log("사용가능한 아이템이 없습니다.");
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (useSlots[3].item != null)
            {
                useSlots[3].theitemEffectDatabase.UsePotion(useSlots[3].item);
                Debug.Log(useSlots[3].item.itemName + " 사용");
                useSlots[3].SetSlotCount(-1);
            }
            else
                Debug.Log("사용가능한 아이템이 없습니다.");
        }
    }
}
