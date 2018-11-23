using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

	public bool isOn;
	public bool isPermanent;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			if (!isOn) {
				isOn = true;
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
	}
}
