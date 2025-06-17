using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public UIManager uiManager { get; private set; }
    public SkillExecuter skillExecuter { get; private set; }
    public EffectManager effectManager { get; private set; }

    public QuestManager questManager { get; private set; }
    public QuestSystem questSystem { get; private set; }
    public static TableMgr tableMgr;

    


    private void Awake()
    {
        if (Instance != null && Instance == this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        uiManager = FindAnyObjectByType<UIManager>();
        skillExecuter = FindAnyObjectByType<SkillExecuter>();
        effectManager = FindAnyObjectByType<EffectManager>();
        questManager = FindAnyObjectByType<QuestManager>();


        questSystem = new QuestSystem();

        tableMgr = new TableMgr();
    }
}
