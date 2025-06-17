using UnityEngine;

public class SkillBase
{
    public string SkillName { get; protected set; }

    public virtual eSKILL_TYPE skillType => eSKILL_TYPE.MELEE;
    public virtual eINDICATOR indicatorType { get; protected set; }


    public float Damage { get; protected set; }
    public float CoolDown { get; protected set; }
    public float Range { get; protected set; }
    public float Angle { get; protected set; }

    public float IndicatorGrowTime { get; protected set; } = 0.5f;
    public float IndicatorLifeTime { get; protected set; } = 1.5f;

    public GameObject IndicatorPrefab { get; protected set; }
    public GameObject EffectPrefab { get; protected set; }
    public string IconPath { get; protected set; }

    public float lastUsedTime = -999f;

    public bool CanExecute() => Time.time >= lastUsedTime + CoolDown;
    public void MarkExecute() => lastUsedTime = Time.time;

    // 실행은 외부(SkillManager)에서 담당하므로 내부에 실행 함수 없음
}