using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public float weaponDamage;


    //public SkillData[] skills;  // SO ¹è¿­
    [SerializeField]
    protected SkillBase[] skills;

    public SkillBase[] Skills => skills;



    public abstract void Attack(CharacterBase owner);
}
