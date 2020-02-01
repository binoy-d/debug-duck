using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeechBubble : MonoBehaviour
{
    [SerializeField] private float d_width = 11f;
    [SerializeField] private float offset = 1f;
    private string TEXT = "";
    private TextMeshProUGUI text_mesh;
    private float width;
    private float height;

    private RectTransform bubble;

    void Start()
    {
        text_mesh = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        text_mesh.text = "";
        bubble = transform.GetChild(0).GetComponent<RectTransform>();
    }
    
    void Update()
    {
        if (text_mesh.text != TEXT)
        {
            text_mesh.text = TEXT;
            width = text_mesh.preferredWidth > d_width ? d_width : text_mesh.preferredWidth;
            height = text_mesh.preferredHeight;

            bubble.sizeDelta = new Vector2(width+offset, height+offset);
        }
    }

    public void SetText(string txt)
    {
        TEXT = txt;
    }

    private void SetBubbleSize()
    {
    }
}
