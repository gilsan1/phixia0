using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{

    [Header("References")]
    public Player owner;               // �������� �ִ� ��ü
    public Transform followTarget;     // �� (R_equip_joint)

    public Collider hitCollider;

    private HashSet<MonsterBase> alreadyHit = new HashSet<MonsterBase>();

    private void Awake()
    {
        hitCollider = GetComponent<Collider>();
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
            // �� ������Ʈ ��ü�� ���� ���󰡵��� ����
            transform.position = followTarget.position;
            transform.rotation = followTarget.rotation;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (owner == null || owner.CombatSystem == null) return;

        if (other.TryGetComponent(out MonsterBase monster))
        {
            if (alreadyHit.Contains(monster)) return;
            alreadyHit.Add(monster);


            owner.CombatSystem.MeleeAttack(owner, monster);
        }
    }
}