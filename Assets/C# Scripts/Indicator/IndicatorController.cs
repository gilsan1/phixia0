using UnityEngine;
using System.Collections;

public class IndicatorController : MonoBehaviour
{
    public eINDICATOR type; // 장판 타입

    private float growDuration = 0.5f;
    private float lifeTime = 1.5f;
    private bool autoDestroy = true;

    public void CreateIndicator(SkillBase skill)
    {
        this.type = skill.indicatorType;
        this.growDuration = skill.IndicatorGrowTime;
        this.lifeTime = skill.IndicatorLifeTime;
        this.autoDestroy = true;

        StartCoroutine(IndicatorHandle(skill));
    }

    private IEnumerator IndicatorHandle(SkillBase skill)
    {
        switch (type)
        {
            case eINDICATOR.CIRCLE:
                {
                    float fixScale = 0.1f;
                    Vector3 targetScale = new Vector3(skill.Range * fixScale * 2f, 1f, skill.Range * fixScale * 2f); // 원형은 넓게
                    yield return AnimateScale(targetScale);
                    break;
                }

            case eINDICATOR.RECTANGLE:
                {
                    float fixScale = 0.1f;
                    Vector3 targetScale = new Vector3(skill.Range * fixScale, 1f, skill.Range * fixScale * 2f); // 사각형은 앞뒤 길게
                    yield return AnimateScale(targetScale);
                    break;
                }

            case eINDICATOR.FAN:
                {
                    if (TryGetComponent(out FanMeshGenerator fan))
                    {
                        fan.Generate(skill.Range, skill.Angle);
                    }
                    break;
                }
        }

        if (autoDestroy)
        {
            yield return new WaitForSeconds(lifeTime);
            Destroy(gameObject);
        }
    }

    private IEnumerator AnimateScale(Vector3 targetScale)
    {
        transform.localScale = Vector3.zero;

        float t = 0f;
        while (t < growDuration)
        {
            t += Time.deltaTime;
            float normalized = Mathf.Clamp01(t / growDuration);
            transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, normalized);
            yield return null;
        }

        transform.localScale = targetScale;
    }
}