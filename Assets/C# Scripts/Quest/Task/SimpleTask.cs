using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SimpleTask : QuestTask
{
    private bool isComplete = false;
    public override bool IsComplete => isComplete;

    public SimpleTask(eTASKTYPE taskType, string targetID)
    {
        this.taskType = taskType;
        this.targetID = targetID;
        isComplete = false;
    }


    public override void Progress(string target, int amount = 1)
    {
        if (isComplete) return;

        if (target == targetID)
        {
            isComplete = true;
            Debug.Log("SimpleTask ¿Ï·á");
        }
    }
}
