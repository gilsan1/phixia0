using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FactoryPattern // 패턴은 대부분 상속에 사용함 // Util
{
    public static CharacterBase CreateCharacter(eCHARACTER _e)
    {
        CharacterBase character = null; // 테이블로 대체

        switch (_e)
        {
            case eCHARACTER.eCHARACTER_PLAYER:
                character = new Player();
                break;
            case eCHARACTER.eCHARACTER_MONSTER:
                character = new Monster();
                break;
        }

        return character;
    }
}
