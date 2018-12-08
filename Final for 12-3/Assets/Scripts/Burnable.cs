using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burnable : MonoBehaviour {

	public bool isOnFire;
	public bool isBurning;
	public float burnTimer;
	public float whenBurning;
	public float whenVisualAppear;
	public Sprite[] sprites;
	SpriteRenderer rend;
	public Burnable[] nearbyBurnables;
	public bool frozen;

	// Use this for initialization
	void Start () {
		rend = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!isOnFire && !frozen) {
			rend.sprite = sprites [0];
		} else if (burnTimer < whenBurning && burnTimer > whenVisualAppear && !frozen) {
			isBurning = true;
		} else if (burnTimer < whenVisualAppear && !frozen) {
			rend.sprite = sprites [1];
		} else if (frozen) {
			rend.sprite = sprites [2];
		}

		if (isOnFire && !frozen) {
			burnTimer -= Time.deltaTime;
		}

		if (burnTimer < 0) {
			Destroy (gameObject);
		}

		for (int i = 0; i < nearbyBurnables.Length; i++) {
			if (nearbyBurnables [i].isBurning) {
				isOnFire = true;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collision){
		if (collision.gameObject.tag == "Fire") {
			isOnFire = true;
			Destroy (collision.gameObject);
			frozen = false;
		}
		if (collision.gameObject.tag == "Pointer" && isOnFire) {
			isOnFire = false;
			frozen = true;
			rend.sprite = sprites [0];
		}
	}

	void OnCollisionStay2D(Collision2D collision){
		if (collision.gameObject.tag == "Wooden Box") {
			Debug.Log ("Collision");
			Burnable burnableScript = collision.gameObject.GetComponent<Burnable> ();
			if (burnableScript.isBurning) {
				isOnFire = true;
				frozen = false;
			}
		}
	}
}
