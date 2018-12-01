using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

    SpriteRenderer rend;
    BoxCollider2D col;
    public Color waterColor;
    public Color iceColor;
    public Color electricColor;
    public float iceTimerMax;
    float iceTimer;
	Conductor attachedConductor;

	// Use this for initialization
	void Start () {
        rend = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
        col.isTrigger = true;
        rend.color = waterColor;
		attachedConductor = GetComponent<Conductor> ();
		Debug.Log ("Attached Conductor: " + attachedConductor.gameObject.name);
	}
	
	// Update is called once per frame
	void Update () {
		if (iceTimer < 0) {
			col.isTrigger = true;
		} else {
			col.isTrigger = false;
		}
		if(iceTimer < 0 && !attachedConductor.isPowered){
            rend.color = waterColor;
        }
		if (attachedConductor.isPowered) {
			rend.color = electricColor;
			col.isTrigger = true;
		}

        iceTimer -= Time.deltaTime;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pointer")
        {
            rend.color = iceColor;
            col.isTrigger = false;
            iceTimer = iceTimerMax;
        }

        if (collision.gameObject.tag == "Lightning")
        {
            rend.color = electricColor;
            col.isTrigger = true;
        }
    }
}
