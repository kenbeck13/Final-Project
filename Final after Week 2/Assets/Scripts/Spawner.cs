using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject objectToInstantiate;
    public float spawnTimerMax;
    public float spawnTimer;

	// Use this for initialization
	void Start () {
        spawnTimer = spawnTimerMax;
	}
	
	// Update is called once per frame
	void Update () {
        if(spawnTimer < 0){
            spawnTimer = spawnTimerMax;
            Instantiate(objectToInstantiate, transform.position, Quaternion.identity);
        }
        spawnTimer -= Time.deltaTime;
	}
}
