using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public static class Shared
{
    //�÷��̾� ���� - FSM ��� ������ ����
    public static Player player_;

    //���� ����Ʈ - ��ų ���� üũ �� ���� ���� ��
    public static List<MonsterBase> enemyList = new List<MonsterBase>();

    public static PlayerEquip playerEquip;

    public static BuffManager buffMgr;

    public static List<NPC> npcList = new List<NPC>();

    public static void SetPlayer(Player p) => player_ = p;
  
}
