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
    int speed = 3;


    void Start()
    {
        velocity = new Vector3(0,0);
    }

    // Update is called once per frame
    void Update()
    {
        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        velocity = new Vector3(Convert.ToSingle(Mathf.PerlinNoise(xCoord, yCoord)-0.5),Convert.ToSingle(Mathf.PerlinNoise(-xCoord,-yCoord)-0.5));

        transform.position+=velocity*Time.deltaTime*speed;
        xCoord+=Convert.ToSingle(0.001);
        yCoord+=Convert.ToSingle(0.001);

    }
}
