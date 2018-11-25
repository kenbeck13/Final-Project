using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

	//is the switch on?
	public bool isOn;
	//will the switch stay on when the player moves off of it?
	public bool isPermanent;
	//what door this switch opens
	public Door attachedDoor;

	public Sprite[] sprites;

	SpriteRenderer rend;

	// Use this for initialization
	void Start () {
		rend = GetComponent<SpriteRenderer> ();
		rend.sprite = sprites [0];
	}
	
	// Update is called once per frame
	void Update () {
		if (isOn) {
			rend.sprite = sprites [1];
		} else {
			rend.sprite = sprites [0];
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			if (!isOn) {
				isOn = true;
				attachedDoor.CheckForSwitches();
			}
		} 
		else if (collision.gameObject.tag == "Pointer")
		{
			if (!isOn) {
				isOn = true;
				attachedDoor.CheckForSwitches ();
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			if (isOn && !isPermanent) {
				isOn = false;
			}
		}

		else if (collision.gameObject.tag == "Pointer")
		{
			if (isOn && !isPermanent) {
				isOn = false;
			}
		}
	}
}
