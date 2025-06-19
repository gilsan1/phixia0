using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public abstract class BaseSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI quantityText;

    public ItemSlot slotData { get; private set; }


    protected void Start()
    {
        RefreshSlot();
    }

    public virtual void SetSlot(ItemSlot slot)
    {
        slotData = new ItemSlot(slot.Item, slot.Quantity);
        RefreshSlot();
    }

    public void ClearSlot()
    {
        slotData = null;
        RefreshSlot();
    }

    /// <summary>
    /// 슬롯 UI 갱신하기
    /// </summary>
    private void RefreshSlot()
    {
        if (slotData != null && !slotData.IsEmpty)
        {
            itemIcon.sprite = slotData.Item.ItemIcon;

            if(slotData.Quantity == 1)
                quantityText.text = "";
            else
                quantityText.text = slotData.Quantity.ToString();

            itemIcon.enabled = true;
        }
        else
        {
            itemIcon.sprite = null;
            quantityText.text = "";
            itemIcon.enabled = false;
        }
    }

    /// <summary>
    /// 아이템 툴팁
    /// </summary>
    public virtual void ShowTooltip()
    {
        if (slotData != null && !slotData.IsEmpty)
        {
            if (slotData.Item.Item_Type == eITEMTYPE.EQUIP)
                GameManager.Instance.uiManager.ItemTooltip.ShowTooltip(slotData.Item, this.GetComponentInParent<Canvas>().transform);
            else if (slotData.Item.Item_Type == eITEMTYPE.CONSUMABLE)
                GameManager.Instance.uiManager.ItemTooltip.ShowTooltip(slotData.Item, this.GetComponentInParent<Canvas>().transform);
                
        }
    }

    public abstract void OnPointerClick(PointerEventData eventData);
}
