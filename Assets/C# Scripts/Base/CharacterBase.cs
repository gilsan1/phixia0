using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase
{
    public abstract void Play();
}

public class Player : CharacterBase
{
    public override void Play()
    {
    }
}

public class Monster : CharacterBase
{
    public override void Play()
    { }
}
