using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾�� ����Ǵ� ������ �����ϴ� Ŭ����
/// - �ߺ� ����
/// - ���ӽð� ����
/// - Shared.buffMgr�� ���� ����
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
        Shared.buffMgr = this; // ���� ���
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
            Debug.Log("Stat�� Null�Դϴ�");
            return;
        }
        Debug.Log("Stat�� Null�� �ƴմϴ�");
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
                Debug.Log("��������");
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