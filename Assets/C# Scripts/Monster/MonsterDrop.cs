using System;
using UnityEngine;

[Serializable]
public class MonsterDrop
{
    [SerializeField] public int itemId;

    [Range(0f, 1f)]
    public float dropProbability;

    public int minCount = 1;
    public int maxCount = 1;
}