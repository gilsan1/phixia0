using UnityEngine;

/// <summary>
/// NPC 기본 클래스. CharacterBase를 상속하며, 타입별 상호작용 기능을 제공
/// </summary>
public abstract class NPC : CharacterBase
{
    public eNPC_TYPE npcType;
    public string npcName = "???";
    [SerializeField] private float interactRange = 2.5f;


    /// <summary>
    /// 게임 시작 시 Shared.npcList에 이 NPC를 등록
    /// </summary>
    protected virtual void Start()
    {
        if (!Shared.npcList.Contains(this))
            Shared.npcList.Add(this);
    }

    /// <summary>
    /// NPC와 상호작용 시 실행되는 메서드. 타입에 따라 NPC의 기능에 맞는 행동을 실행~ 얘는 각 NPC클래스에서 구현
    /// </summary>
    public abstract void OnInteractable();

    public virtual void OffInteractable()
    {
        Shared.player_.SetInteractionState(eINTERACTIONSTATE.NONE);
        GameManager.Instance.uiManager.CloseDialog();
        CameraController.Instance.ReturnToPlayer();
    }



    //-------------------Player와의 거리를 체크-------------------//

    /// <summary>
    /// 플레이어 위치와 NPC 간의 거리를 반환
    /// </summary>
    public float GetInteractDistance(Vector3 playerPos)
    {
        return Vector3.Distance(transform.position, playerPos);
    }

    /// <summary>
    /// 플레이어가 상호작용 가능한 거리 내에 있는지 확인
    /// </summary>
    public bool IsInteractable(Vector3 playerPos)
    {
        return GetInteractDistance(playerPos) <= interactRange;
    }
}