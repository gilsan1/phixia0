using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : CharacterBase
{
    public delegate void StatChangeHandler();
    public event StatChangeHandler OnStatChange;

    public void StatChanged()
    {
        OnStatChange?.Invoke();
    }
}
