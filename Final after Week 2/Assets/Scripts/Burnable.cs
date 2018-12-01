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

	// Use this for initialization
	void Start () {
		rend = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!isOnFire) {
			rend.sprite = sprites [0];
		} else if (burnTimer < whenBurning && burnTimer > whenVisualAppear) {
			isBurning = true;
		} else if (burnTimer < whenVisualAppear) {
			rend.sprite = sprites [1];
		}

		if (isOnFire) {
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
		}
		if (collision.gameObject.tag == "Wooden Box") {
			Burnable burnableScript = collision.gameObject.GetComponent<Burnable> ();
			if (burnableScript.isBurning) {
				isOnFire = true;
			}
		}
	}
}
