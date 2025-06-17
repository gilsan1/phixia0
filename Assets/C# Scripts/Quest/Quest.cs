using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Quest : ���� ���� Task�� ����, ��ü �Ϸ� ���� �Ǵ�
/// </summary>
public class Quest
{
    public int id;
    public string questTitle;
    public string description;

    // ���� ���� �½�ũ
    public List<QuestTask> tasks;


    /// <summary>
    /// ���� : �ϴ� ���.. ����ġ.. ������
    /// </summary>
    public int rewardGold;
    public int rewardExp;
    public List<int> rewardItemID;


    /// <summary>
    /// ���� �½�ũ ���� ��Ȳ
    /// </summary>
    public bool TasksCompleted => CheckAllTasksComplete();

    // ����Ʈ�� ����(����, �Ϸ�, ���� ����)
    public eQUESTSTATE questState;

    public Quest(int id, string title, string description, List<QuestTask> taskList, int rewardGold, int rewardExp, List<int> rewardItemID)
    {
        this.id = id;
        this.questTitle = title;
        this.description = description;
        this.tasks = taskList;

        this.rewardGold = rewardGold;
        this.rewardExp = rewardExp;
        this.rewardItemID = rewardItemID;

        this.questState = eQUESTSTATE.PROGRESS;
    }

    /// <summary>
    /// ��� �½�ũ�� �Ϸ� ���� Ȯ�� �� ����Ʈ �Ϸ� ����
    /// </summary>
    private bool CheckAllTasksComplete()
    {
        for (int i = 0; i < tasks.Count; i++)
        {
            if (!tasks[i].IsComplete)
                return false;
        }
        return true;

    }
    /// <summary>
    /// �ܺο��� ����Ʈ ���� ��ȣ�� ������ ����
    /// </summary>
    public void CheckProgress(eTASKTYPE taskType, int targetID, int amount)
    {
        if (questState != eQUESTSTATE.PROGRESS) return;

        for (int i = 0; i < tasks.Count; i++)
        {
            if (tasks[i].taskType == taskType)
                tasks[i].Progress(targetID, amount);
        }

        if (TasksCompleted)
        {
            Complete(); 
        }
    }

    public void Complete()
    {
        questState = eQUESTSTATE.COMPLETE;

        GiveReward();

        GameManager.Instance.questManager.OnQuestComplete(this);
        questState = eQUESTSTATE.REWARD;
    }


    /// <summary>
    /// ���� ó��
    /// </summary>
    private void GiveReward()
    {
        // �̷������� ����? �ϴ� ������ ���߿�
        //Shared.player_.AddGold(rewardGold); <- ���� ��� ������ ����غ���
        //Shared.player_.stat.addExp(rewardExp);

        for (int i = 0; i < rewardItemID.Count; i++)
        {
            int itemID = rewardItemID[i];
            Debug.Log($"{itemID} ����");
        }
    }
}