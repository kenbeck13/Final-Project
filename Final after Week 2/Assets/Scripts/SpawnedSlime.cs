using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedSlime : MonoBehaviour {

	//2D movement from class
    public bool isFrozen;
    public float iceTimerMax;
    float iceTimer;
    public float moveSpeed;
    SpriteRenderer rend;
    public Sprite[] sprites;

    // Use this for initialization
    void Start () {
        rend = GetComponent<SpriteRenderer>();
    }
    
    // Update is called once per frame
    void Update () {

        if (iceTimer < 0) {
            isFrozen = false;
        }

        if(!isFrozen){
            transform.position = transform.position + (new Vector3(0, moveSpeed, 0) * Time.deltaTime);
            rend.sprite = sprites[0];
        }else{
            rend.sprite = sprites[1];
        }


        iceTimer -= Time.deltaTime;

		if(transform.position.y < -20){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pointer")
        {
            iceTimer = iceTimerMax;
            isFrozen = true;
        }
    }
}
