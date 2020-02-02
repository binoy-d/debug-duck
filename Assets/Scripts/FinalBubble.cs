using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FinalBubble : MonoBehaviour
{
    [Header("Spawning")]
    [SerializeField] float maxY = 3f;
    public float startY = 0f;

    [Header("Movement")]
    [SerializeField] float pause_time = 0.5f;
    private bool can_move = true;

    [SerializeField] protected float spd = 1f;
    [SerializeField] int movement_scheme = 0;
    private int num_schemes = 3;

    //private float angle = 0f;
    [SerializeField] float sin_y_offset = 0f;
    //[SerializeField] float sin_freq = 10f;
    [SerializeField] float sin_amp = 5f;

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
    //private string alt_txt = "";
    private string[] alt_txt;
    private int curr_txt = 0;

    protected bool done_typing = false;
    [SerializeField] private float time_to_move_after_hit = 0.8f;
    protected bool hit = false;
    private bool complete = false;
    
    private string[] lines;

    [SerializeField] private Image black;

    void Start()
    {
        black = GameObject.FindGameObjectWithTag("BlackScreen").GetComponent<Image>();
        startY = Random.value * maxY * 2 - maxY;
        sin_y_offset = startY;
        transform.position = new Vector2(transform.position.x, startY);
    }

    void Update()
    {
        if (hit)
            time_to_move_after_hit -= Time.deltaTime;

        if (time_to_move_after_hit <= 0f)
        {
            can_move = true;
            hit = false;
            time_to_move_after_hit = 0.8f;
        }

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
    }

    private IEnumerator FadeBlack()
    {
        while (black.color.a < 1)
        {
            black.color = new Color(black.color.r,black.color.g,black.color.b, black.color.a+0.1f);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.5f);
        GameObject.Find("debug_duck_64").GetComponent<Controller>().ChangeSprite();
        while (black.color.a > 0)
        {
            black.color = new Color(black.color.r, black.color.g, black.color.b, black.color.a - 0.1f);
            yield return new WaitForSeconds(0.05f);
        }
        yield return null;
    }

    private void UpdateText()
    {
        if (use_alt)
        {
            current_txt = alt_txt[++curr_txt];
            use_alt = false;
            if (curr_txt+1  == alt_txt.Length)
            {
                sprite.color = Color.green;
                complete = true;
                StartCoroutine(FadeBlack());
            }
        }

        if (text_mesh.text != current_txt)
        {
            text_mesh.text = current_txt;
            width = text_mesh.preferredWidth > d_width ? d_width : text_mesh.preferredWidth;
            height = text_mesh.preferredHeight;

            Vector2 new_size = new Vector2(width * w_scale, height * h_scale);
            bubble.localScale = new_size;
        }
    }

    public void SetText(string txt)
    {
        alt_txt = txt.Split('/');
        TEXT = alt_txt[0];
        curr_txt = 0;
        use_alt = false;
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
            movement_scheme = 1;

            sprite.color = Color.red;
        }
    }

    protected IEnumerator TypeText()
    {
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < TEXT.Length; i++)
        {
            current_txt += TEXT[i];
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(pause_time);
        done_typing = true;

        yield return null;
    }

    private void AltText()
    {
        if (curr_txt + 1 == alt_txt.Length)
        {
            SetInteractable(false);
        }
        else
        {
            use_alt = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet" && isInteractable && done_typing && !hit)
        {
            Destroy(other.gameObject);
            can_move = false;
            hit = true;
            AltText();
            //SetInteractable(false);
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

    public bool GetHit()
    {
        return complete;
    }
}