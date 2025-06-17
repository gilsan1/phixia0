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
/// 드롭다운을 통해 인벤토리 아이템을 정렬하는 핸들러 클래스
/// - 각 패널별로 붙어서 해당 카테고리에 맞는 정렬 실행
/// </summary>
public class DropDownSortHandler : MonoBehaviour
{
    [SerializeField] private eINVENTORY_CATEGORY category; // 정렬 대상 카테고리 (무기, 방어구 등)
    private Dropdown dropdown;

    private void Awake()
    {
        dropdown = GetComponent<Dropdown>();
        dropdown.onValueChanged.AddListener(OnSortOptionChanged); // 드롭다운 값 변경 시 정렬 함수 호출
    }

    private void Start()
    {
        // 드롭다운에 옵션이 없는 경우 기본 옵션 추가
        if (dropdown.options.Count == 0)
        {
            dropdown.ClearOptions();
            dropdown.AddOptions(new List<string> { "이름순", "기능 내림차순", "기능 오름차순" });
        }

        // 시작 시 현재 선택된 옵션으로 정렬 한 번 수행
        OnSortOptionChanged(dropdown.value);
    }

    /// <summary>
    /// 드롭다운 항목 선택 시 호출되는 정렬 분기 함수
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
                InventorySystem.Instance.SortConsumableItem(eITEMCONSUM_TYPE.BOX, index); // ETC 항목도 소비형 구조 기반
                break;
        }
    }
}