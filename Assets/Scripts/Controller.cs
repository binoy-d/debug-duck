using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField]
    private float speed = 10;
    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private GameObject gameController;

    private GameObject current;

    [SerializeField] private bool can_move_horizontal = false;
    [SerializeField] private bool can_move_vertical = false;
    private float shoot_delay = 0f;
    private float t = 0f;
    
    private AudioSource fireData;

    [SerializeField] private Sprite girl;
    private bool can_shoot = true;

    void Start(){
        fireData = GetComponent<AudioSource>();
        
    }
    void Update()
    {
        t -= Time.deltaTime;

        bool fire = Input.GetKeyDown("space");

        if (fire && t <= 0f)
        {
            fireData.Play(0);
            Vector3 pos = transform.position;
            pos[2] = 0;
            Shoot(pos);
            t = shoot_delay;
        }

        Movement();
    }

    private void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (!can_move_horizontal)
            horizontal = 0;
        if (!can_move_vertical)
            vertical = 0;

        Vector3 movement = new Vector3(horizontal, vertical, 0);

        Vector3 unmaxed = transform.position + (movement * Time.deltaTime * speed);
        Vector3 maxed = new Vector3(Mathf.Min(Mathf.Max(unmaxed[0], -8.25f), 8.25f),
            Mathf.Min(Mathf.Max(unmaxed[1], -4.5f), 4.5f), 0);

        transform.position = maxed;
    }

    private void Shoot(Vector3 t)
    {
        if (!can_shoot)
            return;
        current = Instantiate(_bullet,t,Quaternion.identity);
    }

    public void SetCanMove(bool a, bool b)
    {
        can_move_horizontal = a;
        can_move_vertical = b;
    }
    /*
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag!="Bullet"){
            gameController.GetComponent<GameController>().UpdateHealth();
        }
    }
    */
    public void SetShootDelay(float d)
    {
        shoot_delay = d;
    }

    public void ChangeSprite()
    {
        GetComponent<SpriteRenderer>().sprite = girl;
        GetComponent<SpriteRenderer>().flipX = false;
        can_shoot = false;

        transform.position = new Vector2(-8, -2);
        transform.localScale = new Vector2(1, 1);
    }
}
