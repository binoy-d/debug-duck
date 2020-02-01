using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeechBubble : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float pause_time = 0.5f;
    private bool can_move = false;

    [SerializeField] float spd = 5f;
    private RectTransform position;
    
    private float angle = 0f;
    [SerializeField] float sin_y_offset = 0f;
    [SerializeField] float sin_freq = 10f;
    [SerializeField] float sin_amp = 1f;

    [Header("ResizingBubble")]
    [SerializeField] private float d_width = 5.5f;
    [SerializeField] private float offset = 1f;
    private string TEXT = "";
    private string current_txt = "";
    private TextMeshProUGUI text_mesh;
    private float width;
    private float height;

    private RectTransform bubble;
    private BoxCollider2D b_collider;

    void Start()
    {
        position = GetComponent<RectTransform>();
        text_mesh = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        text_mesh.text = "";
        bubble = transform.GetChild(0).GetComponent<RectTransform>();
        b_collider = GetComponent<BoxCollider2D>();
    }
    
    void Update()
    {
        Move();
        UpdateText();
    }

    private void Move()
    {
        if (!can_move)
            return;

        angle += sin_freq * Time.deltaTime;
        angle = angle % 360;
        position.anchoredPosition = new Vector2(position.anchoredPosition.x - spd*Time.deltaTime, sin_y_offset + sin_amp*Mathf.Sin(angle));
    }

    private void UpdateText()
    {
        if (text_mesh.text != current_txt)
        {
            text_mesh.text = current_txt;
            width = text_mesh.preferredWidth > d_width ? d_width : text_mesh.preferredWidth;
            height = text_mesh.preferredHeight;

            Vector2 new_size = new Vector2(width + offset, height + offset);
            bubble.sizeDelta = new_size;
            b_collider.size = new_size;
        }
    }

    public void SetText(string txt)
    {
        TEXT = txt;
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < TEXT.Length; i++)
        {
            current_txt += TEXT[i];
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(pause_time);
        can_move = true;

        yield return null;
    }
}
