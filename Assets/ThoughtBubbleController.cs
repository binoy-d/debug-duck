using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ThoughtBubbleController : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 velocity;
    float xCoord = 0;
    float yCoord = 0;
    BoxCollider2D m_Collider;
    [SerializeField]
    int speed = 3;


    void Start()
    {

        velocity = new Vector3(0,0);

        m_Collider = GameObject.Find("thoughtbubblebox").GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        float newY = Convert.ToSingle(Mathf.PerlinNoise(xCoord,yCoord)-0.5);
        float newX = Convert.ToSingle(Mathf.PerlinNoise(yCoord,xCoord)-0.5);
        

        velocity = new Vector3(newX,newY);
        if(m_Collider.bounds.Contains(transform.position+velocity*Time.deltaTime*speed)){
            transform.position+=velocity*Time.deltaTime*speed;
        }
       
        
        xCoord+=Convert.ToSingle(0.001);
         
    }
}
