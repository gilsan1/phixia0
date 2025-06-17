using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SkillSlot : MonoBehaviour
{
    [SerializeField] private Image iconImage;         // ��ų ������
    [SerializeField] private Image cooldownFillImage; // ��Ÿ�ӿ� fill �̹��� (Filled ���)

    private SkillBase skill;
    private Coroutine cooldownRoutine;

    public void SetSkill(SkillBase skillData)
    {
        skill = skillData;

        if (skill != null)
        {
            //iconImage.sprite = Resources.Load<Sprite>($"Icons/Skill/SKill_Sword/{skill.SkillName}");
            iconImage.sprite = Resources.Load<Sprite>(skillData.IconPath);
            cooldownFillImage.fillAmount = 0f;
        }
        else
        {
            iconImage.sprite = null;
            cooldownFillImage.fillAmount = 0f;
        }
    }

    public void StartCooldown()
    {
        if (skill == null) return;

        if (cooldownRoutine != null)
            StopCoroutine(cooldownRoutine);

        cooldownRoutine = StartCoroutine(CooldownTick());
    }

    private IEnumerator CooldownTick()
    {
        float duration = skill.CoolDown;
        float startTime = Time.time;

        cooldownFillImage.fillAmount = 1f;

        while (Time.time < startTime + duration)
        {
            float elapsed = Time.time - startTime;
            float remain = duration - elapsed;

            cooldownFillImage.fillAmount = remain / duration;
            yield return null;
        }

        cooldownFillImage.fillAmount = 0f;
    }

}
