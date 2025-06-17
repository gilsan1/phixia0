using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryTester : MonoBehaviour
{

    [Header("������ ���̺�")]
    private TableWeaponItem weaponTable;
    private TableArmorItem armorTable;

    private TablePotion potionTable;
    private TableScroll scrollTable;

    private void Start()
    {
        var weaponTable = TableMgr.Instance.weaponItem;
        var armorTable = TableMgr.Instance.armorItem;
        var potionTable = TableMgr.Instance.potionItem;
        var scrollTable = TableMgr.Instance.scrollItem;

        var sword = weaponTable.GetItem(1001);
        var sword2 = weaponTable.GetItem(1002);
        var helmet = armorTable.GetItem(1003);
        var helmet2 = armorTable.GetItem(1004);
        var potion = potionTable.GetItem(6001); // ����
        var scroll = scrollTable.GetItem(7001); // ����


        InventorySystem.Instance.TryAddItem(sword);
        InventorySystem.Instance.TryAddItem(sword2);

        InventorySystem.Instance.TryAddItem(helmet);
        InventorySystem.Instance.TryAddItem(helmet2);

        InventorySystem.Instance.TryAddItem(potion);
        InventorySystem.Instance.TryAddItem(potion);
        InventorySystem.Instance.TryAddItem(potion);
        InventorySystem.Instance.TryAddItem(scroll);

    }
}
