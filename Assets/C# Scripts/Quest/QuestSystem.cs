using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class QuestSystem
{
    private QuestManager questManager;


    public void OnKillEvent(int monsterID, int amount)
    {
        List<Quest> activeQuests = GameManager.Instance.questManager.GetActivesQuests();

        for (int i = 0; i < activeQuests.Count; i++)
        {
            Quest quest = activeQuests[i];
            quest.CheckProgress(eTASKTYPE.KILL, monsterID, 1);
        }
    }
    
    public void OnTalkEvent(int npcID)
    {
        List<Quest> activeQuests = GameManager.Instance.questManager.GetActivesQuests();

        for (int i = 0; i < activeQuests.Count; i++)
        {
            Quest quest = activeQuests[i];
            quest.CheckProgress(eTASKTYPE.TALK, npcID, 1);
        }
    }


    public void OnCollectEvent(int itemID, int amount)
    {
        List<Quest> activeQuests = GameManager.Instance.questManager.GetActivesQuests();

        for (int i = 0; i < activeQuests.Count; i++)
        {
            Quest quest = activeQuests[i];
            quest.CheckProgress(eTASKTYPE.COLLECT, itemID, amount);
        }
    }

    public void OnVisitEvent(int areaID)
    {
        List<Quest> activeQuests = GameManager.Instance.questManager.GetActivesQuests();

        for (int i = 0; i < activeQuests.Count; i++)
        {
            Quest quest = activeQuests[i];
            quest.CheckProgress(eTASKTYPE.VISIT, areaID, 1);
        }
    }
    public void Init(QuestManager manger)
    {
        questManager = manger;
    }
}
