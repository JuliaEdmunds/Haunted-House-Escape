using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class PuzzleManager : MonoBehaviour
{
    public const string SAFE_PUZZLE_CODE_KEY = "SafePuzzleCode";
    public const string CANDLE_PUZZLE_CODE_KEY = "CandlePuzzleCode";

    [Header("Safe Puzzle Elements")]
    [SerializeField] private List<GameObject> m_PosterPossiblePositions;
    [SerializeField] private GameObject m_HiddenPoster;
    [SerializeField] private TextMeshProUGUI m_1stDigit;
    [SerializeField] private TextMeshProUGUI m_2ndDigit;
    [SerializeField] private TextMeshProUGUI m_3rdDigit;

    [Header("Puzzle Pieces Elements")]
    [SerializeField] private List<GameObject> m_PuzzlePossibleBedroomPositions;
    [SerializeField] private List<GameObject> m_PuzzlePossibleOtherPositions;
    [SerializeField] private List<CollectableItem> m_Puzzles;

    [Header("Last Puzzle")]
    [SerializeField] private MoveableObject m_LastDoor;
    
    private bool m_FinalDoorUnlocked = false;

    private void Start()
    {
        SetSafePuzzle();
        SetPuzzlePieces();
        SetCandlePuzzle();
    }

    private void SetSafePuzzle()
    {
        // Randomly generate the password
        string passcode = "";

        int numOfDigits = 3;
        for (int i = 0; i < numOfDigits; i++)
        {
            int value = Random.Range(0, 10);

            string currentDigitString = value.ToString();

            switch (i)
            {
                case 0:
                    m_1stDigit.text = value.ToString();
                    break;

                case 1:
                    m_2ndDigit.text = value.ToString();
                    break;

                case 2:
                    m_3rdDigit.text = value.ToString();
                    break;

                default:
                    throw new System.ArgumentException($"{i} is not a supported digit for a passcode");
            }

            passcode += currentDigitString;
        }

        int passcodeInt = int.Parse(passcode);

        FactDB.SetIntFact(SAFE_PUZZLE_CODE_KEY, EOperation.Set, passcodeInt);

        // Makes the GameObject posterParent the parent of the GameObject poster
        GameObject posterParent = GetRandomPos(m_PosterPossiblePositions);
        m_HiddenPoster.transform.parent = posterParent.transform;
        m_HiddenPoster.transform.localPosition = Vector3.zero;
    }

    private void SetPuzzlePieces()
    {
        // Attach puzzle pieces to random positions
        for (int i = 0; i < m_Puzzles.Count; i++)
        {
            GameObject puzzleParent;

            if (i == 0 || i == 1)
            {
                puzzleParent = GetRandomPos(m_PuzzlePossibleBedroomPositions);
            }
            else
            {
                puzzleParent = GetRandomPos(m_PuzzlePossibleOtherPositions);
            }

            CollectableItem currentPuzzle = m_Puzzles[i];
            currentPuzzle.transform.parent = puzzleParent.transform;
            currentPuzzle.transform.localPosition = Vector3.zero;
            currentPuzzle.transform.localRotation = Quaternion.identity;
        }
    }

    private void SetCandlePuzzle()
    {
        // Code is fixed based on the candles size
        const int passcode = 132;

        FactDB.SetIntFact(CANDLE_PUZZLE_CODE_KEY, EOperation.Set, passcode);
    }

    private GameObject GetRandomPos(List<GameObject> possiblePositions)
    {
        int randomIndex = Random.Range(0, possiblePositions.Count);
        GameObject position = possiblePositions[randomIndex];
        possiblePositions.RemoveAt(randomIndex);
        return position;
    }
}
