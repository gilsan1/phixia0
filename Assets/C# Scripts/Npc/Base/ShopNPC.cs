using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNpc : NPC
{
    public override void OnInteractable()
    {
        Debug.Log($"[NPC] {npcName}과 대화합니다.");
    }
}
