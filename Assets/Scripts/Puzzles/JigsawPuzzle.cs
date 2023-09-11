using System.Collections.Generic;
using UnityEngine;

public class JigsawPuzzle : MonoBehaviour
{
    private const string JIGSAW_PUZZLE_SOLVED_KEY = "JigsawPuzzleSolved";
    private const string FINAL_KEY_COLLECTED = "IsFinalKeyCollected";
    private static readonly int ANIM_BOOL_HASH = Animator.StringToHash("isComplete");

    [Header("Puzzle Elements")]
    [SerializeField] private MoveableObject m_PantryDoor;
    [SerializeField] private List<CollectableItem> m_PuzzlePieces;
    [SerializeField] private CollectableItem m_Key;
    [SerializeField] private GameObject m_SpotLight;

    [Header("SFX & Animation")]
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_OpenSound;
    [SerializeField] private Animator m_Animator;
    [SerializeField] private AudioClip m_PickUpSound;

    private void Awake()
    {
        CollectableItem.OnItemCollected += OnItemCollected;
        MoveableObject.OnObjectUnlocked += OnObjectUnlocked;
    }

    private void OnItemCollected(CollectableItem item)
    {
        if (item == m_Key)
        {
            FactDB.SetBoolFact(FINAL_KEY_COLLECTED, true);
            m_SpotLight.SetActive(false);
            m_AudioSource.PlayOneShot(m_PickUpSound);
        }

        if (m_PuzzlePieces.Remove(item))
        {
            item.gameObject.SetActive(false);
            m_AudioSource.PlayOneShot(m_PickUpSound);
        }

        if (m_PuzzlePieces.Count == 0)
        {
            FactDB.SetBoolFact(JIGSAW_PUZZLE_SOLVED_KEY, true);
        }
    }

    private void OnObjectUnlocked(MoveableObject unlockedObject)
    {
        if (unlockedObject == m_PantryDoor)
        {
            m_Key.gameObject.SetActive(true);
            m_SpotLight.SetActive(true);

            m_AudioSource.PlayOneShot(m_OpenSound);
            m_Animator.enabled = true;
            m_Animator.SetBool(ANIM_BOOL_HASH, true);
        }
    }

    private void OnDestroy()
    {
        CollectableItem.OnItemCollected -= OnItemCollected;
        MoveableObject.OnObjectUnlocked -= OnObjectUnlocked;
    }
}
