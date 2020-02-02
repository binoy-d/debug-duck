using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeechBubble : MonoBehaviour
{
    [Header("Spawning")]
    [SerializeField] float maxY = 3f;
    public float startY = 0f;

    [Header("Movement")]
    [SerializeField] float pause_time = 0.5f;
    private bool can_move = true;

    [SerializeField] protected float spd = 5f;
    [SerializeField] int movement_scheme = 0;
    private int num_schemes = 3;

    //private float angle = 0f;
    [SerializeField] float sin_y_offset = 0f;
    //[SerializeField] float sin_freq = 10f;
    [SerializeField] float sin_amp = 1f;

    [Header("ResizingBubble")]
    [SerializeField] protected float d_width = 4f;
    [SerializeField] protected float w_scale = 2f;
    [SerializeField] protected float h_scale = 4f;
    [SerializeField] protected TextMeshPro text_mesh;
    protected float width;
    protected float height;

    [SerializeField] protected Transform bubble;
    [SerializeField] protected BoxCollider2D b_collider;
    [SerializeField] protected SpriteRenderer sprite;

    [Header("State")]
    [SerializeField] protected bool isInteractable = false;
    protected string TEXT = "";
    protected string current_txt = "";
    private bool use_alt = false;
    private string alt_txt = "";

    protected bool done_typing = false;
    [SerializeField] private float time_to_move_after_hit = 2f;
    protected bool hit = false;
    

    private string[] lines;
    void Start()
    {
        startY = Random.value * maxY*2 - maxY;
        sin_y_offset = startY;
        transform.position = new Vector2(transform.position.x, startY);
    }

    void Update()
    {
        if (hit)
            time_to_move_after_hit -= Time.deltaTime;

        if (time_to_move_after_hit <= 0f)
            can_move = true;

        Move();
        UpdateText();
    }

    private void Move()
    {
        if (!can_move || !done_typing)
            return;

        //angle += sin_freq * Time.deltaTime;
        //angle = angle % 360;

        if (movement_scheme == 0)
            transform.position = new Vector2(transform.position.x - spd * Time.deltaTime, sin_y_offset);
        else if (movement_scheme == 1)
            transform.position = new Vector2(transform.position.x - spd * Time.deltaTime, sin_y_offset + sin_amp * Mathf.Sin(transform.position.x));
        else if (movement_scheme == 2)
            transform.position = new Vector2(transform.position.x - spd * Time.deltaTime, sin_y_offset + sin_amp/Mathf.PI * Mathf.Asin(Mathf.Sin(transform.position.x)));
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
        if (isInteractable)
        {
            lines = txt.Split('/');
            TEXT = lines[0];
            alt_txt = lines[1];
        }
        else
        {
            TEXT = txt;
        }
        StartCoroutine(TypeText());
    }

    public void SetCanMove(bool canmove)
    {
        can_move = canmove;
    }

    public void SetY(float y)
    {
        startY = y;
        sin_y_offset = startY;
        transform.position = new Vector2(transform.position.x, y);
    }

    public void SetInteractable(bool interactable)
    {
        isInteractable = interactable;
        b_collider.enabled = true;
        if (!isInteractable)
        {
            movement_scheme = 0;
        }
        else
        {
            movement_scheme = (int)(Random.Range(1,num_schemes));

            sprite.color = Color.red;
        }
    }

    protected IEnumerator TypeText()
    {
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < TEXT.Length; i++)
        {
            current_txt += TEXT[i];
            yield return new WaitForSeconds(0.03f);
        }
        yield return new WaitForSeconds(pause_time);
        done_typing = true;

        yield return null;
    }

    private void AltText()
    {
        use_alt = true ;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet" && isInteractable && done_typing)
        {
            Destroy(other.gameObject);
            //Destroy(gameObject);
            can_move = false;
            hit = true;
            AltText();
            SetInteractable(false);
            sprite.color = Color.green;
        }
        else if (other.tag == "End")
        {
            if (isInteractable)
            {
                GameObject.Find("GameController").GetComponent<GameController>().UpdateHealth();
            }
            Destroy(gameObject);
        }
        else if (other.tag == "Bullet")
            Destroy(other.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "TopBoundary")
        {
            SetY(startY - 1f);
        }
        else if (collision.tag == "BotBoundary")
        {
            SetY(startY + 1f);
        }
    }
}