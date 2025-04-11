using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [Header("Speed Value")]
    public float moveSpeed = 3f;
    public float turnSpeed = 1f;
    public float jumpPower = 20f;

    private Vector2 moveInput;
    private Vector3 movement;

    private Camera mainCamera;
    private Animator animator;
    private Rigidbody rigid;

    private bool isJumping;
    private bool isGround;
    private bool isJumpDown;

    private void Awake()
    {
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        isJumping = false;
        isGround = true;
    }
    private void Update()
    {
        HandleMove();
        Jump();
    }


    private void HandleMove()
    {
        bool isMove = movement.magnitude > 0.1f;

        if (isMove)
        {
            animator.SetBool("isMove", isMove);

            Quaternion targetRotation = Quaternion.LookRotation(movement); // LookRotation(벡터 방향 값) : 자기 기준에서 지정한 방향 값을 바라보게함

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
            transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            animator.SetBool("isMove", isMove);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isJumping)
            {
                animator.SetLayerWeight(1, 0f);
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
        animator.SetLayerWeight(1, 1f);
    }

    private void OnAttack()
    {
        animator.SetTrigger("Attack");
    }


    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

        if (moveInput != null)
        {
            movement = new Vector3(moveInput.x, 0f, moveInput.y).normalized; // 방향벡터 값을 movement에
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Ground");
            animator.SetBool("isGround", true);
            animator.SetTrigger("JumpDown");
            isJumping = false;

        }
    }
}







