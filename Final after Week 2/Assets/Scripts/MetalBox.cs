using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalBox : MonoBehaviour {

	public Conductor[] nearbyConductors;
	bool isElectrified;
	public float elecTimerMax;
	public float elecTimer;
	Conductor incitingConductor;

	// Use this for initialization
	void Start () {
		elecTimer = elecTimerMax;
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < nearbyConductors.Length; i++) {
			
			if (nearbyConductors [i].isElectrified && Vector3.Distance(transform.position, nearbyConductors[i].gameObject.transform.position) < 2 && !isElectrified) {
				isElectrified = true;
				incitingConductor = nearbyConductors [i];
			}
			if (isElectrified && nearbyConductors[i] != incitingConductor) {
				nearbyConductors [i].isElectrified = true;
			}
		}
		if (isElectrified) {
			elecTimer -= Time.deltaTime;
		}

		if (elecTimer < 0) {
			isElectrified = false;
			elecTimer = elecTimerMax;
		}
	}
}
