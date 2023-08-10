using UnityEngine;

public class GUIConfig
{
    public bool ShouldShowMsg;

    private GUIStyle m_GuiStyle;
    private string m_Msg;

    public GUIConfig()
    {
        SetupGui();
    }

    //configure the style of the GUI
    private void SetupGui()
    {
        m_GuiStyle = new()
        {
            fontSize = 16,
            fontStyle = FontStyle.Bold
        };
        m_GuiStyle.normal.textColor = Color.white;
        m_Msg = "Press E/Fire1 to Open";
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
        if (ShouldShowMsg) // show on-screen prompts to user for guide.
        {
            GUI.Label(new Rect(50, Screen.height - 50, 200, 50), m_Msg, m_GuiStyle);
        }
    }

    private string GetGuiMsg(bool isOpen)
    {
        return isOpen ? "Press E/Fire1 to Close" : "Press E/Fire1 to Open";
    }
}

