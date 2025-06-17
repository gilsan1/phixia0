using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountableTask : QuestTask
{
    public int targetAmount;
    public int currentAmount;


    public override bool IsComplete => currentAmount >= targetAmount;
      
    public CountableTask(eTASKTYPE taskType, string targetID, int targetAmount)
    {
        this.taskType = taskType;
        this.targetID = targetID;
        this.targetAmount = targetAmount;
        this.currentAmount = 0;
    }

    public override void Progress(string target, int amount)
    {
        if (IsComplete) return;

        if (target == targetID)
        {
            currentAmount += amount;

            if (IsComplete)
                Debug.Log("¿Ï·á");
        }
    }

}
