using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public static class Shared
{
    //플레이어 참조 - FSM 등에서 빠르게 접근
    public static Player player_;

    //몬스터 리스트 - 스킬 범위 체크 및 전투 판정 등
    public static List<MonsterBase> enemyList = new List<MonsterBase>();

    public static PlayerEquip playerEquip;

    public static BuffManager buffMgr;

    public static List<NPC> npcList = new List<NPC>();

    public static void SetPlayer(Player p) => player_ = p;
  
}
