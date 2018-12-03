using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour {
	
	public bool isElectrified;
	public bool isPowered;
	public float elecTimerMax;
	public float elecTimer;
	public float whenPowered;
	public float whenVisualAppear;
	public Sprite[] sprites;
	SpriteRenderer rend;
	public Conductor[] nearbyConductors;
	public bool isWater;
	public bool turnsOnSwitches;
	Water waterScript;

	// Use this for initialization
	void Start () {
		rend = GetComponent<SpriteRenderer> ();
		elecTimer = elecTimerMax;
		waterScript = GetComponent<Water> ();
	}

	// Update is called once per frame
	void Update () {
		if (!isElectrified) {
			if (!isWater) {
				rend.sprite = sprites [0];
			}
		} else if (elecTimer < whenPowered && elecTimer > whenVisualAppear) {
			isPowered = true;
		} else if (elecTimer < whenVisualAppear) {
			if (!isWater) {
				rend.sprite = sprites [1];
			}
			isPowered = false;
			turnsOnSwitches = true;
		}

		if (isElectrified) {
			elecTimer -= Time.deltaTime;
		}

		if (elecTimer < 0) {
			turnsOnSwitches = false;
			isElectrified = false;
			if (!isWater) {
				rend.sprite = sprites [0];
			}
			elecTimer = elecTimerMax;
		}
		if (!isWater) {
			for (int i = 0; i < nearbyConductors.Length; i++) {
				if (nearbyConductors [i].isPowered) {
					isElectrified = true;
				}
			}
		} else if(isWater && !waterScript.isFrozen) {
			for (int i = 0; i < nearbyConductors.Length; i++) {
				if (nearbyConductors [i].isPowered) {
					isElectrified = true;
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collision){
		if (collision.gameObject.tag == "Lightning") {
			isElectrified = true;
			//Debug.Log ("Electrified");
		}
		if (collision.gameObject.tag == "Metal Box") {
			Conductor conductorScript = collision.gameObject.GetComponent<Conductor> ();
			if (conductorScript.isElectrified) {
				isElectrified = true;
			}
		}
		if (collision.gameObject.tag == "Water") {
			Conductor waterScript = collision.gameObject.GetComponent<Conductor> ();
			if (waterScript.isElectrified) {
				isElectrified = true;
			}
		}
		if (collision.gameObject.tag == "Fire" && !isWater) {
			Destroy (collision.gameObject);
		}
	}
}
