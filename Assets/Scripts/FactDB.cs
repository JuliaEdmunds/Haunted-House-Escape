using System.Collections.Generic;


public static class FactDB
{
    private static Dictionary<string, int> m_Facts = new();

    public static bool GetBoolFact(string key)
    {
        if (m_Facts.TryGetValue(key, out int value) && value != 0)
        {
            return true;
        }
        else
        {
            return false;
        }       
    }

    public static int GetIntFact(string key)
    {
        if (m_Facts.TryGetValue(key, out int value))
        {
            return value;
        }
        else
        {
            return 0;
        }
    }

    public static void SetBoolFact(string key, bool isFullfilled)
    {
        m_Facts[key] = isFullfilled ? 1 : 0;
    }

    public static void SetIntFact(string key, EOperation operationType, int value)
    {
        switch (operationType)
        {
            default:
            case EOperation.Add:
                m_Facts[key] += value;
                break;
            case EOperation.Subtract:
                m_Facts[key] -= value;
                break;
            case EOperation.Set:
                m_Facts[key] = value;
                break;
        }
    }
}