using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    

    /// <summary>
    /// ����Ʈ ����/����
    /// </summary>

    public GameObject SpawnEffect(GameObject effectPrefab, Vector3 pos)
    {
        if (effectPrefab == null) return null;

        GameObject fx = Instantiate(effectPrefab, pos, Quaternion.identity);
        StartCoroutine(DestroyWhenDone(fx));

        return fx;
    }

    IEnumerator DestroyWhenDone(GameObject fx)
    {
        var ps = fx.GetComponentInChildren<ParticleSystem>();
        if (ps != null)
        {
            yield return new WaitUntil(() => !ps.IsAlive(true));
        }

        Destroy(fx);
    }
}
