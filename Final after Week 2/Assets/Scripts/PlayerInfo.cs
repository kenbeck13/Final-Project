using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {

    public bool hasFire;
    public bool hasLightning;
    public bool hasIce;
    //is the player in water?
    public bool inWater;
    public GameObject lightningAura;
    public GameObject rightFireballPrefab;
    public GameObject leftFireballPrefab;
    public bool isFacingRight;
    PlayerMovement movementScript;
    public float shootTimerMax;
    float shootTimer;

	// Use this for initialization
	void Start () {
        lightningAura.SetActive(false);
        isFacingRight = true;
        movementScript = GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
        if(movementScript.velocity.x > 0.5f){
            isFacingRight = true;
        }else if (movementScript.velocity.x < -0.5f){
            isFacingRight = false;
        }

		//left click to shoot fire
        if(Input.GetMouseButtonDown(0) && hasFire && shootTimer < 0){
            if (isFacingRight)
            {
                Instantiate(rightFireballPrefab, transform.position, Quaternion.identity);
            }else{
                Instantiate(leftFireballPrefab, transform.position, Quaternion.identity);
            }
            shootTimer = shootTimerMax;
        }
        //Shift to use lightning
        if(Input.GetKey(KeyCode.LeftShift) && hasLightning){
            lightningAura.SetActive(true);
        }
        if(Input.GetKeyUp(KeyCode.LeftShift)){
            lightningAura.SetActive(false);
            inWater = false;
        }
        shootTimer -= Time.deltaTime;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Spellbook")
        {
            Spellbook spellbookScript = collision.gameObject.GetComponent<Spellbook>();
            if(spellbookScript.element == "Fire"){
                hasFire = true;
            }
            if (spellbookScript.element == "Lightning")
            {
                hasLightning = true;
            }
            if (spellbookScript.element == "Ice")
            {
                hasIce = true;
            }
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            inWater = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            inWater = true;
        }
    }
}
