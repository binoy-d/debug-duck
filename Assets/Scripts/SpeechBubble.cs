﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeechBubble : MonoBehaviour
{
    [Header("Spawning")]
    [SerializeField] float maxY = 3f;
    private float startY = 0f;

    [Header("Movement")]
    [SerializeField] float pause_time = 0.5f;
    private bool can_move = false;

    [SerializeField] float spd = 5f;
    [SerializeField] int movement_scheme = 0;
    private int num_schemes = 2;

    private float angle = 0f;
    [SerializeField] float sin_y_offset = 0f;
    [SerializeField] float sin_freq = 10f;
    [SerializeField] float sin_amp = 1f;

    [Header("ResizingBubble")]
    [SerializeField] private float d_width = 4f;
    [SerializeField] private float w_scale = 2f;
    [SerializeField] private float h_scale = 4f;
    private TextMeshPro text_mesh;
    private float width;
    private float height;

    private Transform bubble;
    private BoxCollider2D b_collider;

    [Header("State")]
    [SerializeField] private bool isInteractable = false;
    private string TEXT = "";
    private string current_txt = "";
    private bool use_alt = false;
    private string alt_txt = "";

    void Start()
    {
        text_mesh = transform.GetChild(1).GetComponent<TextMeshPro>();
        text_mesh.text = "";
        bubble = transform.GetChild(0).GetComponent<Transform>();
        b_collider = transform.GetChild(0).GetComponent<BoxCollider2D>();

        if (!isInteractable)
            b_collider.enabled = false;

        startY = Random.value * maxY*2 - maxY;
        sin_y_offset = startY;
        transform.position = new Vector2(transform.position.x, startY);

        movement_scheme = (int)(Random.value * num_schemes + 1) % num_schemes;
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

        if (movement_scheme == 0)
            transform.position = new Vector2(transform.position.x - spd * Time.deltaTime, sin_y_offset + sin_amp * Mathf.Sin(angle));
        else if (movement_scheme == 1)
            transform.position = new Vector2(transform.position.x - spd * Time.deltaTime, sin_y_offset + sin_amp/Mathf.PI * Mathf.Asin(Mathf.Sin(angle)));
    }

    private void UpdateText()
    {
        if (use_alt)
            current_txt = alt_txt;

        if (text_mesh.text != current_txt)
        {
            text_mesh.text = current_txt;
            width = text_mesh.preferredWidth > d_width ? d_width : text_mesh.preferredWidth;
            height = text_mesh.preferredHeight;

            Vector2 new_size = new Vector2(width*w_scale, height*h_scale);
            bubble.localScale = new_size;
        }
    }

    public void SetText(string txt)
    {
        string[] lines = txt.Split('/');
        TEXT = lines[0].Substring(1);
        alt_txt = lines[1].Substring(0, lines[1].Length - 1);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}