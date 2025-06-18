public enum eCHARACTER  // 캐릭터가 플레이어 or 몬스터
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
/// NPC 관련 
/// </summary>
public enum eNPC_TYPE // NPC 타입
{
    NORMAL, // 일반
    SHOP, // 상점
    ENHANCER, // 강화
    QUEST // 퀘스트
}

public enum eINTERACTIONSTATE
{
    NONE,
    NORMAL,
    SHOPPING,
    ENHANCING
}

/// <summary>
/// 퀘스트
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
/// 캐릭터 상태 (애니메이터에서 사용)
/// </summary>
public enum eCHARACTER_STATE  // 캐릭터 상태
{
    IDLE, // 0
    WALK, // 1
    RUN, // 2
    ATTACK, // 3
    SKILL1, // 4
    SKILL2 // 5
}

public enum eMONSTER_STATE  // 몬스터 상태
{  
    IDLE, // 0
    CHASE, // 1
    ATTACK, // 2
    SKILL1, // 3
    SKILL2, // 4
    DIE, // 5
    NONE    //6
}

public enum eSKILL_TYPE  //스킬의 타입(근접, 마법, 버프, 투사체)
{
    MELEE,
    MAGIC,
    BUFF,
    PROJECTILE
}

public enum eWeaponType  // 무기 타입
{
    MELEE,
    RANGED,
}

public enum eSKILL_SLOT  // 스킬 슬롯
{
    SKILL1,
    SKILL2,
    SKILL3,
    SKILL4  // BUFF
}


public enum eINDICATOR  // 장판 타입
{
    FAN,
    RECTANGLE,
    CIRCLE
}

public enum eITEMTYPE  // 아이템 구분 장비/소비/기타
{
    NONE,
    EQUIP,
    CONSUMABLE,
    ETC
}

public enum eITEMEQUIP_TYPE  // 장착아이템 구분 무기/방어구
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

public enum eITEMCONSUM_TYPE  // 소비아이템 구분 HP회복 / MP회복 / 버프 물약
{
    NONE,
    POTION,
    SCROLL,
    BOX
}

public enum eINVENTORY_SLOT_TYPE
{
    NONE,

    // 장비
    EQUIP_WEAPON,
    EQUIP_HELMET,
    EQUIP_EARING,

    // 소비
    CONSUMABLE_POTION,
    CONSUMABLE_SCROLL,
    CONSUMABLE_BOX,

    // 기타
    ETC
}

public enum eBUFF_TYPE
{
    NONE,
    ATK_UP,
    DEF_UP
}