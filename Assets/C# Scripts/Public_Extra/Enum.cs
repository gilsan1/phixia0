public enum eCHARACTER  // ĳ���Ͱ� �÷��̾� or ����
{
    PLAYER,
    MONSTER,
    NPC
}

public enum eAI 
{
    NONE,
    CREATE,
    SEARCH,
    MOVE,
    RESET,
}
/// <summary>
/// NPC ���� 
/// </summary>
public enum eNPC_TYPE // NPC Ÿ��
{
    NORMAL, // �Ϲ�
    SHOP, // ����
    ENHANCER, // ��ȭ
    QUEST // ����Ʈ
}

public enum eINTERACTIONSTATE
{
    NONE,
    NORMAL,
    SHOPPING,
    ENHANCING
}

/// <summary>
/// ����Ʈ
/// </summary>
public enum eTASKTYPE
{
    KILL,
    TALK,
    VISIT,
    COLLECT
}

public enum eQUESTSTATE
{
    PROGRESS,
    COMPLETE,
    REWARD
}

public enum eQUESTTYPE
{
    MAINQUEST,
    SUBQUEST
}

public enum eQUESTREWARD
{
    WEAPON,
    ARMOR,
    //EARING,
    POTION,
    SCROLL
}

/// <summary>
/// ĳ���� ���� (�ִϸ����Ϳ��� ���)
/// </summary>
public enum eCHARACTER_STATE  // ĳ���� ����
{
    IDLE, // 0
    WALK, // 1
    RUN, // 2
    ATTACK, // 3
    SKILL1, // 4
    SKILL2 // 5
}

public enum eMONSTER_STATE  // ���� ����
{  
    IDLE, // 0
    CHASE, // 1
    ATTACK, // 2
    SKILL1, // 3
    SKILL2, // 4
    DIE, // 5
    NONE    //6
}

public enum eSKILL_TYPE  //��ų�� Ÿ��(����, ����, ����, ����ü)
{
    MELEE,
    MAGIC,
    BUFF,
    PROJECTILE
}

public enum eWeaponType  // ���� Ÿ��
{
    MELEE,
    RANGED,
}

public enum eSKILL_SLOT  // ��ų ����
{
    SKILL1,
    SKILL2,
    SKILL3,
    SKILL4  // BUFF
}


public enum eINDICATOR  // ���� Ÿ��
{
    FAN,
    RECTANGLE,
    CIRCLE
}

public enum eITEMTYPE  // ������ ���� ���/�Һ�/��Ÿ
{
    NONE,
    EQUIP,
    CONSUMABLE,
    ETC
}

public enum eITEMEQUIP_TYPE  // ���������� ���� ����/��
{
    NONE,
    WEAPON,
    ARMOR,
    EARING
}

public enum eARMORTYPE
{
    NONE,
    HELMET,
}

public enum eITEMCONSUM_TYPE  // �Һ������ ���� HPȸ�� / MPȸ�� / ���� ����
{
    NONE,
    POTION,
    SCROLL,
    BOX
}

public enum eINVENTORY_SLOT_TYPE
{
    NONE,

    // ���
    EQUIP_WEAPON,
    EQUIP_HELMET,
    EQUIP_EARING,

    // �Һ�
    CONSUMABLE_POTION,
    CONSUMABLE_SCROLL,
    CONSUMABLE_BOX,

    // ��Ÿ
    ETC
}

public enum eBUFF_TYPE
{
    NONE,
    ATK_UP,
    DEF_UP
}