using UnityEngine;
using System.Collections;

public abstract class CharacterBase : MonoBehaviour
{
    public CharacterStat stat { get; protected set; }
    public string charcterName { get; protected set; }
    public eCHARACTER characType { get; protected set; }

    // ISkillSystem, Stat�� Player, Monster���� ����
 
    protected virtual void Awake()
    {
        
    }
}