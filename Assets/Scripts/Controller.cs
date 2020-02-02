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
    public  GameObject programmer;
    private GameObject current;

    private bool can_move_horizontal = false;
    private bool can_move_vertical = false;
    private float shoot_delay = 0f;
    private float t = 0f;

    void Update()
    {
        t -= Time.deltaTime;

        bool fire = Input.GetKeyDown("space");

        if (fire && t <= 0f)
        {
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
        current = Instantiate(_bullet,t,Quaternion.identity);
    }

    public void SetCanMove(bool a, bool b)
    {
        can_move_horizontal = a;
        can_move_vertical = b;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag!="Bullet"){
            programmer.UpdateHealth();
        }
    }
    public void SetShootDelay(float d)
    {
        shoot_delay = d;
    }

    
}
