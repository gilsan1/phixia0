using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestTask 
{
    public eTASKTYPE taskType;
    public string targetID;
    
    public abstract bool IsComplete { get; }
    public abstract void Progress(string id, int amount = 1);
  
}
