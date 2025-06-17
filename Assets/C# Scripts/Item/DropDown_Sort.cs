using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public enum eINVENTORY_CATEGORY
{
    WEAPON,
    ARMOR,
    EARING,
    POTION,
    SCROLL,
    ETC
}

/// <summary>
/// ��Ӵٿ��� ���� �κ��丮 �������� �����ϴ� �ڵ鷯 Ŭ����
/// - �� �гκ��� �پ �ش� ī�װ��� �´� ���� ����
/// </summary>
public class DropDownSortHandler : MonoBehaviour
{
    [SerializeField] private eINVENTORY_CATEGORY category; // ���� ��� ī�װ� (����, �� ��)
    private Dropdown dropdown;

    private void Awake()
    {
        dropdown = GetComponent<Dropdown>();
        dropdown.onValueChanged.AddListener(OnSortOptionChanged); // ��Ӵٿ� �� ���� �� ���� �Լ� ȣ��
    }

    private void Start()
    {
        // ��Ӵٿ �ɼ��� ���� ��� �⺻ �ɼ� �߰�
        if (dropdown.options.Count == 0)
        {
            dropdown.ClearOptions();
            dropdown.AddOptions(new List<string> { "�̸���", "��� ��������", "��� ��������" });
        }

        // ���� �� ���� ���õ� �ɼ����� ���� �� �� ����
        OnSortOptionChanged(dropdown.value);
    }

    /// <summary>
    /// ��Ӵٿ� �׸� ���� �� ȣ��Ǵ� ���� �б� �Լ�
    /// </summary>
    private void OnSortOptionChanged(int index)
    {
        switch (category)
        {
            case eINVENTORY_CATEGORY.WEAPON:
                InventorySystem.Instance.SortEquipItem(eITEMEQUIP_TYPE.WEAPON, index);
                break;

            case eINVENTORY_CATEGORY.ARMOR:
                InventorySystem.Instance.SortEquipItem(eITEMEQUIP_TYPE.ARMOR, index);
                break;

            case eINVENTORY_CATEGORY.EARING:
                InventorySystem.Instance.SortEquipItem(eITEMEQUIP_TYPE.EARING, index);
                break;

            case eINVENTORY_CATEGORY.POTION:
                InventorySystem.Instance.SortConsumableItem(eITEMCONSUM_TYPE.POTION, index);
                break;

            case eINVENTORY_CATEGORY.SCROLL:
                InventorySystem.Instance.SortConsumableItem(eITEMCONSUM_TYPE.SCROLL, index);
                break;

            case eINVENTORY_CATEGORY.ETC:
                InventorySystem.Instance.SortConsumableItem(eITEMCONSUM_TYPE.BOX, index); // ETC �׸� �Һ��� ���� ���
                break;
        }
    }
}