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
    private float jumpPower = 2f;

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

            // 점프 키 입력 시
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
         
        float speed = (state == eCHARACTER_STATE.RUN) ? runSpeed : moveSpeed; // 상태에 따라 속도를 정함

        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        // 회전 //
        Quaternion targetRotation = Quaternion.LookRotation(movement); // 바라볼 방향
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime); // targetRotation으로 캐릭터 회전

    }


    void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        movement = new Vector3(input.x, 0f, input.y).normalized;
    }

    void JumpHandler()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isJumping)
            {
                float jumpTime = 2 * Mathf.Sqrt(jumpPower / -Physics.gravity.y);
                animator.SetFloat("JumpSpeed", jumpTime);
                Debug.Log(jumpTime);
                animator.SetTrigger("Jump");

                Vector3 jumpVelocity = Vector3.up * Mathf.Sqrt(jumpPower * -Physics.gravity.y);
                rigid.AddForce(jumpVelocity, ForceMode.VelocityChange);

                animator.SetBool("isGround", false);
                isJumping = true;
            }
            else
            {
                return;
            }
        }
    }

    void OnAttack()
    {
        animator.SetTrigger("Attack");
    }

    public void JumpStart()
    {
        state = eCHARACTER_STATE.JUMP;
    }

    public void JumpEnd()
    {
        UpdateState();
    }
    

    // RayCast로 해보기
}
