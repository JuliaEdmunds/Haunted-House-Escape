using System;
using TMPro;
using UnityEngine;


public class PuzzleManager : MonoBehaviour
{
    private const string SAFE_PUZZLE_CODE_KEY = "SafePuzzleCode";

    [Header("Safe Puzzle Elements")]
    [SerializeField] private TextMeshProUGUI m_1stDigit;
    [SerializeField] private TextMeshProUGUI m_2ndDigit;
    [SerializeField] private TextMeshProUGUI m_3rdDigit;

    private void Awake()
    {
        SetSafePuzzle();
    }

    private void SetSafePuzzle()
    {
        // Randomly generate the password
        System.Random rnd = new();
        string passcode = "";

        int numOfDigits = 3;
        for (int i = 0; i < numOfDigits; i++)
        {
            int value = rnd.Next(0, 10);

            string currentDigitString = value.ToString();

            switch (i)
            {
                default:
                case 0:
                    m_1stDigit.text = value.ToString();
                    break;
                case 1:
                    m_2ndDigit.text = value.ToString();
                    break;
                case 2:
                    m_3rdDigit.text = value.ToString();
                    break;
            }

            passcode += currentDigitString;
        }

        int passcodeInt = int.Parse(passcode);

        FactDB.SetIntFact(SAFE_PUZZLE_CODE_KEY, EOperation.Set, passcodeInt);
    }
}

