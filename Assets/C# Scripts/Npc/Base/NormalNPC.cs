using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalNPC : NPC
{
    [TextArea(2, 5)]
    public string dialogLine = "NPC Test";

    [SerializeField] private Transform cameraAnchor;
    [SerializeField] private Transform dialogAnchor;
    public override void OnInteractable()
    {
        Shared.player_.SetInteractionState(eINTERACTIONSTATE.NORMAL);
        Debug.Log($"[NPC] {npcName}�� ��ȭ�մϴ�.");

        CameraController.Instance.FocusOn(cameraAnchor, ShowDialog);

        /// ���� ǥ��
        /*CameraController.Instance.FocusOn(cameraAnchor, () =>
        {
            GameManager.Instance.uiManager.ShowNpcDialog(npcName, dialogLine, dialogAnchor);
        });*/
    } 


    private void ShowDialog()
    {
        GameManager.Instance.uiManager.ShowNpcDialog(npcName, dialogLine, dialogAnchor);
        GameManager.Instance.uiManager.CreateNpcButton(this.npcType);
    }

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }
}
