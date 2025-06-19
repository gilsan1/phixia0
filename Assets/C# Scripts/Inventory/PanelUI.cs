using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelUI : MonoBehaviour
{
    [SerializeField] GameObject[] panels;

    [SerializeField] Toggle[] toggles;

    private void Awake()
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            int index = i;
            toggles[i].onValueChanged.AddListener((isOn) => 
            { 
                if (isOn) 
                    ShowPanel(index); 
            });
        }
    }
    private void Start()
    {
        if (panels.Length > 0)
        {
            ShowPanel(0);
        }
    }
    private void ShowPanel(int index)
    {
        for (int i = 0;i < panels.Length;i++)
        {
            panels[i].SetActive(i == index);
        }
    }
}
