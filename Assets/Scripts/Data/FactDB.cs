using System;
using System.Collections.Generic;

public static class FactDB
{
    private static Dictionary<string, int> m_Facts = new();

    private static Dictionary<string, string> m_StringFacts = new();

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

    public static string GetStringFact(string key)
    {
        if (m_StringFacts.TryGetValue(key, out string value))
        {
            return value;
        }
        else
        {
            return string.Empty;
        }
    }

    public static void SetBoolFact(string key, bool isFulfilled)
    {
        m_Facts[key] = isFulfilled ? 1 : 0;
    }

    public static void SetIntFact(string key, EOperation operationType, int value)
    {
        m_Facts.TryGetValue(key, out int currentValue);

        switch (operationType)
        {
            case EOperation.Add:
                m_Facts[key] = currentValue + value;
                break;
            case EOperation.Subtract:
                m_Facts[key] = (int)MathF.Max(currentValue - value, 0);
                break;
            default:
            case EOperation.Set:
                m_Facts[key] = value;
                break;
        }
    }

    public static void SetStringFact(string key, string value)
    {
        m_StringFacts[key] = value;
    }
}