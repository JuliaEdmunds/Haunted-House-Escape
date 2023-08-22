using System.Collections.Generic;
using UnityEngine;


public class JigsawPuzzle : MonoBehaviour
{
    private const string JIGSAW_PUZZLE_SOLVED_KEY = "JigsawPuzzleSolved";
    private const string ANIM_BOOL_NAME = "isComplete";

    [Header("Puzzle Elements")]
    [SerializeField] private MoveableObject m_PantryDoor;
    [SerializeField] private List<CollectableItem> m_PuzzlePieces;

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
        if (m_PuzzlePieces.Contains(item))
        {
            m_PuzzlePieces.Remove(item);
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
            m_AudioSource.PlayOneShot(m_OpenSound);
            m_Animator.enabled = true;
            m_Animator.SetBool(ANIM_BOOL_NAME, true);
        }
    }
}

