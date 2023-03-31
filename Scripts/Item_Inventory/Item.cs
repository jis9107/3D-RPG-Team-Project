using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public string itemName; // 아이템의 이름
    [TextArea]
    public string itemDesc; // 아이템의 설명
    public ItemType itemType; // 아이템의 유형
    public Sprite itemimage; // 아이템의 이미지
    public int itemPrice; // 아이템 가격
    public GameObject itemPrefab; // 아이템의 프리팹
    public ItemDataBase useItem;
       

    public string weaponType; // 무기 유형

    public enum ItemType
    {
        Equipment,
        Used,
        Money,
        ETC
    }

}
