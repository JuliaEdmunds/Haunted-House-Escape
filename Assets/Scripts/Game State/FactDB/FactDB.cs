using System;
using System.Collections.Generic;

public static class FactDB
{
    private static readonly Dictionary<string, int> m_Facts = new();

    private static readonly Dictionary<string, string> m_StringFacts = new();

    public static bool GetBoolFact(string key)
    {
        return m_Facts.TryGetValue(key, out int value) && value != 0;
    }

    public static int GetIntFact(string key)
    {
        return m_Facts.TryGetValue(key, out int value) ? value : 0;
    }

    public static string GetStringFact(string key)
    {
        return m_StringFacts.TryGetValue(key, out string value) ? value : string.Empty;
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

            case EOperation.Set:
                m_Facts[key] = value;
                break;

            default:
                throw new ArgumentException($"{operationType} is not supported type of FactDB SetIntFact");
        }
    }

    public static void SetStringFact(string key, string value)
    {
        m_StringFacts[key] = value;
    }
}
