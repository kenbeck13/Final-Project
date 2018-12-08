using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour {

	public bool isElectrified;
	float elecTimer;
	public float elecTimerMax;
	public Sprite[] sprites;
	SpriteRenderer rend;

	// Use this for initialization
	void Start () {
		rend = GetComponent<SpriteRenderer> ();
		elecTimer = elecTimerMax;
	}
	
	// Update is called once per frame
	void Update () {
		if (isElectrified) {
			elecTimer -= Time.deltaTime;
			rend.sprite = sprites [1];
		} else {
			rend.sprite = sprites [0];
		}

		if (elecTimer < 0) {
			isElectrified = false;
			elecTimer = elecTimerMax;
		}
	}

	void OnTriggerEnter2D(Collider2D collision){
		if (collision.gameObject.tag == "Lightning") {
			isElectrified = true;
		}
	}
}
