using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public abstract class AI_Base : MonoBehaviour
{
    //protected CharacterBase character;

    protected int targetIndex = 0;

    protected eAI aiState = eAI.NONE;

   /* public void Init(CharacterBase _characterBase)
    {
        character = _characterBase;
    }*/

    public void State()
    {
        switch(aiState)
        {
            case eAI.NONE:
                break;
            case eAI.CREATE:
                break;
            case eAI.SEARCH:
                break;
            case eAI.MOVE:
                break;
            case eAI.RESET:
                break;
        }
    }

    protected virtual void Create() { aiState = eAI.SEARCH; }

    protected virtual void Search() { aiState = eAI.RESET; }

    protected virtual void Resets() { aiState = eAI.NONE; }
}
