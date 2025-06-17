using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimaton : MonoBehaviour
{
    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void SetState(eCHARACTER_STATE state)
    {
        animator.SetInteger("STATE", (int)state);
    }
    
    public void SetSpeed(float speed)
    {
        animator.SetFloat("Speed", speed);
    }
}
