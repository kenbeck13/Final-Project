using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour {

	SpriteRenderer rend;
	CircleCollider2D col;
	public float pointerTimerMax;
	float pointerTimer;
    public PlayerInfo player;
	public float degreesPerSecond;

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
		transform.Rotate (Vector3.forward, degreesPerSecond * Time.deltaTime);
		if (Input.GetMouseButtonDown (1) && player.hasIce && !player.inWater && !player.noMagic) {
			pointerTimer = pointerTimerMax;
			rend.enabled = true;
			col.enabled = true;
		}
		if (Input.GetMouseButtonUp (1)) {
			rend.enabled = false;
			col.enabled = false;
		}
		if (pointerTimer < 0) {
			rend.enabled = false;
			col.enabled = false;
		}
		if (player.noMagic) {
			rend.enabled = false;
			col.enabled = false;
		}
		Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		transform.position = mousePos;
		pointerTimer -= Time.deltaTime;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "EnergyShield") {
			rend.enabled = false;
			col.enabled = false;
		}
		if (collision.gameObject.tag == "FireShield") {
			rend.enabled = false;
			col.enabled = false;
		}
	}

}
