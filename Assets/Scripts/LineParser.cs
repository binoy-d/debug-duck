using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineParser : MonoBehaviour
{
    [SerializeField] private string JUMPTO = "&";

    public static LineParser instance = null;

    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

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
}

/* % ignore
 * & JUMPTO linenumber 
 * + interactable
 * - noninteractable
 */