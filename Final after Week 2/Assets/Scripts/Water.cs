using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

    SpriteRenderer rend;
    BoxCollider2D col;
    public Color waterColor;
    public Color iceColor;
    public Color electricColor;
    public bool electrified;
    public float iceTimerMax;
    float iceTimer;
    public float electricTimerMax;
    float electricTimer;

	// Use this for initialization
	void Start () {
        rend = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
        col.isTrigger = true;
        rend.color = waterColor;
	}
	
	// Update is called once per frame
	void Update () {
        if(iceTimer < 0){
            col.isTrigger = true;
        }
        if (electricTimer < 0)
        {
            electrified = false;
        }
        if(iceTimer < 0 && electricTimer < 0){
            rend.color = waterColor;
        }
        iceTimer -= Time.deltaTime;
        electricTimer -= Time.deltaTime;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pointer")
        {
            rend.color = iceColor;
            col.isTrigger = false;
            electrified = false;
            iceTimer = iceTimerMax;
        }

        if (collision.gameObject.tag == "Lightning")
        {
            rend.color = electricColor;
            electrified = true;
            col.isTrigger = true;
            electricTimer = electricTimerMax;
        }
    }
}
