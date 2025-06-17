using UnityEngine;

public class SkillPanel : MonoBehaviour
{
    [SerializeField] private SkillSlot[] skillSlots;

    /// <summary>
    /// ���� ������ ��ų ������ ���Կ� ����
    /// </summary>
    public void RefreshSkills(SkillBase[] skills)
    {
        for (int i = 0; i < skillSlots.Length; i++)
        {
            if (skills != null && i < skills.Length)
                skillSlots[i].SetSkill(skills[i]);
            else
                skillSlots[i].SetSkill(null);
        }
    }

    /// <summary>
    /// Ư�� ��ų ������ ��Ÿ�� ����
    /// </summary>
    public void TriggerCooldown(int index)
    {
        if (index < 0 || index >= skillSlots.Length) return;
        skillSlots[index].StartCooldown();
    }
}