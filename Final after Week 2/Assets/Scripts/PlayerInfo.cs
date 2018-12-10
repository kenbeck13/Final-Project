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
	Scene currentScene;
	string currentSceneName;
	public bool noMagic;
	AudioSource source;
	public AudioClip[] clips;

	// Use this for initialization
	void Start () {
        lightningAura.SetActive(false);
        isFacingRight = true;
        movementScript = GetComponent<PlayerMovement>();
		source = GetComponent<AudioSource> ();

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

		//left click to shoot fire
		if(Input.GetMouseButtonDown(0) && hasFire && shootTimer < 0 && !noMagic){
			source.clip = clips [0];
			source.Play ();
			source.loop = false;
            if (isFacingRight)
            {
				Instantiate(rightFireballPrefab, transform.position + fireballSpawnDistance, Quaternion.identity);
            }else{
				Instantiate(leftFireballPrefab, transform.position - fireballSpawnDistance, Quaternion.identity);
            }
            shootTimer = shootTimerMax;
        }
        //Shift to use lightning
		if(Input.GetKey(KeyCode.LeftShift) && hasLightning && !noMagic){
            lightningAura.SetActive(true);
        }
		if (Input.GetKeyDown (KeyCode.LeftShift) && hasLightning && !noMagic) {
			source.clip = clips [1];
			source.Play ();
			source.loop = true;
		}
		if(Input.GetKeyUp(KeyCode.LeftShift) && hasLightning){
            lightningAura.SetActive(false);
            inWater = false;
			source.Stop ();
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
			SceneManager.LoadScene (spellbookScript.loadScene);
        }

        if (collision.gameObject.tag == "Exit")
        {
            Star starScript = collision.gameObject.GetComponent<Star>();
            SceneManager.LoadScene(starScript.loadScene);
        }

		if (collision.gameObject.tag == "EnergyShield")
		{
			noMagic = true;
		}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            inWater = false;
        }
		if (collision.gameObject.tag == "EnergyShield")
		{
			noMagic = false;
		}
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            inWater = true;
        }
    }

	private void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == "Slime") {
			Slime slimeScript = collision.gameObject.GetComponent<Slime> ();
			if (!slimeScript.isFrozen) {
				source.clip = clips [2];
				source.Play ();
				source.loop = false;
			}
		}
	}
}
