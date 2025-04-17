using System.Collections;
using JetBrains.Rider.Unity.Editor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector3 movement;

    public float runSpeed = 4f;
    public float moveSpeed = 2f;
    public float turnSpeed = 10f;

    public eCHARACTER_STATE state;
    private Animator animator;
    private Rigidbody rigid;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    private void UpdateState()
    {
        if (state == eCHARACTER_STATE.ATTACK)
            return;

        if (movement.magnitude < 0.1f)
        {
            state = eCHARACTER_STATE.IDLE;
        }
        else if (Keyboard.current.leftShiftKey.isPressed)
        {
            state = eCHARACTER_STATE.RUN;
        }
        else
        {
            state = eCHARACTER_STATE.WALK;
        }

        animator.SetInteger("State", (int)state);
        animator.SetBool("isMove", state == eCHARACTER_STATE.WALK || state == eCHARACTER_STATE.RUN);
        animator.SetBool("isRun", state == eCHARACTER_STATE.RUN);
    }

    private void Update()
    {
        UpdateState();
        MoveHandler();
    }

    private void MoveHandler()
    {
        if (state == eCHARACTER_STATE.IDLE || state == eCHARACTER_STATE.ATTACK)
            return;

        float speed = (state == eCHARACTER_STATE.RUN) ? runSpeed : moveSpeed;
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        Quaternion targetRotation = Quaternion.LookRotation(movement);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    void OnMove(InputValue value)
    {
        if (state == eCHARACTER_STATE.ATTACK) return;

        Vector2 input = value.Get<Vector2>();
        movement = new Vector3(input.x, 0f, input.y).normalized;
    }

    void OnAttack()
    {
        if (state != eCHARACTER_STATE.ATTACK)
        {
            state = eCHARACTER_STATE.ATTACK;
            animator.SetInteger("State", (int)state);
            animator.SetTrigger("Attack");
        }
    }

    public void AttackEnd()
    {
        state = eCHARACTER_STATE.IDLE;
    }
}