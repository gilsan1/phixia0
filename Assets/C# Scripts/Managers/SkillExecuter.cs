using UnityEngine;

/// <summary>
/// 스킬 실행 흐름을 관리하는 전략 패턴 기반 매니저
/// </summary>
public class SkillExecuter : MonoBehaviour
{
    /// <summary>
    /// 스킬 사전 처리 (장판, 예고 등)
    /// </summary>
    public void PreExecute(SkillBase skill, ISkillSystem user)
    {
        if (skill == null || user == null) return;

        ISkillStrategy strategy = SkillResistry.GetStrategy(skill.skillType);
        if (strategy == null)
        {
            Debug.LogWarning($"No startegy!!!!!!!! : {skill.skillType}");
            return;
        }

        strategy.PreExecute(skill, user);
    }

    /// <summary>
    /// 스킬 실제 실행 (데미지 판정, 효과 적용)
    /// </summary>
    public void Execute(SkillBase skill, ISkillSystem user, string target)
    {
        if (skill == null || user == null) return;

        ISkillStrategy strategy = SkillResistry.GetStrategy(skill.skillType);
        if (strategy == null)
        {
            Debug.LogWarning($"No startegy!!!!!!!! : {skill.skillType}");
            return;
        }

        strategy.Execute(skill, user, target);
    }
}
