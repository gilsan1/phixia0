using UnityEngine;

public class SkillPanel : MonoBehaviour
{
    [SerializeField] private SkillSlot[] skillSlots;

    /// <summary>
    /// 현재 무기의 스킬 정보를 슬롯에 연결
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
    /// 특정 스킬 슬롯의 쿨타임 시작
    /// </summary>
    public void TriggerCooldown(int index)
    {
        if (index < 0 || index >= skillSlots.Length) return;
        skillSlots[index].StartCooldown();
    }
}