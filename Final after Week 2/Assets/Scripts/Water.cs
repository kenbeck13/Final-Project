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
	public bool startsAsIce;
	public bool isFrozen;

	// Use this for initialization
	void Start () {
        rend = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
        col.isTrigger = true;
        rend.color = waterColor;
		attachedConductor = GetComponent<Conductor> ();
		if (startsAsIce) {
			col.isTrigger = false;
			rend.color = iceColor;
			isFrozen = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!isFrozen) {
			col.isTrigger = true;
		} else {
			col.isTrigger = false;
		}
		if(iceTimer < 0 && !startsAsIce){
            rend.color = waterColor;
			isFrozen = false;
        }

		if (attachedConductor.isPowered && !isFrozen) {
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
			isFrozen = true;
        }

		if (collision.gameObject.tag == "Lightning" && col.isTrigger && !isFrozen)
        {
            rend.color = electricColor;
            col.isTrigger = true;
        }
		if (collision.gameObject.tag == "Fire")
		{
			startsAsIce = false;
			rend.color = waterColor;
			col.isTrigger = true;
			Destroy (collision.gameObject);
			isFrozen = false;
		}
    }
}
