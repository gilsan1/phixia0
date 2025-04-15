using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FactoryPattern // ������ ��κ� ��ӿ� ����� // Util
{
    public static CharacterBase CreateCharacter(eCHARACTER _e)
    {
        CharacterBase character = null; // ���̺�� ��ü

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
