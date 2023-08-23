using TMPro;
using UnityEngine;

public class SafePuzzle : MonoBehaviour
{
    private const string SAFE_PUZZLE_CODE_KEY = "SafePuzzleCode";
    private const string SAFE_PUZZLE_SOLVED_KEY = "SafePuzzleSolved";

    [Header("Puzzle Elements")]
    [SerializeField] private MoveableObject m_SafeDoor;
    [SerializeField] private TextMeshProUGUI m_UserInput;
    [SerializeField] private CollectableItem m_Key;
    [SerializeField] private GameObject m_SpotLight;
        
    [Header("Audio")]
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_ClickySound;
    [SerializeField] private AudioClip m_ErrorSound;
    [SerializeField] private AudioClip m_SuccessSound;
    [SerializeField] private AudioClip m_PickUpSound;

    private void Awake()
    {
        CollectableItem.OnItemCollected += OnItemCollected;
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

    public void PlayClicky()
    {
        m_AudioSource.PlayOneShot(m_ClickySound);
    }

    private void UnlockSafe()
    {
        m_SafeDoor.Unlock();
        m_AudioSource.PlayOneShot(m_SuccessSound);
        m_Key.gameObject.SetActive(true);
    }

    private void OnItemCollected(CollectableItem item)
    {
        if (item == m_Key)
        {
            FactDB.SetBoolFact(SAFE_PUZZLE_SOLVED_KEY, true);
            m_SpotLight.SetActive(false);
            m_AudioSource.PlayOneShot(m_PickUpSound);

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

    public void ResetCode()
    {
        m_UserInput.text = "";
    }
}
