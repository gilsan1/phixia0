using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Quest : 여러 개의 Task로 구성, 전체 완료 여부 판단
/// </summary>
public class Quest
{
    public int id;
    public string questTitle;
    public string description;

    // 여러 개의 태스크
    public List<QuestTask> tasks;


    /// <summary>
    /// 보상 : 일단 골드.. 경험치.. 아이템
    /// </summary>
    public int rewardGold;
    public int rewardExp;
    public List<int> rewardItemID;


    /// <summary>
    /// 현재 태스크 진행 상황
    /// </summary>
    public bool TasksCompleted => CheckAllTasksComplete();

    // 퀘스트의 상태(진행, 완료, 보상 수령)
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
    /// 모든 태스크의 완료 여부 확인 → 퀘스트 완료 판정
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
    /// 외부에서 퀘스트 진행 신호가 들어오면 실행
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
    /// 보상 처리
    /// </summary>
    private void GiveReward()
    {
        // 이런식으로 구현? 일단 보상은 나중에
        //Shared.player_.AddGold(rewardGold); <- 골드는 어디에 만들지 고민해보기
        //Shared.player_.stat.addExp(rewardExp);

        for (int i = 0; i < rewardItemID.Count; i++)
        {
            int itemID = rewardItemID[i];
            Debug.Log($"{itemID} 지급");
        }
    }
}