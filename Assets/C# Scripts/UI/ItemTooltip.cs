using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour
{
    [Header("UI")]
    public GameObject ItemTooltipPrefab;
    public GameObject consumableTooltip;
    private GameObject currentPanel;


    public void ShowTooltip(ItemBase item, Transform canvasPos)
    {
        if (item.Item_Type == eITEMTYPE.EQUIP)
        {
            if (currentPanel != null)
                Destroy(currentPanel);

            currentPanel = Instantiate(ItemTooltipPrefab, canvasPos);

            var iconImage = currentPanel.transform.Find("Background_Image/ItemToolTip/Icon_Image").GetComponent<Image>();
            var nameText = currentPanel.transform.Find("Background_Image/ItemToolTip/Name_Text").GetComponent<TextMeshProUGUI>();
            var atkText = currentPanel.transform.Find("Background_Image/ItemToolTip/Stat/Atk_Value_Text").GetComponent<TextMeshProUGUI>();
            var defText = currentPanel.transform.Find("Background_Image/ItemToolTip/Stat/Def_Value_Text").GetComponent<TextMeshProUGUI>();
            var descText = currentPanel.transform.Find("Background_Image/ItemToolTip/Desc_Text").GetComponent<TextMeshProUGUI>();


            var equip = item as EquipBase;

            iconImage.sprite = equip.ItemIcon;
            nameText.text = equip.Item_Name.ToString();
            atkText.text = equip.Item_Atk.ToString();
            defText.text = equip.Item_Def.ToString();
            descText.text = equip.Item_Description.ToString();
        }

        else if (item.Item_Type == eITEMTYPE.CONSUMABLE)
        {
            if (currentPanel != null)
                Destroy(currentPanel);

            currentPanel = Instantiate(consumableTooltip, canvasPos);

            var iconImage = currentPanel.transform.Find("Background_Image/ItemToolTip/Icon_Image").GetComponent<Image>();
            var nameText = currentPanel.transform.Find("Background_Image/ItemToolTip/Name_Text").GetComponent<TextMeshProUGUI>();
            var typeText = currentPanel.transform.Find("Background_Image/ItemToolTip/Stat/TypeValue_Text").GetComponent<TextMeshProUGUI>();
            var valueText = currentPanel.transform.Find("Background_Image/ItemToolTip/Stat/PercentValue_Text").GetComponent<TextMeshProUGUI>();
            var descText = currentPanel.transform.Find("Background_Image/ItemToolTip/Desc_Text").GetComponent<TextMeshProUGUI>();

            var consumable = item as ConsumableBase;

            switch (consumable)
            {
                case ConsumablePotion potion:
                    iconImage.sprite = potion.LoadIcon();
                    nameText.text = potion.Item_Name.ToString();
                    typeText.text = potion.ConsumableType.ToString(); // e.g. "POTION"
                    valueText.text = $"{potion.healPercent}%";
                    descText.text = potion.Item_Description.ToString() ;
                    break;

                case ConsumableScroll scroll:
                    iconImage.sprite = scroll.LoadIcon();
                    nameText.text = scroll.Item_Name.ToString();
                    typeText.text = scroll.ConsumableType.ToString(); // e.g. "ATK_UP"
                    valueText.text = $"+{scroll.buffAmount} / {scroll.buffDuration}s";
                    descText.text = scroll.Item_Description.ToString();
                    break;

                default:
                    typeText.text = "Unknown";
                    valueText.text = "-";
                    break;
            }
        }
    }
        


    public void DestroyToolTip()
    {
        Destroy(gameObject);
    }
}
