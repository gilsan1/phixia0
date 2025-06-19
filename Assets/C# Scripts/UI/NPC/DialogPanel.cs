using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogText;


    public void SetDialog(string name, string dialog)
    {
        nameText.text = name;
        dialogText.text = dialog;
    }
}
