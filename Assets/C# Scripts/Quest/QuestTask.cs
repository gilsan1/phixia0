using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestTask 
{
    public eTASKTYPE taskType;
    public int targetID;
    
    public abstract bool IsComplete { get; }
    public abstract void Progress(int id, int amount = 1);
  
}
