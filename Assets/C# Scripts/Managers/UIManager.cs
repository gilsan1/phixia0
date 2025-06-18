using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Player Reference")]
    [SerializeField] private Player player;

    private float hpCurrent;
    private float mpCurrent;

    [Header("UI - Status Bars")]
    [SerializeField] private Image hpBar;
    [SerializeField] private Image mpBar;

    [Header("UI - Toggle Windows")]
    [SerializeField] private GameObject statInfo;
    [SerializeField] public GameObject inventory;

    [Header("UI - Ingame")]
    [SerializeField] private SkillPanel skillPanel;
    //[SerializeField] private QuickSlot[] quickSlots;

    [SerializeField] public ItemTooltip ItemTooltip;

    [Header("NPC Dialog")]
    [SerializeField] private GameObject dialogPrefab;
    [SerializeField] private DialogPanel currentDialog;

    [SerializeField] private GameObject exitButtonPrefab;
    [SerializeField] private GameObject shopButtonPrefab;
    [SerializeField] private GameObject enhanceButtonPrefab;
    [SerializeField] private GameObject questButtonPrefab;

    [SerializeField] private Transform uiRoot;
    [SerializeField] private Transform buttonRoot;
    private List<GameObject> buttonList;

    [Header("UI - Root Group")]
    [SerializeField] private CanvasGroup inGameGroup;

    private void Awake()
    {
        // Shared 접근 제거 → GameManager 구조 기준으로 작동
        if (player == null)
            player = FindObjectOfType<Player>();

        buttonList = new List<GameObject>();
    }

    private void Start()
    {
        StartCoroutine(UpdatePlayerUI());
    }

    /// <summary>
    /// 플레이어 HP/MP UI 실시간 갱신
    /// </summary>
    private IEnumerator UpdatePlayerUI()
    {
        while (true)
        {
            if (player != null && player.stat != null)
            {
                hpCurrent = hpBar.fillAmount;
                float targetHpRatio = player.stat.currentHP / player.stat.Base_maxHP;
                hpBar.fillAmount = Mathf.Lerp(hpCurrent, targetHpRatio, Time.deltaTime * 30f);

                mpCurrent = mpBar.fillAmount;
                float targetMpRatio = player.stat.currentMP / player.stat.Base_maxMp;
                mpBar.fillAmount = Mathf.Lerp(mpCurrent, targetMpRatio, Time.deltaTime * 30f);
            }

            yield return null;
        }
    }

    /// <summary>
    /// 스킬 쿨타임 UI 갱신
    /// </summary>
    public void TriggerSkillCooldown(int index)
    {
        skillPanel?.TriggerCooldown(index);
    }


    /// <summary>
    /// 스탯 창 토글
    /// </summary>
    public void OpenCloseStatInfo()
    {
        statInfo.SetActive(!statInfo.activeSelf);
    }

    /// <summary>
    /// 인벤토리 창 토글
    /// </summary>
    public void OpenCloseInventory()
    {
        inventory.SetActive(!inventory.activeSelf);
    }

    public void ShowNpcDialog(string npcName, string npcDialog, Transform npcAnchor)
    {
        Debug.Log($"[ShowNpcDialog] 호출됨: {npcName}"); // 호출 확인

        if (dialogPrefab == null)
        {
            Debug.LogError("[ShowNpcDialog] dialogPrefab이 할당되지 않았습니다.");
            return;
        }

        if (currentDialog != null)
            Destroy(currentDialog.gameObject);

        GameObject dialog = Instantiate(dialogPrefab, uiRoot);
        RectTransform rt = dialog.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector3(0f, 300f, 0f);
        Debug.Log("[ShowNpcDialog] 프리팹 인스턴스화 완료");

        currentDialog = dialog.GetComponent<DialogPanel>();
        currentDialog.SetDialog(npcName, npcDialog);
    }


    /// <summary>
    /// NPC 관련 UI
    /// </summary>
    /// <param name="npcType"></param>
    public void CreateNpcButton(eNPC_TYPE npcType)
    {
        ClearButton();

        // 조건 버튼만 생성
        if (npcType == eNPC_TYPE.SHOP)
            buttonList.Add(Instantiate(shopButtonPrefab, buttonRoot));
        else if (npcType == eNPC_TYPE.ENHANCER)
            buttonList.Add(Instantiate(enhanceButtonPrefab, buttonRoot));
        else if (npcType == eNPC_TYPE.QUEST)
            buttonList.Add(Instantiate(questButtonPrefab, buttonRoot));

            // 공통 Exit 버튼은 항상 생성
            buttonList.Add(Instantiate(exitButtonPrefab, buttonRoot));
    }



    private void ClearButton()
    {
        for (int i = 0; i < buttonList.Count; i++)
        {
            if (buttonList[i] != null)
                Destroy(buttonList[i].gameObject);
        }
        buttonList.Clear();
    }


    public void CloseDialog()
    {
        if (currentDialog != null)
            Destroy(currentDialog.gameObject);
    }


    public void SetInGameUIVisible(bool isVisible)
    {
        inGameGroup.alpha = isVisible ? 1f : 0f;
        inGameGroup.interactable = isVisible;
        inGameGroup.blocksRaycasts = isVisible;
    }



    public void ShowQuestListUI(List<Quest> questList, QuestNPC npc)
    {

    }
}