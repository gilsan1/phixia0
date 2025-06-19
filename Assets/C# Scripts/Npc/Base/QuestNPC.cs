using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 퀘스트 NPC -> 각 NPC마다 가지고 있는 퀘스트ID를 통하여 퀘스트 목록을 생성해주고 플레이어에게 제공
/// </summary>

public class QuestNPC : NPC
{
    [Header("QUEST IDS")]
    [SerializeField] private List<int> questIDs; // ID들을 설정


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
    /// 현재 NPC가 제공할 수 있는 퀘스트 중, 아직 수락하지 않고 완료도 안 한 퀘스트만 반환
    /// </summary>
    /// <returns>수락 가능한 퀘스트 리스트</returns>
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
