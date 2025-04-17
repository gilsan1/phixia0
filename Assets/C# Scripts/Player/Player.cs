using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterCombat combat;
    [SerializeField] private CharacterStat stat;
    [SerializeField] private Weapon weapon;




    private void Awake()
    {
        stat.SetStat(1, 100f, 100f, 10f);
    }



}
