using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactRange = 2.5f;

    private NPC currentNPC;


    // Update is called once per frame
    void Update()
    {
        currentNPC = FindNPC();

        if (currentNPC != null && Input.GetKeyDown(KeyCode.E))
        {
            currentNPC.OnInteractable();
        }

        if (currentNPC != null && Shared.player_.interacState != eINTERACTIONSTATE.NONE && Input.GetKeyDown(KeyCode.Escape))
        {
            currentNPC.OffInteractable();
        }
    }


    private NPC FindNPC()
    {
        float minDistance = float.MaxValue;
        NPC nearNpc = null;

        for (int i = 0; i < Shared.npcList.Count; i++)
        {
            NPC npc = Shared.npcList[i];

            if (npc == null || !npc.gameObject.activeInHierarchy) continue; // npc가 null 이거나 하어이라키에 없으면 continue

            float distance = npc.GetInteractDistance(transform.position); // npc와 player의 거리를 계산

            if (npc.IsInteractable(transform.position) && distance < minDistance)
            {
                minDistance = distance;
                nearNpc = npc;
            }
        }
        return nearNpc;
    }
}
