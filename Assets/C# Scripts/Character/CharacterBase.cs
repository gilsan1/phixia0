using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    public CharacterStat stat { get; protected set; }
    public CombatSystem combatSystem { get; protected set; }

    public virtual void TakeDamage(float amount)
    {
        stat.TakeDamage(amount);
        Debug.Log($"{name} HP : {stat.currentHP}/{stat.maxHP}");
    }
}























