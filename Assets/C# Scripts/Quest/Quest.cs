// 리팩토링된 Quest.cs
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public int id;
    public string questTitle;
    public string description;

    public List<QuestTask> tasks;
    public int rewardGold;
    public int rewardExp;
    public List<QuestRewardItems> rewardItems;

    public bool TasksCompleted => CheckAllTasksComplete();
    public eQUESTSTATE questState;

    public Quest(int id, string title, string description, List<QuestTask> taskList, int rewardGold, int rewardExp, List<QuestRewardItems> rewardItems)
    {
        this.id = id;
        this.questTitle = title;
        this.description = description;
        this.tasks = taskList;
        this.rewardGold = rewardGold;
        this.rewardExp = rewardExp;
        this.rewardItems = rewardItems;
        this.questState = eQUESTSTATE.PROGRESS;
    }

    private bool CheckAllTasksComplete()
    {
        for (int i = 0; i < tasks.Count; i++)
        {
            if (!tasks[i].IsComplete)
                return false;
        }
        return true;
    }

    public void CheckProgress(eTASKTYPE taskType, int targetID, int amount)
    {
        if (questState != eQUESTSTATE.PROGRESS) return;

        for (int i = 0; i < tasks.Count; i++)
        {
            if (tasks[i].taskType == taskType)
                tasks[i].Progress(targetID, amount);
        }

        if (TasksCompleted)
            GameManager.Instance.questSystem.CompleteQuest(this); // 시스템으로 넘김
    }

    public void MarkRewarded()
    {
        questState = eQUESTSTATE.REWARD;
    }
}
