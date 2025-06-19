using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어에게 적용되는 버프를 관리하는 클래스
/// - 중복 방지
/// - 지속시간 관리
/// - Shared.buffMgr로 전역 접근
/// </summary>
public class BuffManager : MonoBehaviour
{
    private class BuffData
    {
        public eBUFF_TYPE buffType;
        public float amount;
        public float duration;
        public Coroutine coroutine;
    }

    private Dictionary<eBUFF_TYPE, BuffData> activeBuffs = new Dictionary<eBUFF_TYPE, BuffData>();

    private CharacterStat stat;

    private void Awake()
    {
        Shared.buffMgr = this; // 전역 등록
    }

    public void ApplyBuff(eBUFF_TYPE buffType, float amount, float duration)
    {
        if (activeBuffs.TryGetValue(buffType, out var existing))
        {
            StopCoroutine(existing.coroutine);
            RevertBuff(buffType, existing.amount);
            activeBuffs.Remove(buffType);
            Shared.player_.StatChanged();
        }

        ApplyStat(buffType, amount);

        BuffData data = new BuffData
        {
            buffType = buffType,
            amount = amount,
            duration = duration,
            coroutine = StartCoroutine(BuffDuration(buffType, amount, duration))
        };

        activeBuffs[buffType] = data;
    }

    private IEnumerator BuffDuration(eBUFF_TYPE buffType, float amount, float duration)
    {
        yield return new WaitForSeconds(duration);
        RevertBuff(buffType, amount);
        activeBuffs.Remove(buffType);
    }

    private void ApplyStat(eBUFF_TYPE type, float amount)
    {
        stat = Shared.player_.stat;
        if (stat == null)
        {
            Debug.Log("Stat이 Null입니다");
            return;
        }
        Debug.Log("Stat이 Null이 아닙니다");
        switch (type)
        {
            case eBUFF_TYPE.ATK_UP:
                stat.BonusAtk += amount;
                Shared.player_.StatChanged();
                break;
            case eBUFF_TYPE.DEF_UP:
                stat.BonusDef += amount;
                break;
            default:
                Debug.Log("ㄴㄴㄴㄴ");
                break;
        }
    }

    private void RevertBuff(eBUFF_TYPE type, float amount)
    {
        switch (type)
        {
            case eBUFF_TYPE.ATK_UP:
                stat.BonusAtk -= amount;
                Shared.player_.StatChanged();
                break;
            case eBUFF_TYPE.DEF_UP:
                stat.BonusDef -= amount;
                Shared.player_.StatChanged();
                break;
        }
    }
}