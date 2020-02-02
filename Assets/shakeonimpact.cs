using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakeonimpact : MonoBehaviour
{
    private bool shaking;
    [SerializeField]
    private int shakeTime = 3;

    private int countdown = 3000;
    private float speed = 1.0f;
    private float amount = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        shaking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(shaking){
            transform.position = new Vector3(Mathf.Sin(Time.time * speed) * amount, 0 , 0);
            if(countdown <= 0){
                shaking = !shaking;
            }
            countdown--;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        shaking = true;

    }
}
