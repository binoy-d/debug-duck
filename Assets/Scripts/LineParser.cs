using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineParser : MonoBehaviour
{
    public string ParseLine(string s)
    {
        if (s.StartsWith("%"))
            return "";
        else
            return s;
    }

    public bool LineIsInteractable(string s)
    {
        return s.StartsWith("+");
    }

    public bool IsSimultaneous(string s)
    {
        return s[1] == '+';
    }
}

/* % ignore
 * + interactable
 * - noninteractable
 */