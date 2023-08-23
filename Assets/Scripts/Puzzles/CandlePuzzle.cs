using UnityEngine;
using UnityEngine.UI;

public class CandlePuzzle : MonoBehaviour
{
    private const string CANDLE_PUZZLE_SOLVED_KEY = "CandlePuzzleSolved";
    private const string CANDLE_PUZZLE_CODE_KEY = "CandlePuzzleCode";

    [Header("Puzzle Elements")]
    [SerializeField] private Slider m_Slider1;
    [SerializeField] private Slider m_Slider2;
    [SerializeField] private Slider m_Slider3;


    [Header("Audio")]
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_SuccessSound;

    public void CheckCode()
    {
        int slider1 = (int)m_Slider1.value;
        int slider2 = (int)m_Slider2.value;
        int slider3 = (int)m_Slider3.value;
        int code = slider1 * 100 + slider2 * 10 + slider3;

        if (code != FactDB.GetIntFact(CANDLE_PUZZLE_CODE_KEY))
        {
            return;
        }

        UnlockDoor();
    }

    private void UnlockDoor()
    {
        m_AudioSource.PlayOneShot(m_SuccessSound);
        FactDB.SetBoolFact(CANDLE_PUZZLE_SOLVED_KEY, true);

        // Disable ClickOverlayButton in children
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<ClickOverlayButton>(out ClickOverlayButton component))
            {
                component.IsEnabled = false;
            }
        }
    }
}
