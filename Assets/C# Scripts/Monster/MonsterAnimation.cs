using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimation : MonoBehaviour
{
    [SerializeField] public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetState(eMONSTER_STATE state)
    {
        Debug.Log($"[MonsterAnimation] SetState: {state}");
        animator.SetInteger("STATE", (int)state);
    }
}
