using TMPro;
using UnityEngine;


public class SafePuzzle : MonoBehaviour
{
    private const string SAFE_PUZZLE_CODE_KEY = "SafePuzzleCode";
    private const string SAFE_PUZZLE_SOLVED_KEY = "SafePuzzleSolved";

    [SerializeField] private TextMeshProUGUI m_UserInput;
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_ErrorSound;
    [SerializeField] private AudioClip m_SuccessSound;

    // TODO: Randomly generate the password
    // TODO: Move the puzzle fact into a specific puzzle manager class
    private void Start()
    {
        FactDB.SetIntFact(SAFE_PUZZLE_CODE_KEY, EOperation.Set, 739);
    }

    public void CheckCode()
    {
        int.TryParse(m_UserInput.text, out int number);

        if (number == FactDB.GetIntFact(SAFE_PUZZLE_CODE_KEY))
        {
            UnlockSafe();
        }
        else
        {
            m_AudioSource.PlayOneShot(m_ErrorSound);
            ResetCode();
        }
    }

    public void AddCode(string num)
    {
        if (m_UserInput.text.Length >= 3)
        {
            ResetCode();
        }

        m_UserInput.text += num;
    }

    private void UnlockSafe()
    {
        //TODO: play an animation of the safe opening and reveal the key
        m_AudioSource.PlayOneShot(m_SuccessSound);
        FactDB.SetBoolFact(SAFE_PUZZLE_SOLVED_KEY, true);
    }

    public void ResetCode()
    {
        m_UserInput.text = "";
    }
}
