using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ����Ʈ NPC -> �� NPC���� ������ �ִ� ����ƮID�� ���Ͽ� ����Ʈ ����� �������ְ� �÷��̾�� ����
/// </summary>

public class QuestNPC : NPC
{
    [Header("QUEST IDS")]
    [SerializeField] private List<int> questIDs; // ID���� ����


    public override void OnInteractable()
    {
        Shared.player_.SetInteractionState(eINTERACTIONSTATE.NORMAL);

        CameraController.Instance.FocusOn(cameraAnchor, () =>
        {
            List<Quest> availableQuests = GetAvailableQuests();
            GameManager.Instance.uiManager.ShowQuestListUI(availableQuests, this);
        });
    }


    /// <summary>
    /// ���� NPC�� ������ �� �ִ� ����Ʈ ��, ���� �������� �ʰ� �Ϸᵵ �� �� ����Ʈ�� ��ȯ
    /// </summary>
    /// <returns>���� ������ ����Ʈ ����Ʈ</returns>
    private List<Quest> GetAvailableQuests()
    {
        List<Quest> result = new List<Quest>();

        for (int i = 0; i < questIDs.Count; i++)
        {
            Quest quest = GameManager.Instance.questManager.GetQuest(questIDs[i]);
            if (quest == null) continue;

            if (quest.questState == eQUESTSTATE.PROGRESS || quest.questState == eQUESTSTATE.REWARD)
                continue;


            result.Add(quest);
        }

        return result;
    }
} 
