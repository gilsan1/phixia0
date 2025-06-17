using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicFieldController : MonoBehaviour
{
    private ISkillSystem owner;
    private SkillBase skill;
    private string target;

    float elapsed = 0f;
    float tickRate = 2f;

    public void Init(ISkillSystem owner, SkillBase skill, string target)
    {
        this.owner = owner;
        this.skill = skill;
        this.target = target;

        StartCoroutine(ApplyAOEDamage());
    }
   
    IEnumerator ApplyAOEDamage()
    {
        float duration = skill.IndicatorLifeTime;

        while (elapsed < duration)
        {
            elapsed += tickRate;

            Collider[] targets = Physics.OverlapSphere(transform.position, skill.Range);

            int count = targets.Length;

            for (int i = 0; i < count; i++)
            {
                var col = targets[i];

                if (target == "Player")
                {
                    Player player = col.GetComponent<Player>();
                    if (player != null)
                    {
                        owner.CombatSystem.UseSkill(owner.Owner, player, skill.Damage);
                    }
                }
                if (target == "Enemy")
                {
                    MonsterBase monster = col.GetComponent<MonsterBase>();
                    if (monster != null)
                    {
                        owner.CombatSystem.UseSkill(owner.Owner, monster, skill.Damage);
                    }
                }
            }
            yield return new WaitForSeconds(tickRate);
        }
        Destroy(gameObject);
    }
}
