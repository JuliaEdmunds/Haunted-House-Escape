using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroController : MonoBehaviour
{
    private const string GAMEPLAY_SCENE_NAME = "Gameplay";
    private const string PLAYER_NAME_KEY = "PlayerName";

    [SerializeField] private GameObject m_TextScreen;
    [SerializeField] private TextMeshProUGUI m_IntroText;
    [SerializeField] private TMP_InputField m_InputField;
    [SerializeField] private Button m_StartButton;

    private IEnumerator Start()
    {
        m_IntroText.text = string.Empty;
        m_TextScreen.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        var op = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("TextTable", "Intro");

        while (!op.IsDone)
        {
            yield return null;
        }

        string currentLine = op.Result;
        yield return PrintText(currentLine);

        yield return new WaitForSeconds(1.5f);

        m_InputField.gameObject.SetActive(true);
        m_StartButton.gameObject.SetActive(true);
    }

    private IEnumerator PrintText(string line)
    {
        for (int i = 0; i <= line.Length; i++)
        {
            string currentText = line.Substring(0, i);
            m_IntroText.text = currentText;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void OnInputFieldChanged()
    {
        string playerName = m_InputField.text.Trim();
        m_StartButton.interactable = !string.IsNullOrEmpty(playerName);
    }

    public void OnStartButtonClicked()
    {
        FactDB.SetStringFact(PLAYER_NAME_KEY, m_InputField.text);
        SceneManager.LoadScene(GAMEPLAY_SCENE_NAME);
    }
}
