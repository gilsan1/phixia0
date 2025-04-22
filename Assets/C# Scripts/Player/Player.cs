using UnityEngine;

public class Player : CharacterBase
{
    [Header("Weapon")]
    [SerializeField] public WeaponHitbox hitBox;
    [SerializeField] public WeaponBase currentWeapon;

    private void Awake()
    {
        characType = eCHARACTER.eCHARACTER_PLAYER;

        stat = new CharacterStat();
        stat.Init();
        combatSystem = new CombatSystem(stat);

        currentWeapon = GetComponentInChildren<WeaponBase>();
        hitBox = GetComponentInChildren<WeaponHitbox>();

        if (hitBox != null)
            hitBox.owner = this;
    }

    public void EnableWeaponHitbox() => hitBox?.Activate();
    public void DisableWeaponHitbox() => hitBox?.DeActivate();


    public void Attack()
    {
        currentWeapon?.Attack(this);
    }


    //-----스킬-------//
    public void UseSkill(int index)
    {
        currentWeapon?.UseSkill(index, this);
    }

    public void SkillEffect()
    {
        currentWeapon?.UseSkillEffect();
    }
}