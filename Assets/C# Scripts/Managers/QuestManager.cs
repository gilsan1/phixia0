using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����Ʈ ���¸� �����ϴ� �Ŵ��� Ŭ����
/// - ���̺��� �ʱ�ȭ
/// - ���� ��/�Ϸ� ����Ʈ ����
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
    /// CSV ���̺��� ����Ʈ ������ �ε� �� ����
    /// </summary>
    public void InitFromTable()
    {
        var questInfos = GameManager.tableMgr.questData.GetAll();

        for (int i = 0; i < questInfos.Count; i++)
        {
            var info = questInfos[i];

            // ����Ʈ ������ ó���̶�� ���
            if (!allQuests.TryGetValue(info.ID, out Quest quest))
            {
                quest = new Quest(
                    info.ID,
                    info.QuestTitle,
                    info.Description,
                    new List<QuestTask>(),
                    info.RewardGold,
                    info.RewardExp,
                    new List<int>() { info.RewardItemID }
                );

                allQuests.Add(info.ID, quest);
                activeQuests.Add(quest);
                Debug.Log($"New Quest ID {info.ID}");
            }

            // TaskType ���ڿ� �� enum���� ��ȯ
            if (!System.Enum.TryParse(info.TaskType, out eTASKTYPE taskType))
            {
                Debug.LogError($"[QuestManager] �߸��� TaskType: {info.TaskType}");
                continue;
            }

            QuestTask task = (taskType == eTASKTYPE.KILL || taskType == eTASKTYPE.COLLECT)
                ? new CountableTask(taskType, info.TargetID, info.TargetAmount)
                : new SimpleTask(taskType, info.TargetID);

            quest.tasks.Add(task);
        }

        Debug.Log($"[QuestManager] ����Ʈ ������ {allQuests.Count}�� �ε� �Ϸ�");
    }

    /// <summary>
    /// ����Ʈ ���� (ID ����)
    /// </summary>
    public void AcceptQuest(int questID)
    {
        if (!allQuests.TryGetValue(questID, out Quest quest))
        {
            Debug.LogError($"[QuestManager] �ش� ID�� ����Ʈ�� �������� �ʽ��ϴ�: {questID}");
            return;
        }

        if (activeQuests.Contains(quest))
        {
            Debug.LogWarning($"[QuestManager] �̹� ������ ����Ʈ�Դϴ�: {quest.questTitle}");
            return;
        }

        activeQuests.Add(quest);
        Debug.Log($"[QuestManager] ����Ʈ ����: {quest.questTitle}");
    }

    public void OnQuestComplete(Quest quest)
    {
        if (!activeQuests.Contains(quest))
        {
            Debug.LogWarning($"[QuestManager] �Ϸ� ó�� ��û�� ����Ʈ�� ���� �� ����Ʈ�� �����ϴ�: {quest.questTitle}");
            return;
        }

        activeQuests.Remove(quest);
        completeQuests.Add(quest);

        Debug.Log($"[QuestManager] ����Ʈ �Ϸ��: {quest.questTitle}");

        for (int i = 0; i < quest.rewardItemID.Count; i++)
        {
            InventorySystem.Instance.TryAddItem(TableMgr.Instance.weaponItem.GetItem(quest.rewardItemID[i]));
        }
        // TODO: ���� ���� ó�� ����
    }

    public List<Quest> GetActivesQuests() => activeQuests;
    public List<Quest> GetCompletedQuests() => completeQuests;
    public Dictionary<int, Quest> GetAllQuests() => allQuests;

    public void ResetAll()
    {
        activeQuests.Clear();
        completeQuests.Clear();
        allQuests.Clear();
        Debug.Log("[QuestManager] ����Ʈ �ʱ�ȭ �Ϸ�");
    }

    public Quest GetQuest(int id)
    {
        return allQuests[id];
    }
}