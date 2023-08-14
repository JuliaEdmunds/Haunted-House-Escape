using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;


public class SafePuzzle : MonoBehaviour
{
    private string m_CorrectCode = "739";
    [SerializeField] private TextMeshProUGUI m_UserInput;

    public void CheckCode()
    {
        if (m_UserInput.text == m_CorrectCode)
        {
            UnlockSafe();
        }
        else
        {
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
        // Code to unlock the safe, maybe play an animation, sound effect, etc.
    }

    public void ResetCode()
    {
        m_UserInput.text = "";
    }
}
