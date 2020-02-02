using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakeonimpact : MonoBehaviour
{
    private bool shaking;
    private int countdown = 300;
    private float speed = 20.0f;
    private float amount = 0.2f;
    private float oldY;
    // Start is called before the first frame update
    void Start()
    {
        shaking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(shaking){
            float oldX = transform.position[0];
            float oldZ = transform.position[2];
            transform.position= new Vector3(oldX, oldY+Mathf.Sin(speed*Time.time)*amount , oldZ);
            if(countdown <= 0){
                shaking = !shaking;
            }
            countdown--;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        oldY = transform.position[1];
        if(!shaking){
            print("I'm hit!");
            shaking = true;
        }
    }
}
