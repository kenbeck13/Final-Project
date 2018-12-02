using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
	public Vector3 fireballSpawnDistance;
	SpriteRenderer rend;
	Scene currentScene;
	string currentSceneName;

	// Use this for initialization
	void Start () {
		rend = GetComponent<SpriteRenderer> ();
        lightningAura.SetActive(false);
        isFacingRight = true;
        movementScript = GetComponent<PlayerMovement>();

		currentScene = SceneManager.GetActiveScene ();
		currentSceneName = currentScene.name;
	}
	
	// Update is called once per frame
	void Update () {
        if(movementScript.velocity.x > 0.5f){
            isFacingRight = true;
        }else if (movementScript.velocity.x < -0.5f){
            isFacingRight = false;
        }

		if (isFacingRight)
		{
			rend.flipX = false;
		}else{
			rend.flipX = true;
		}

		//left click to shoot fire
        if(Input.GetMouseButtonDown(0) && hasFire && shootTimer < 0){
            if (isFacingRight)
            {
				Instantiate(rightFireballPrefab, transform.position + fireballSpawnDistance, Quaternion.identity);
            }else{
				Instantiate(leftFireballPrefab, transform.position - fireballSpawnDistance, Quaternion.identity);
            }
            shootTimer = shootTimerMax;
        }
        //Shift to use lightning
        if(Input.GetKey(KeyCode.LeftShift) && hasLightning){
            lightningAura.SetActive(true);
        }
		if(Input.GetKeyUp(KeyCode.LeftShift) && hasLightning){
            lightningAura.SetActive(false);
            inWater = false;
        }

		if (Input.GetKeyDown (KeyCode.R)) {
			SceneManager.LoadScene (currentSceneName);
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

		if (collision.gameObject.tag == "Star")
		{
			Star starScript = collision.gameObject.GetComponent<Star> ();
			SceneManager.LoadScene (starScript.loadScene);
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
