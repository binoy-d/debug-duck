using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : MonoBehaviour
{
    private Vector3 velocity; 
    [SerializeField]
    private float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position+=velocity*Time.deltaTime*speed;
    }
}
