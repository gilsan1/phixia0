using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public float weaponDamage;


    //public SkillData[] skills;  // SO �迭
    [SerializeField]
    protected SkillBase[] skills;

    public SkillBase[] Skills => skills;



    public abstract void Attack(CharacterBase owner);
}
