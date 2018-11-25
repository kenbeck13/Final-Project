using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour {

	SpriteRenderer rend;
	CircleCollider2D col;
	public float pointerTimerMax;
	float pointerTimer;

	// Use this for initialization
	void Start () {
		rend = GetComponent<SpriteRenderer> ();
		col = GetComponent<CircleCollider2D> ();
		rend.enabled = false;
		col.enabled = false;
		pointerTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			pointerTimer = pointerTimerMax;
			rend.enabled = true;
			col.enabled = true;
		}
		if (Input.GetMouseButtonUp (0)) {
			rend.enabled = false;
			col.enabled = false;
		}
		if (pointerTimer < 0) {
			rend.enabled = false;
			col.enabled = false;
		}
		Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		transform.position = mousePos;
		pointerTimer -= Time.deltaTime;
	}
}
