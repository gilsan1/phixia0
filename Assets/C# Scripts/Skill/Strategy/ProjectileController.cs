using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private ISkillSystem owner;
    private SkillBase skill;
    private string target;
    private Coroutine moveRoutine;

    [SerializeField] private float speed;
    [SerializeField] private float lifetime;

    private float timer = 0f;

    public void Launch(ISkillSystem owner, SkillBase skill, string target)
    {
        this.owner = owner;
        this.skill = skill;
        this.target = target;
        timer = 0f;

        moveRoutine = StartCoroutine(ShootProjectile());
    }

    IEnumerator ShootProjectile()
    {
        while (true)
        {
            transform.position += transform.forward * speed * Time.deltaTime;

            timer += Time.deltaTime;
            if (timer > lifetime)
            {
                Destroy(gameObject);
                yield break;
            }
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (owner == null || skill == null) return;

        if (target == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                owner.CombatSystem.UseSkill(owner.Owner, player, skill.Damage);
                Destroy(gameObject);
                return;
            }
        }

        if (target == "Enemy")
        {
            MonsterBase monster = other.GetComponent<MonsterBase>();  
            
            if(monster != null)
            {
                owner.CombatSystem.UseSkill(owner.Owner, monster, skill.Damage);
                Destroy(gameObject);
            }
        }
    }
}
