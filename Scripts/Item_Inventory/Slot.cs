using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private Vector3 originPos;

    public Item item; // 획득한 아이템.
    public int itemCount; // 획득한 아이템 개수.
    public Image itemImage; // 아이템의 이미지

    // 아이템의 갯수
    [SerializeField]
    private Text text_Count;
    // 카운트 이미지 
    [SerializeField]
    private GameObject go_CountImage;

    public ItemEffectDatabase theitemEffectDatabase;
    public SlotToolTip theSlotToolTip;

    void Start()
    {
        theitemEffectDatabase = FindObjectOfType<ItemEffectDatabase>();
        originPos = transform.position;
        theSlotToolTip = FindObjectOfType<SlotToolTip>();
    }
    // 이미지에 투명도 조절
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    // 아이템 획득
    public void AddItem(Item _item, int _count = 1)
    {
        Debug.Log("AddItem");
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemimage;

        // 아이템이 장비가 아닐 경우 카운트 숫자
        if (item.itemType != Item.ItemType.Equipment)
        {
            go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else
        {
            text_Count.text = "0";
            go_CountImage.SetActive(false);
        }

        SetColor(1);
    }

    //아이템 개수 조정
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        // 아이템이 없을 경우 슬롯 초기화
        if (itemCount <= 0)
            ClearSlot();
    }

    // 슬롯 초기화
    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        go_CountImage.SetActive(false);
    }

    // 아이템 사용
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null)
            {
                theitemEffectDatabase.UsePotion(item);
                if (item.itemType == Item.ItemType.Used)
                {
                    SetSlotCount(-1); // 소모
                }

            }
        }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;


    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
        {
            ChangeSlot();
        }
    }

    private void ChangeSlot()
    {
        Item _tempItem = item;
        int _tempItemCount = itemCount;

        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);

        if (_tempItem != null)
        {
            DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemCount);
        }
        else
        {
            DragSlot.instance.dragSlot.ClearSlot();
        }
    }

    //마우스가 슬롯에 들어갈 때 실행
    public void OnPointerExit(PointerEventData eventData)
    {
        theSlotToolTip.HideToolTip();
    }

    // 마우스가 슬롯에서 빠져나왔을 때 실행
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
            theSlotToolTip.ShowToolTip(item, transform.position);
    }

    public void ShowToolTip(Item _item, Vector3 _pos)
    {
        theSlotToolTip.ShowToolTip(_item, _pos);
    }

    public void HideToolTip()
    {
        theSlotToolTip.HideToolTip();
    }
}
