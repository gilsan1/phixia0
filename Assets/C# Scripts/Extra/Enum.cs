public enum eCHARACTER
{
    eCHARACTER_PLAYER,
    eCHARACTER_MONSTER,
}


public enum eAI
{
    NONE,
    CREATE,
    SEARCH,
    MOVE,
    RESET,
}
public enum eCHARACTER_STATE
{
    IDLE,
    WALK,
    RUN,
    JUMP,
    ATTACK,
}

public enum eMonster_STATE
{
    IDLE, // 0
    CHASE, // 1
    ATTACK, // 2
    SKILL, // 3
    DIE // 4
}

public enum eSKILL_TYPE
{
    MELEE,
    RANGED,
    BUFF
}

public enum eWeaponType
{
    MELEE,
    RANGED,
    MAGIC
}
