using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhancerNPC : NPC
{
    public override void OnInteractable ()
    {
        Debug.Log($"[NPC] {npcName}�� ��ȭ�մϴ�.");
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
