using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ItemEffect
{
    public string itemName; // 아이템 이름 (키값)
}


public class ItemEffectDatabase : MonoBehaviour
{
    [SerializeField]
    private ItemEffect[] itemEffects;

    //필요한 컴포넌트
    [SerializeField]
    private StatusController thePlayerStatus;

    public void UsePotion(Item _item)
    {
        Debug.Log("사용");
        if (_item.itemType == Item.ItemType.Used)
        {
            for (int i = 0; i < itemEffects.Length; i++)
            {
                if (itemEffects[i].itemName == _item.itemName)
                {
                    switch (itemEffects[i].itemName)
                    {
                        case "HP 포션(소)":
                            Debug.Log("사용1");
                            thePlayerStatus.IncreaseHP(5);
                            break;
                        case "HP 포션(중)":
                            thePlayerStatus.IncreaseHP(30);
                            break;
                        case "HP 포션(대)":
                            thePlayerStatus.IncreaseHP(50);
                            break;
                        default:
                            break;
                    }
                }
            }
            return;
        }
        Debug.Log("ItemEffectDatabase에 일치하는 itemName이 없습니다.");
    }
}
