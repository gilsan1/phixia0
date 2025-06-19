using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class UI_HealthBar : MonoBehaviour
{
    public CharacterBase target;
    private UnityEngine.UI.Slider healthSlider;

    private Coroutine updateRoutine;

    private void Awake()
    {
        healthSlider = GetComponentInChildren<UnityEngine.UI.Slider>();
    }

    private void Start()
    {
        healthSlider.maxValue = target.stat.Base_maxHP;
        healthSlider.value = target.stat.currentHP;

        StartCoroutine(WatchHP());
    }


    private IEnumerator WatchHP()
    {
        while (true)
        {
            float current = healthSlider.value;
            float targetValue = target.stat.currentHP;

            if (Mathf.Abs(current - targetValue) > 0.1f)
            {
                if (updateRoutine != null)
                    StopCoroutine(updateRoutine);

                updateRoutine = StartCoroutine(SmoothUpdate(current, targetValue));
            }

            yield return new WaitForSeconds(0.1f); // 감시 주기
        }
    }

    private IEnumerator SmoothUpdate(float from, float to)
    {
        float duration = 0.3f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            healthSlider.value = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        healthSlider.value = to;
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.transform.position + Vector3.up * 3.5f;
            transform.forward = Camera.main.transform.forward;
        }
    }

}

