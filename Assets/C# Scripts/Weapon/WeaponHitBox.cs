using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{

    [Header("References")]
    public Player owner;               // 데미지를 주는 주체
    public Transform followTarget;     // 본 (R_equip_joint)

    public BoxCollider hitCollider;

    private HashSet<Monster> alreadyHit = new HashSet<Monster>();

    private void Awake()
    {
        hitCollider = GetComponent<BoxCollider>();
        if (hitCollider != null)
            hitCollider.enabled = false;
    }

    public void Activate()
    {
        alreadyHit.Clear();
        if (hitCollider != null)
            hitCollider.enabled = true;
    }

    public void DeActivate()
    {
        if (hitCollider != null)
            hitCollider.enabled = false;
    }


    private void LateUpdate()
    {
        if (followTarget != null)
        {
            // 이 오브젝트 자체가 본을 따라가도록 설정
            transform.position = followTarget.position;
            transform.rotation = followTarget.rotation;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (owner == null || owner.combatSystem == null) return;

        if (other.TryGetComponent(out Monster monster))
        {
            if (alreadyHit.Contains(monster)) return;
            alreadyHit.Add(monster);


            owner.combatSystem.MeleeAttack(owner, monster);
        }
    }
}