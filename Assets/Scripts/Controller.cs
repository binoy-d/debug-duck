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
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(horizontal,vertical,0);

        transform.position += (movement * Time.deltaTime * speed);

        bool fire = Input.GetKeyDown("space");
        if (fire) { Shoot(transform.position); }
        
    }

    private void Shoot(Vector3 t)
    {
        current = Instantiate(_bullet,t,Quaternion.identity);
    }
}
