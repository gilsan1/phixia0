using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 플레이어 입력 및 이동/스킬 제어 담당
/// </summary>
public class PlayerController : MonoBehaviour
{
    private Vector3 movement;
    private Vector2 lastInput = Vector2.zero;

    public float runSpeed = 4f;
    public float moveSpeed = 2f;
    public float turnSpeed = 10f;

    public eCHARACTER_STATE state;

    private PlayerAnimaton playerAnimaton;
    private Rigidbody rigid;
    private Player player;

    [SerializeField] private Transform cameraTransform;

    private void Awake()
    {
        playerAnimaton = GetComponent<PlayerAnimaton>();
        rigid = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
    }

    private void Start()
    {
        StartCoroutine(EUpdateState());
    }

    private void Update()
    {
        MoveHandler();
    }

    /// <summary>
    /// 상태 갱신 루프 (이동 상태 등)
    /// </summary>
    IEnumerator EUpdateState()
    {
        while (true)
        {
            if (IsInActionState())
            {
                yield return null;
                continue;
            }

            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            Vector3 currentMove = forward * lastInput.y + right * lastInput.x;

            if (currentMove.magnitude < 0.1f)
            {
                state = eCHARACTER_STATE.IDLE;
                playerAnimaton.SetSpeed(0f);
                movement = Vector3.zero;
            }
            else if (Keyboard.current.leftShiftKey.isPressed)
            {
                state = eCHARACTER_STATE.RUN;
                playerAnimaton.SetSpeed(runSpeed);
                movement = currentMove;
            }
            else
            {
                state = eCHARACTER_STATE.WALK;
                playerAnimaton.SetSpeed(moveSpeed);
                movement = currentMove;
            }

            playerAnimaton.SetState(state);
            yield return null;
        }
    }

    private void MoveHandler()
    {
        if (IsInActionState()) return;

        float speed = (state == eCHARACTER_STATE.RUN) ? runSpeed : moveSpeed;
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }

    public void OnMove(InputValue value)
    {
        if (value == null) return;

        lastInput = value.Get<Vector2>();

        if (!IsInActionState())
            movement = new Vector3(lastInput.x, 0f, lastInput.y).normalized;
    }

    public void OnAttack()
    {
        if (state != eCHARACTER_STATE.ATTACK)
        {
            state = eCHARACTER_STATE.ATTACK;
            playerAnimaton.SetState(state);
        }
    }

    // 애니메이션 이벤트 호출
    public void AttackEnd()
    {
        state = eCHARACTER_STATE.IDLE;
        playerAnimaton.SetState(state);
        playerAnimaton.SetSpeed(0f);
    }

    public void OnSkill1() => TryUseSkill(0, eCHARACTER_STATE.SKILL1);
    public void OnSkill2() => TryUseSkill(1, eCHARACTER_STATE.SKILL2);
    public void OnSkill3() => TryUseSkill(2, eCHARACTER_STATE.SKILL1); // enum에 따로 없으면 재활용

    /// <summary>
    /// 스킬 실행 공통 처리
    /// </summary>
    private void TryUseSkill(int index, eCHARACTER_STATE nextState)
    {
        if (player == null || player.currentWeapon == null) return;

        SkillBase[] skills = player.currentWeapon.Skills;
        if (skills.Length <= index || skills[index] == null || !skills[index].CanExecute()) return;

        state = nextState;
        playerAnimaton.SetState(state);
        player.UseSkill(index);
    }

    private bool IsInActionState()
    {
        return state == eCHARACTER_STATE.ATTACK ||
               state == eCHARACTER_STATE.SKILL1 ||
               state == eCHARACTER_STATE.SKILL2;
    }
}
