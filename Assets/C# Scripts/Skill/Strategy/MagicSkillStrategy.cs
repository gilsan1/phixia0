using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSkillStrategy : ISkillStrategy
{
    private Vector3 targetPosition;

    private Dictionary<SkillBase, Vector3> skillTargetPositions = new();

    public void PreExecute(SkillBase skill, ISkillSystem user) // ��ų �غ�
    {
        if (Shared.player_ == null) return;

        targetPosition = Shared.player_.transform.position;
        skillTargetPositions[skill] = targetPosition;


        if (skill.IndicatorPrefab != null) // ���� ����
        {
            Quaternion rot = Quaternion.LookRotation(Vector3.forward); // ������ �����ٺ��� ����
            GameObject indicator = GameObject.Instantiate(skill.IndicatorPrefab, targetPosition + Vector3.up * 0.5f, rot);
            if (indicator.TryGetComponent(out IndicatorController ctrl))
            {
                ctrl.CreateIndicator(skill);
            }
        }
    }
    public void Execute(SkillBase skill, ISkillSystem user, string target)
    {
        if (!skillTargetPositions.TryGetValue(skill, out Vector3 targetPos))
        {
            Debug.LogWarning($"[MagicSkillStrategy] ���� ��ġ ������ �����ϴ�. �⺻ ��ġ�� ��ü�մϴ�.");
            targetPos = user.SkillOrigin.position;
        }


        GameObject field = GameObject.Instantiate(skill.EffectPrefab, targetPosition, Quaternion.identity);
        field.transform.localScale = Vector3.one * skill.Range * 0.1f;

        if (field.TryGetComponent(out MagicFieldController controller))
        {
            controller.Init(user, skill, target);
        }
    }   


}
