using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestPanel : MonoBehaviour
{
    public Quest quest;
    

    public TextMeshProUGUI title;
    public TextMeshProUGUI description;

    public string currentAmount;
    public string targetAmount;

    
    private void Init()
    {
        quest = GameManager.Instance.questManager.GetQuest(9001);

        title.text = quest.questTitle;
        description.text = quest.description;

        //currentAmount.text = quest[0].
    }
    // Start is called before the first frame update

    private void Update()
    {
        if ( Input.GetKeyDown(KeyCode.H))
        {
            Init();
        }
        
    }
}
