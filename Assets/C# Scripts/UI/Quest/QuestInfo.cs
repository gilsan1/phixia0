using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestInfo : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public TextMeshProUGUI currentAmount;
    public TextMeshProUGUI targetAmount;
    public Button acceptButton;

    private Quest quest;

    /// <summary>
    /// 외부에서 퀘스트 정보를 주입받아 초기화
    /// </summary>
    public void Init(Quest quest)
    {
        this.quest = quest;

        title.text = quest.questTitle;
        description.text = quest.description;

        if (quest.tasks.Count > 0 && quest.tasks[0] is CountableTask countable)
        {
            currentAmount.text = countable.currentAmount.ToString();
            targetAmount.text = countable.targetAmount.ToString();
        }
        else
        {
            currentAmount.text = "-";
            targetAmount.text = "-";
        }

        if (acceptButton != null)
        {
            acceptButton.onClick.RemoveAllListeners();
            acceptButton.onClick.AddListener(() =>
            {
                GameManager.Instance.questManager.AcceptQuest(quest.id);
                gameObject.SetActive(false); // 혹은 Destroy(gameObject);
            });
        }
    }
}