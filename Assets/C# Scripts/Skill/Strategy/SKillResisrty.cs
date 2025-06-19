using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkillResistry
{
    private static Dictionary<eSKILL_TYPE, ISkillStrategy> strategyMap = new();


    static SkillResistry()
    {
        strategyMap[eSKILL_TYPE.MELEE] = new MeleeSkillStrategy();
        strategyMap[eSKILL_TYPE.MAGIC] = new MagicSkillStrategy();
        strategyMap[eSKILL_TYPE.BUFF] = new BuffSkillStrategy();
        strategyMap[eSKILL_TYPE.PROJECTILE] = new ProjectileSkillStrategy();
    }


    public static void Register(eSKILL_TYPE type, ISkillStrategy strategy)
    {
        strategyMap[type] = strategy;
    }

    public static ISkillStrategy GetStrategy(eSKILL_TYPE type)
    {
        return strategyMap.TryGetValue(type, out var strategy) ? strategy : null;
    }
}

