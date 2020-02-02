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
    [SerializeField]
    BoxCollider2D m_Collider;
    [SerializeField]
    int speed = 8;



    void Start()
    {

        velocity = new Vector3(0,0);

    }

    // Update is called once per frame
    void Update()
    {
        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        float newY = Convert.ToSingle(Mathf.PerlinNoise(xCoord,yCoord)-0.5);
        float newX = Convert.ToSingle(Mathf.PerlinNoise(yCoord,xCoord)-0.5);

        velocity = new Vector3(newX,newY);
        if(m_Collider.bounds.Contains(transform.position+velocity*Time.deltaTime*speed)){
            print("yes, i want to move");
            transform.position+=velocity*Time.deltaTime*speed;
        }
       
        
        xCoord+=Convert.ToSingle(0.001);
         
    }
}
