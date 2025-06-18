using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestRewardItems
{

    public eQUESTREWARD itemType;
    public int id;


    public QuestRewardItems(eQUESTREWARD itemType, int id)
    {
        this.itemType = itemType;
        this.id = id;
    }

}
