using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSkillStrategy : ISkillStrategy
{
    public void PreExecute(SkillBase skill, ISkillSystem user)
    {
        if (skill.IndicatorPrefab == null)
        {
            Debug.LogWarning($"[MeleeSkillStrategy] IndicatorPrefab이 null입니다. skill = {skill.SkillName}");
            return;
        }

        Vector3 pos = user.SkillOrigin.position + Vector3.up * 0.5f;
        Quaternion rot = Quaternion.LookRotation(user.SkillOrigin.forward);

        GameObject Indicator = GameObject.Instantiate(skill.IndicatorPrefab, pos, rot);
        if (Indicator.TryGetComponent(out IndicatorController control))
        {
            control.CreateIndicator(skill);
        }
    }


    public void Execute(SkillBase skill, ISkillSystem user, string target)
    {
        if (user == null || user.CombatSystem == null || user.Owner == null)
        {
            Debug.LogError("[MeleeSkillStrategy] user 또는 필드가 null입니다.");
            return;
        }
        Vector3 origin = user.SkillOrigin.position;
        Vector3 forward = user.SkillOrigin.forward;

        if (target == "Enemy")
        {
            List<MonsterBase> enemies = Shared.enemyList;
            for (int i = 0; i < enemies.Count; i++)
            {
                var enemy = enemies[i];
                if (enemy == null || !enemy.gameObject.activeInHierarchy) continue;

                Vector3 toTarget = enemy.transform.position - origin;
                float distance = toTarget.magnitude;
                float angle = Vector3.Angle(forward, toTarget.normalized);

                if (distance <= skill.Range && angle < skill.Angle / 2f)
                {
                    user.CombatSystem.UseSkill(user.Owner, enemy, skill.Damage);
                }
            }
        }

        else if (target == "Player" && Shared.player_ != null)
        {
            Vector3 toPlayer = Shared.player_.transform.position - origin;

            float distance = toPlayer.magnitude;
            float angle = Vector3.Angle(forward, toPlayer.normalized);

            if (distance <= skill.Range && angle <= skill.Angle / 2f)
            {
                user.CombatSystem.UseSkill(user.Owner, Shared.player_, skill.Damage);
            }
        }
    }
}
