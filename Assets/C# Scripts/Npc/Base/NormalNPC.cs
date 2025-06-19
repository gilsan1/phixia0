using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalNPC : NPC
{
    public override void OnInteractable()
    {
        Shared.player_.SetInteractionState(eINTERACTIONSTATE.NORMAL);
        Debug.Log($"[NPC] {npcName}과 대화합니다.");

        CameraController.Instance.FocusOn(cameraAnchor, ShowDialog);
    } 
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
