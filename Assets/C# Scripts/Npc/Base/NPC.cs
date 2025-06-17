using UnityEngine;

/// <summary>
/// NPC �⺻ Ŭ����. CharacterBase�� ����ϸ�, Ÿ�Ժ� ��ȣ�ۿ� ����� ����
/// </summary>
public abstract class NPC : CharacterBase
{
    public eNPC_TYPE npcType;
    public string npcName = "???";
    [SerializeField] private float interactRange = 2.5f;


    /// <summary>
    /// ���� ���� �� Shared.npcList�� �� NPC�� ���
    /// </summary>
    protected virtual void Start()
    {
        if (!Shared.npcList.Contains(this))
            Shared.npcList.Add(this);
    }

    /// <summary>
    /// NPC�� ��ȣ�ۿ� �� ����Ǵ� �޼���. Ÿ�Կ� ���� NPC�� ��ɿ� �´� �ൿ�� ����~ ��� �� NPCŬ�������� ����
    /// </summary>
    public abstract void OnInteractable();

    public virtual void OffInteractable()
    {
        Shared.player_.SetInteractionState(eINTERACTIONSTATE.NONE);
        GameManager.Instance.uiManager.CloseDialog();
        CameraController.Instance.ReturnToPlayer();
    }



    //-------------------Player���� �Ÿ��� üũ-------------------//

    /// <summary>
    /// �÷��̾� ��ġ�� NPC ���� �Ÿ��� ��ȯ
    /// </summary>
    public float GetInteractDistance(Vector3 playerPos)
    {
        return Vector3.Distance(transform.position, playerPos);
    }

    /// <summary>
    /// �÷��̾ ��ȣ�ۿ� ������ �Ÿ� ���� �ִ��� Ȯ��
    /// </summary>
    public bool IsInteractable(Vector3 playerPos)
    {
        return GetInteractDistance(playerPos) <= interactRange;
    }
}