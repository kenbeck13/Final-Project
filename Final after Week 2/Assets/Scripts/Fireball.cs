using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

    Rigidbody2D rb;
    public float speed;
    public float killTimer;
    public bool isMovingRight;

	// Use this for initialization
	void Start () {
        if(!isMovingRight){
            speed *= -1;
        }

        if(killTimer < 0){
            Destroy(gameObject);
        }

        rb = GetComponent<Rigidbody2D>();
        killTimer -= Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
        rb.velocity = transform.right * speed * Time.deltaTime;
	}
}
