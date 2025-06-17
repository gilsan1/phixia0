    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerEquip : MonoBehaviour
{
    public static PlayerEquip Instance { get; private set; }

    public Player player;

    private int layer;


    [Header("Weapon")]
    [SerializeField] Transform weaponSlot;
    private GameObject currentWeapon;

    [Header("Helmet")]
    [SerializeField] Transform helmetSlot;
    private GameObject currentHelmet;


    [Header("CharacterPriviewUI")]
    [SerializeField] private PortraitCamera portraitCamera;

    [Header("Skill Icons")]
    [SerializeField] private SkillPanel skillPanel;



    private void Awake()
    {
        Instance = this;
        player = GetComponent<Player>();
        layer = LayerMask.NameToLayer("Item");
    }

    /// <summary>
    /// 무기를 장착 시 Prefab을 생성하여 외형을 만들고, Player의 CurrentWeapon / hitBox / effectTransform을 설정해줌
    /// </summary>
    public void SpawnWeapon(GameObject weaponPrefabs)
    {
        ClearWeapon();

        if (weaponPrefabs != null && weaponSlot != null)
        {
            currentWeapon = Instantiate(weaponPrefabs, weaponSlot);
            SetLayerRecursively(currentWeapon, layer);
            skillPanel.RefreshSkills(currentWeapon.GetComponent<WeaponBase>().Skills);

            currentWeapon.transform.localPosition = new Vector3(0f, 1.562f, 0.9f);
            currentWeapon.transform.localRotation = Quaternion.Euler(new Vector3(0f, -90f, -180f));

            player.currentWeapon = currentWeapon.GetComponent<WeaponBase>();
            player.hitBox = currentWeapon.GetComponentInChildren<WeaponHitbox>();
            player.effectTransform = currentWeapon.transform.Find("M_Knight_Greatsword/EffectPoint")?.GetComponent<Transform>();
        }
    }

    public void SpawnHelmet(GameObject helmetPrefab)
    {
        ClearHelmet();

        if (helmetPrefab != null && helmetSlot != null)
        {
            currentHelmet = Instantiate(helmetPrefab, helmetSlot);
            SetLayerRecursively (currentHelmet, layer);

            currentHelmet.transform.localPosition = new Vector3(0.06f, 0.008f, -0.003f);
            currentHelmet.transform.localRotation = Quaternion.Euler(new Vector3(-83, 120, -31));
        }
        portraitCamera.CapturePortrait();
    }


    public void ClearWeapon()
    {
        if (currentWeapon != null)
        {
            DestroyImmediate(currentWeapon); // 즉시 제거
            portraitCamera.CapturePortrait();
        }

        player.currentWeapon = null;
        player.hitBox = null;
        player.effectTransform = null;

        skillPanel.RefreshSkills(null);
    }

    public void ClearHelmet()
    {
        if (currentHelmet != null)
        {
            DestroyImmediate(currentHelmet); // 즉시 제거
            portraitCamera.CapturePortrait();
        }
    }



    private void SetLayerRecursively(GameObject obj, int layer) // 생성된 아이템 레이어 설정.
    {
        obj.layer = layer;
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            SetLayerRecursively(obj.transform.GetChild(i).gameObject, layer);
        }
    }
}
