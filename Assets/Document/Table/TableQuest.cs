using System.Collections.Generic;
using UnityEngine;

public class TableQuest : TableBase
{
    public class QuestInfo
    {
        public int ID;
        public string QuestTitle;
        public string Description;

        public int RewardGold;
        public int RewardExp;

        public List<QuestRewardItems> RewardItems = new List<QuestRewardItems>();

        public eTASKTYPE TaskType;
        public int TargetID;
        public int TargetAmount;
    }

    private List<QuestInfo> questList = new List<QuestInfo>();

    public void Init_CSV(string fileName, int startRow, int startCol)
    {
        CSVReader reader = GetCSVReader(fileName);

        for (int row = startRow; row <= reader.row; row++)
        {
            QuestInfo rowData = new QuestInfo();
            if (!Read(reader, rowData, row, startCol))
                break;

            questList.Add(rowData);
        }
    }

    protected bool Read(CSVReader reader, QuestInfo data, int row, int col)
    {
        if (!reader.reset_row(row, col)) return false;

        reader.get(row, ref data.ID);
        reader.get(row, ref data.QuestTitle);
        reader.get(row, ref data.Description);
        reader.get(row, ref data.RewardGold);
        reader.get(row, ref data.RewardExp);

        string rawReward = "";
        reader.get(row, ref rawReward);
        data.RewardItems = ParseRewardItems(rawReward);

        string rawTaskType = "";
        reader.get(row, ref rawTaskType);
        System.Enum.TryParse(rawTaskType, out data.TaskType);
        reader.get(row, ref data.TargetID);
        reader.get(row, ref data.TargetAmount);

        return true;
    }

    private List<QuestRewardItems> ParseRewardItems(string raw)
    {
        List<QuestRewardItems> result = new List<QuestRewardItems>();
        if (string.IsNullOrEmpty(raw)) return result;

        string[] tokens = raw.Split('|');
        for (int i = 0; i < tokens.Length; i++)
        {
            string[] parts = tokens[i].Split(':');
            if (parts.Length != 2) continue;

            if (System.Enum.TryParse(parts[0], out eQUESTREWARD rewardType))
            {
                if (int.TryParse(parts[1], out int id))
                {
                    result.Add(new QuestRewardItems(rewardType, id));
                }
            }
        }

        return result;
    }

    public List<QuestInfo> GetAll() => questList;

    public void Save_Binary(string name) => Save_Binary(name, questList);
    public void Init_Binary(string name) => Load_Binary(name, ref questList);
}