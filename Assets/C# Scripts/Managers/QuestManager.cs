using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 퀘스트 상태를 관리하는 매니저 클래스
/// - 테이블에서 초기화
/// - 진행 중/완료 퀘스트 관리
/// </summary>
public class QuestManager : MonoBehaviour
{
    private List<Quest> activeQuests = new List<Quest>();
    private List<Quest> completeQuests = new List<Quest>();
    private Dictionary<int, Quest> allQuests = new Dictionary<int, Quest>();

    private void Start()
    {
        InitFromTable();
    }

    /// <summary>
    /// CSV 테이블에서 퀘스트 데이터 로드 및 생성
    /// </summary>
    public void InitFromTable()
    {
        var questInfos = GameManager.tableMgr.questData.GetAll();

        for (int i = 0; i < questInfos.Count; i++)
        {
            var info = questInfos[i];

            if (!allQuests.TryGetValue(info.ID, out Quest quest))
            {
                quest = new Quest(
                    info.ID,
                    info.QuestTitle,
                    info.Description,
                    new List<QuestTask>(),
                    info.RewardGold,
                    info.RewardExp,
                    info.RewardItems
                );

                allQuests.Add(info.ID, quest);
                activeQuests.Add(quest);
                Debug.Log($"New Quest ID {info.ID}");
            }

            QuestTask task = (info.TaskType == eTASKTYPE.KILL || info.TaskType == eTASKTYPE.COLLECT)
                ? new CountableTask(info.TaskType, info.TargetID, info.TargetAmount)
                : new SimpleTask(info.TaskType, info.TargetID);

            quest.tasks.Add(task);
        }

        Debug.Log($"[QuestManager] 퀘스트 데이터 {allQuests.Count}개 로드 완료");
    }

    /// <summary>
    /// 퀘스트 수락 (ID 기준)
    /// </summary>
    public void AcceptQuest(int questID)
    {
        if (!allQuests.TryGetValue(questID, out Quest quest))
        {
            Debug.LogError($"[QuestManager] 해당 ID의 퀘스트가 존재하지 않습니다: {questID}");
            return;
        }

        if (activeQuests.Contains(quest))
        {
            Debug.LogWarning($"[QuestManager] 이미 수락한 퀘스트입니다: {quest.questTitle}");
            return;
        }

        activeQuests.Add(quest);
        Debug.Log($"[QuestManager] 퀘스트 수락: {quest.questTitle}");
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

        // 보상 지급은 이제 QuestSystem에서 처리
    }

    public List<Quest> GetActivesQuests() => activeQuests;
    public List<Quest> GetCompletedQuests() => completeQuests;
    public Dictionary<int, Quest> GetAllQuests() => allQuests;

    public void ResetAll()
    {
        activeQuests.Clear();
        completeQuests.Clear();
        allQuests.Clear();
        Debug.Log("[QuestManager] 퀘스트 초기화 완료");
    }

    public Quest GetQuest(int id)
    {
        return allQuests[id];
    }
}