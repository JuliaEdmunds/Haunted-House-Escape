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

    [Header("Text Elements")]
    [SerializeField] private TextMeshProUGUI m_Headline;
    [SerializeField] private TextMeshProUGUI m_TimePassedText;
    [SerializeField] private TextMeshProUGUI m_ItemsCollectedText;

    [Header("Image Elements")]
    [SerializeField] private Image m_LampImage;
    [SerializeField] private Image m_RingImage;
    [SerializeField] private Image m_RemoteImage;

    private string m_CurrentLine;

    private void Start()
    {
        string playerName = FactDB.GetStringFact(PLAYER_NAME_KEY);
        if (string.IsNullOrEmpty(playerName))
        {
            playerName = "Bond";
        }

        StartCoroutine(GetLocalizedText("Headline"));

        // Print headline
        m_Headline.text = $"{playerName} {m_CurrentLine}";

        // Print time elapsed
        string timePassed = CheckTimePassed();
        StartCoroutine(GetLocalizedText("TimeEntry"));
        string firstLine = m_CurrentLine;
        StartCoroutine(GetLocalizedText("TimeDescription"));
        string secondLine = m_CurrentLine;

        string timePassedText = $"{firstLine} {timePassed}, {playerName} {secondLine}";
        m_TimePassedText.text = timePassedText;

        // Print items collected
        ShowItemsCollected();
        string itemsCollected = CheckItemsCollectedText();
        m_ItemsCollectedText.text = $"{playerName} {itemsCollected}";
    }

    private string CheckTimePassed()
    {
        TimeSpan time = TimeSpan.FromSeconds(FactDB.GetIntFact(TIME_PASSED_KEY));
        string formattedTime;
        return formattedTime = $"{time.Hours:D2}h:{time.Minutes:D2}m:{time.Seconds:D2}s";
    }

    private string CheckItemsCollectedText()
    {
        string itemsCollected = "";

        int numItems = FactDB.GetIntFact(ECollectable.Lamp.ToString()) + FactDB.GetIntFact(ECollectable.Ring.ToString()) + FactDB.GetIntFact(ECollectable.Remote.ToString());

        switch (numItems)
        {
            default:
            case 0:
                StartCoroutine(GetLocalizedText("NoItems"));
                itemsCollected = m_CurrentLine;
                break;
            case 1:
                StartCoroutine(GetLocalizedText("OneItem"));
                itemsCollected = m_CurrentLine;
                break;
            case 2:
                StartCoroutine(GetLocalizedText("TwoItems"));
                itemsCollected = m_CurrentLine;
                break;
            case 3:
                StartCoroutine(GetLocalizedText("AllItems"));
                itemsCollected = m_CurrentLine;
                break;
        }

        return itemsCollected;
    }

    private void ShowItemsCollected()
    {
        if (FactDB.GetIntFact(ECollectable.Lamp.ToString()) > 0)
        {
            m_LampImage.gameObject.SetActive(true);
        }

        if (FactDB.GetIntFact(ECollectable.Ring.ToString()) > 0)
        {
            m_RingImage.gameObject.SetActive(true);
        }

        if (FactDB.GetIntFact(ECollectable.Remote.ToString()) > 0)
        {
            m_RemoteImage.gameObject.SetActive(true);
        }
    }

    private IEnumerator GetLocalizedText(string tableKey)
    {
        var op = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("TextTable", $"{tableKey}");

        while (!op.IsDone)
        {
            yield return null;
        }

        m_CurrentLine = op.Result;
    }
}

