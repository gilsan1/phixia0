using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;
using Unity.VisualScripting;
using TMPro;

public class PlayerStatusInfo : MonoBehaviour
{
    //private Player player;

    /// <summary>
    /// MainStats
    /// </summary>
    [Header("MainStats")]
    [SerializeField] private TextMeshProUGUI str_Text;
    [SerializeField] private TextMeshProUGUI dex_Text;
    [SerializeField] private TextMeshProUGUI int_Text;
    [SerializeField] private TextMeshProUGUI luk_Text;

    /// <summary>
    /// ParameterStats
    /// </summary>
    [Header("ParameterStats")]
    [SerializeField] private TextMeshProUGUI hpValue;
    [SerializeField] private TextMeshProUGUI mpValue;
    [SerializeField] private TextMeshProUGUI attack;
    [SerializeField] private TextMeshProUGUI def;




    // Start is called before the first frame update
    private void Awake()
    {
        //player = FindAnyObjectByType<Player>();
    }

    private void Start()
    {
        RefreshStatUI(); // 초기 1회 반영

        // Shared.player_가 null일 수 있으므로 안전하게 처리
        if (Shared.player_ != null)
        {
            Shared.player_.OnStatChange += RefreshStatUI;
        }
    }

    public void RefreshStatUI()
    {
        if (Shared.player_ == null || Shared.player_.stat == null) return;

        str_Text.text = Shared.player_.stat.STR.ToString();
        dex_Text.text = Shared.player_.stat.DEX.ToString();
        int_Text.text = Shared.player_.stat.INT.ToString();
        luk_Text.text = Shared.player_.stat.LUK.ToString();

        hpValue.text = Shared.player_.stat.Base_maxHP.ToString();
        mpValue.text = Shared.player_.stat.Base_maxMp.ToString();
        attack.text = Shared.player_.stat.Total_Atk.ToString();
        def.text = Shared.player_.stat.Total_Def.ToString();
    }
}
