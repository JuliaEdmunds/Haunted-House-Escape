using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;


public class JigsawPuzzle : MonoBehaviour
{
    private const string JIGSAW_PUZZLE_SOLVED_KEY = "JigsawPuzzleSolved";

    [Header("Puzzle Elements")]
    [SerializeField] private MoveableObject m_PantryDoor;
    [SerializeField] private List<CollectableItem> m_PuzzlePieces;

    [Header("Audio")]
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_OpenSound;

    private void Awake()
    {
        CollectableItem.OnItemCollected += OnItemCollected;
    }

    private void OnItemCollected(CollectableItem item)
    {
        if (m_PuzzlePieces.Contains(item))
        {
            m_PuzzlePieces.Remove(item);
            item.gameObject.SetActive(false);
        }

        if (m_PuzzlePieces.Count == 0)
        {
            FactDB.SetBoolFact(JIGSAW_PUZZLE_SOLVED_KEY, true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // TODO: Add an animation for the puzzle pieces to fly into the puzzle
        if ((other.CompareTag("Player") && !m_PantryDoor.Locked))
        {
            m_AudioSource.PlayOneShot(m_OpenSound);
        }
    }
}

