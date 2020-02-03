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
        return s.Substring(1).StartsWith("+");
    }

    public float TimeBeforeLine(string s)
    {
        return (float)(int.Parse(s[0] + ""));
    }
}

/* % ignore
 * + interactable
 * - noninteractable
 */