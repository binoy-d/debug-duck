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
    
    private GameObject current;



    // Update is called once per frame
    void Update()
    {
        bool fire = Input.GetKeyDown("space");

        if (fire)
        {
            Vector3 pos = transform.position;
            pos[2] = 0;
            Shoot(pos);
        }
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(horizontal,vertical,0);

        Vector3 unmaxed = transform.position + (movement * Time.deltaTime * speed);
        Vector3 maxed = new Vector3(Mathf.Min(Mathf.Max(unmaxed[0],-8.25f),8.25f),
            Mathf.Min(Mathf.Max(unmaxed[1], -4.5f),4.5f),0);

        transform.position = maxed;
    }

    private void Shoot(Vector3 t)
    {
        current = Instantiate(_bullet,t,Quaternion.identity);
    }
}
