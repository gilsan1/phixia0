using System.Collections;
using System.Collections.Generic;
using JetBrains.Rider.Unity.Editor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerContoller : MonoBehaviour
{
    private eCHARACTER_STATE state;
    private Vector3 movement;
    private Animator animator;
    private Rigidbody rigid;

    private float runSpeed = 4f;
    private float moveSpeed = 2f;
    private float turnSpeed = 10f;
    private float jumpPower = 1f;

    private bool isJumping = false;



    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        if (state != eCHARACTER_STATE.JUMP)
        {
            UpdateState();
            MoveHandler();

            // ���� Ű �Է� ��
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                JumpHandler();
            }
        }
    }



    private void UpdateState()
    {
        if (movement.magnitude < 0.1f)
        {
            state = eCHARACTER_STATE.IDLE;
            animator.SetInteger("State", (int)state);
        }
        else if (Keyboard.current.leftShiftKey.isPressed)
        {
            state = eCHARACTER_STATE.RUN;
            animator.SetInteger("State", (int)state);
        }
        else
        { 
            state = eCHARACTER_STATE.WALK;
            animator.SetInteger("State", (int)state);
        }

        UpdateAnimator();
    }



    private void UpdateAnimator()
    {
        animator.SetBool("isMove", state == eCHARACTER_STATE.WALK || state == eCHARACTER_STATE.RUN);
        animator.SetBool("isRun", state == eCHARACTER_STATE.RUN);
    }

    private void MoveHandler()
    {
        if (state == eCHARACTER_STATE.IDLE)  // IDLE 
            return;
         
        float speed = (state == eCHARACTER_STATE.RUN) ? runSpeed : moveSpeed; // ���¿� ���� �ӵ��� ����

        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        // ȸ�� //
        Quaternion targetRotation = Quaternion.LookRotation(movement); // �ٶ� ����
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime); // targetRotation���� ĳ���� ȸ��

    }


    void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        movement = new Vector3(input.x, 0f, input.y).normalized;
    }

    void JumpHandler()
    {
        state = eCHARACTER_STATE.JUMP;
        animator.SetTrigger("Jump");

        // ���� �ִϸ��̼� Ÿ�ֿ̹� ���� �߷� ����
        Vector3 jumpForce = Vector3.up * Mathf.Sqrt(jumpPower * -2f * Physics.gravity.y);
        rigid.AddForce(jumpForce, ForceMode.VelocityChange);
    }

    public void JumpStart()
    {
        state = eCHARACTER_STATE.JUMP;
        animator.SetBool("Jumping", true);
    }

    public void JumpEnd()
    {
        animator.SetBool("jumping", false);
        UpdateState();
    }
}
