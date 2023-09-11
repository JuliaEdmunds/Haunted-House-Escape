using UnityEngine;

public class GUIConfig
{
    public bool ShouldShowMsg;

    private GUIStyle m_GuiStyle;
    private string m_Msg;

    private const string OPEN_MESSAGE = "Press E/Fire1 to Open";
    private const string CLOSE_MESSAGE = "Press E/Fire1 to Close";

    public GUIConfig()
    {
        SetupGui();
    }

    // Configure the style of the GUI
    private void SetupGui()
    {
        m_GuiStyle = new()
        {
            fontSize = 16,
            fontStyle = FontStyle.Bold
        };
        m_GuiStyle.normal.textColor = Color.white;
        m_Msg = OPEN_MESSAGE;
    }

    public void ShowInteractMsg(bool isOpen)
    {
        m_Msg = GetGuiMsg(isOpen);
    }

    public void ShowInteractMsg(string msg)
    {
        m_Msg = msg;
    }

    public void OnGUI()
    {
        // Show on-screen prompts to user for guide
        if (ShouldShowMsg) 
        {
            GUI.Label(new Rect(50, Screen.height - 50, 200, 50), m_Msg, m_GuiStyle);
        }
    }

    private string GetGuiMsg(bool isOpen)
    {
        return isOpen ? CLOSE_MESSAGE : OPEN_MESSAGE;
    }
}
