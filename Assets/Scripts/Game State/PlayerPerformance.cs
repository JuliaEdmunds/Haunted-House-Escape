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

    private const float LOW_TIME = 300f;
    private const float MID_TIME = 900f;

    [Header("Text Elements")]
    [SerializeField] private TextMeshProUGUI m_Headline;
    [SerializeField] private TextMeshProUGUI m_TimePassedText;
    [SerializeField] private TextMeshProUGUI m_ItemsCollectedText;

    [Header("Image Elements")]
    [SerializeField] private Image m_LampImage;
    [SerializeField] private Image m_RingImage;
    [SerializeField] private Image m_RemoteImage;

    private string m_CurrentLine;

    private IEnumerator Start()
    {
        string playerName = FactDB.GetStringFact(PLAYER_NAME_KEY);
        if (string.IsNullOrEmpty(playerName))
        {
            playerName = "Bond";
        }

        // Print headline
        yield return GetLocalizedText("Headline");
        string headline = m_CurrentLine;
        m_Headline.text = $"{playerName} {headline}";

        // Print time elapsed
        string timePassed = CheckTimePassed();

        yield return GetLocalizedText("TimeEntry");
        string firstLine = m_CurrentLine;

        yield return CheckTimeText();
        string secondLine = m_CurrentLine;

        string timePassedText = $"{firstLine} {timePassed}, {playerName} {secondLine}";
        m_TimePassedText.text = timePassedText;

        // Print items collected
        ShowItemsCollected();

        yield return CheckItemsCollectedText();
        string itemsCollected = m_CurrentLine;

        m_ItemsCollectedText.text = $"{playerName} {itemsCollected}";
    }

    private string CheckTimePassed()
    {
        TimeSpan time = TimeSpan.FromSeconds(FactDB.GetIntFact(TIME_PASSED_KEY));
        return _ = $"{time.Hours:D2}h:{time.Minutes:D2}m:{time.Seconds:D2}s";
    }

    private IEnumerator CheckTimeText()
    {
        float time = FactDB.GetIntFact(TIME_PASSED_KEY); ;

        if (time < LOW_TIME)
        {
            yield return GetLocalizedText("ShortTime");
        }
        else if (time < MID_TIME)
        {
            yield return GetLocalizedText("MediumTime");
        }
        else
        {
            yield return GetLocalizedText("LongTime");
        }
    }

    private IEnumerator CheckItemsCollectedText()
    {
        int numItems = FactDB.GetIntFact(ECollectable.Lamp.ToString()) 
            + FactDB.GetIntFact(ECollectable.Ring.ToString()) 
            + FactDB.GetIntFact(ECollectable.Remote.ToString());

        switch (numItems)
        {
            case 0:
                yield return GetLocalizedText("NoItems");
                break;

            case 1:
                yield return GetLocalizedText("OneItem");
                break;

            case 2:
                yield return GetLocalizedText("TwoItems");
                break;

            case 3:
                yield return GetLocalizedText("AllItems");
                break;

            default:
                throw new ArgumentOutOfRangeException($"Unsoprted number of collected items: {numItems}");
        }

    }

    private void ShowItemsCollected()
    {
        if (FactDB.GetBoolFact(ECollectable.Lamp.ToString()))
        {
            m_LampImage.gameObject.SetActive(true);
        }

        if (FactDB.GetBoolFact(ECollectable.Ring.ToString()))
        {
            m_RingImage.gameObject.SetActive(true);
        }

        if (FactDB.GetBoolFact(ECollectable.Remote.ToString()))
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
