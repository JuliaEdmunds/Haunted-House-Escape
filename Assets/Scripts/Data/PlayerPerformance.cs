using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class PlayerPerformance : MonoBehaviour
{
    private const string TIME_PASSED_KEY = "TimePassed";
    private const string PLAYER_NAME_KEY = "PlayerName";

    [SerializeField] private TextMeshProUGUI m_PlayernameText;
    [SerializeField] private TextMeshProUGUI m_TimePassedText;

    private void Start()
    {
        string playerName = FactDB.GetStringFact(PLAYER_NAME_KEY);
        var op = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("TextTable", "Headline");
        string currentLine = op.Result;

        m_PlayernameText.text = $"{playerName} {currentLine}";
        m_TimePassedText.text = CheckTimepassed();
    }

    private string CheckTimepassed()
    {
        TimeSpan time = TimeSpan.FromSeconds(FactDB.GetIntFact(TIME_PASSED_KEY));
        return time.ToString(@"hh\:mm\:ss");
    }

    
}

