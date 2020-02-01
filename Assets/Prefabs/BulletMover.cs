using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BulletMover : MonoBehaviour
{
    private Vector3 velocity; 
    

    [SerializeField]
    private float speed = 5;
    private float aliveTime = 5;

    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(speed, 0);
        Destroy(gameObject, aliveTime);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position+=velocity*Time.deltaTime*speed;
        
    }
}
