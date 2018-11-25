using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	//the switches that must be pressed to open this door
	public Switch[] switches;
	//is the door open?
	public bool isOpen;
	//how many switches are on during check
	int onSwitches;
	//Rigidbody2D
	Rigidbody2D rb;
	//speed the door moves when open
	public float speed;
	//how long it takes for the door to disappear
	public float killTimer;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (isOpen) {
			rb.velocity = transform.up * speed * Time.deltaTime;
			killTimer -= Time.deltaTime;
		}

		if (killTimer < 0) {
			Destroy (gameObject);
		}
	}

	/// <summary>
	/// Checks if the switches assigned to the door are all pressed.
	/// </summary>
	public void CheckForSwitches(){
		for (int i = 0; i < switches.Length; i++) {
			if (switches [i].isOn) {
				onSwitches++;
			} else {
				onSwitches = 0;
				return;
			}
			if (onSwitches == switches.Length) {
				isOpen = true;
				return;
			}
		}

	}
}
