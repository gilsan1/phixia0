using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowUIMananger : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private GameObject equip_Panel;
    [SerializeField] private GameObject consumable_Panel;
    //[SerializeField] private GameObject etc_Panel;

    [SerializeField] private Toggle equip_Toggle;
    [SerializeField] private Toggle consumable_Toggle;
    //[SerializeField] private Toggle etc_Toggle;

    [SerializeField] private GameObject drag_Icon;
    public static Image drag_Image;

    private void Awake()
    {
        equip_Toggle.onValueChanged.AddListener((v) => { if (v) ShowPanel(equip_Panel); });
        consumable_Toggle.onValueChanged.AddListener((v) => { if (v) ShowPanel(consumable_Panel); });
        //etc_Toggle.onValueChanged.AddListener((v) => { if (v) ShowPanel(etc_Panel); });

        drag_Image = drag_Icon.GetComponent<Image>();
    }

    private void ShowPanel(GameObject target)
    {
        equip_Panel.SetActive(false);
        consumable_Panel.SetActive(false);
        //etc_Panel.SetActive(false);

        target.SetActive(true);
    }

    public void EquipPanel_On()
    {
        ShowPanel(equip_Panel);
    }

    public void ConsumablePanel_On()
    {
        ShowPanel(consumable_Panel);
    }
   /* public void EctPanel_On()
    {
        ShowPanel(etc_Panel);
    }*/

    private void SetDragIcon()
    {
        
    }
}


