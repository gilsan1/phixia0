using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class QuestSystem
{
    private QuestManager questManager;


    /// <summary>
    /// 킬 퀘스트
    /// </summary>

    public void OnKillEvent(int monsterID, int amount)
    {
        List<Quest> activeQuests = GameManager.Instance.questManager.GetActivesQuests();

        for (int i = 0; i < activeQuests.Count; i++)
        {
            Quest quest = activeQuests[i];
            quest.CheckProgress(eTASKTYPE.KILL, monsterID, 1);
        }
    }


    /// <summary>
    /// NPC랑 대화
    /// </summary>
    public void OnTalkEvent(int npcID)
    {
        List<Quest> activeQuests = GameManager.Instance.questManager.GetActivesQuests();

        for (int i = 0; i < activeQuests.Count; i++)
        {
            Quest quest = activeQuests[i];
            quest.CheckProgress(eTASKTYPE.TALK, npcID, 1);
        }
    }


    /// <summary>
    /// 모으기 퀘스트
    /// </summary>
    public void OnCollectEvent(int itemID, int amount)
    {
        List<Quest> activeQuests = GameManager.Instance.questManager.GetActivesQuests();

        for (int i = 0; i < activeQuests.Count; i++)
        {
            Quest quest = activeQuests[i];
            quest.CheckProgress(eTASKTYPE.COLLECT, itemID, amount);
        }
    }


    /// <summary>
    /// 목표지점
    /// </summary>
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

    public void CompleteQuest(Quest quest)
    {
        GameManager.Instance.questManager.OnQuestComplete(quest);
        GiveReward(quest);
        quest.MarkRewarded();
    }

    public void GiveReward(Quest quest)
    {
        for (int i = 0; i < quest.rewardItems.Count; i++)
        {
            QuestRewardItems reward = quest.rewardItems[i];
            ItemBase item = null;

            switch (reward.itemType)
            {
                case eQUESTREWARD.WEAPON:
                    item = TableMgr.Instance.weaponItem.GetItem(reward.id);
                    break;
                case eQUESTREWARD.ARMOR:
                    item = TableMgr.Instance.armorItem.GetItem(reward.id);
                    break;
                case eQUESTREWARD.POTION:
                    item = TableMgr.Instance.potionItem.GetItem(reward.id);
                    break;
                case eQUESTREWARD.SCROLL:
                    item = TableMgr.Instance.scrollItem.GetItem(reward.id);
                    break;
            }

            if (item != null)
                InventorySystem.Instance.TryAddItem(item);
            else
                Debug.Log("Reward Item is Null");
        }
    }
}

