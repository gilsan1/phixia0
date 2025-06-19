using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableSkill : TableBase
{
    [Serializable]
    public class SkillInfo
    {
        public int ID;
        public string Name;
        public float Damage;
        public float CoolDown;
        public float Range;
        public float Angle;
        public string Indicator;
        public string EffectPath;
        public string SkillType;
        public string IconPath;
    }

    private Dictionary<int, SkillBase> skillDict = new Dictionary<int, SkillBase>();

    public void Init_CSV(string fileName, int startRow, int startCol)
    {
        CSVReader reader = GetCSVReader(fileName);

        for (int row = startRow; row <= reader.row; row++)
        {
            SkillInfo info = new SkillInfo();
            if (!Read(reader, info, row, startCol))
                break;

            // info를 기반으로 실제 ItemBase 객체 생성
            SkillBase skill = CreateSkill(info);

            if (skill != null)
            {
                Debug.Log("스킬 생성");
                skillDict[info.ID] = skill;
            }
            else
            {
                Debug.Log("스킬 생성 실패");
            }
           
        }
    }

    protected bool Read(CSVReader reader, SkillInfo info, int row, int col)
    {
        if (!reader.reset_row(row, col))
            return false;

        reader.get(row, ref info.ID);
        reader.get(row, ref info.Name);
        reader.get(row, ref info.Damage);
        reader.get(row, ref info.CoolDown);
        reader.get(row, ref info.Range);
        reader.get(row, ref info.Angle);
        reader.get(row, ref info.Indicator);
        reader.get(row, ref info.EffectPath);
        reader.get(row, ref info.IconPath);
        reader.get(row, ref info.SkillType);

        return true;
    }

    private SkillBase CreateSkill(SkillInfo info)
    {
        if (!Enum.TryParse(info.Indicator, out eINDICATOR indicator))
            return null;

        if (!Enum.TryParse(info.SkillType, out eSKILL_TYPE skillType))
            return null;

        GameObject effectObject = Resources.Load<GameObject>(info.EffectPath);

        switch (skillType)
        {
            case eSKILL_TYPE.MELEE:
                return new MeleeSkill(info.Name, info.Damage, info.CoolDown, info.Range, info.Angle, indicator, effectObject, info.IconPath);
            case eSKILL_TYPE.MAGIC:
                return new MagicSkill(info.Name, info.Damage, info.CoolDown, info.Range, info.Angle, indicator, effectObject, info.IconPath);
            
            // 나중에 다른 타입도 추가
            
            default:
                return null;
        }
    }

    public SkillBase GetSkill(int id)
    {
        return skillDict.TryGetValue(id, out var skill) ? skill : null;
    }

    public void Save_Binary(string name) => Save_Binary(name, skillDict);
    public void Init_Binary(string name) => Load_Binary(name, ref skillDict);
}
