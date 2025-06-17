using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    private List<Quest> activeQuests = new List<Quest>();
    private List<Quest> completeQuests = new List<Quest>();


    /// <summary>
    /// 퀘스트 수락 시 
    /// </summary>
    public void AddQuest(Quest quest)
    {
        if (!activeQuests.Contains(quest))
        {
            activeQuests.Add(quest);
        }
    }

    /// <summary>
    /// 현재 진행중인 퀘스트 가져오기 
    /// </summary>
    public List<Quest> GetActivesQuests()
    {
        return activeQuests;
    }


    /// <summary>
    /// 퀘스트 완료 시
    /// </summary>
    /// <param name="quest"></param>
    public void CompleteQuest(Quest quest)
    {
        if (activeQuests.Contains(quest))
        {
            activeQuests.Remove(quest);
            Debug.Log($"[QuestManager] 퀘스트 완료: {quest.questTitle}");
        }
    }

    public void OnQuestComplete(Quest quest)
    {
        if (!activeQuests.Contains(quest))
        {
            Debug.LogWarning($"[QuestManager] 완료 처리 요청된 퀘스트가 진행 중 리스트에 없습니다: {quest.questTitle}");
            return;
        }

        activeQuests.Remove(quest);

        completeQuests.Add(quest);

        Debug.Log($"[QuestManager] 퀘스트 완료됨: {quest.questTitle}");
    }
}
