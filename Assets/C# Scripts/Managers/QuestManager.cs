using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    private List<Quest> activeQuests = new List<Quest>();
    private List<Quest> completeQuests = new List<Quest>();


    /// <summary>
    /// ����Ʈ ���� �� 
    /// </summary>
    public void AddQuest(Quest quest)
    {
        if (!activeQuests.Contains(quest))
        {
            activeQuests.Add(quest);
        }
    }

    /// <summary>
    /// ���� �������� ����Ʈ �������� 
    /// </summary>
    public List<Quest> GetActivesQuests()
    {
        return activeQuests;
    }


    /// <summary>
    /// ����Ʈ �Ϸ� ��
    /// </summary>
    /// <param name="quest"></param>
    public void CompleteQuest(Quest quest)
    {
        if (activeQuests.Contains(quest))
        {
            activeQuests.Remove(quest);
            Debug.Log($"[QuestManager] ����Ʈ �Ϸ�: {quest.questTitle}");
        }
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
    }
}
