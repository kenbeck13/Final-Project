using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

	//2D movement from class
	Rigidbody2D rb;
	public Vector2 velocity;
	public float speed, gravityUp, gravityDown, jumpVelocity, downVelocityMax;
	//how many jumps the player has made
	public int jumps;
	//how many jumps the player can make
	public int maxJumps;
	public float maxAnimTimer;
	public float animTimer;

	PlayerInfo infoScript;

	Animator myAnimator;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator> ();
		infoScript = GetComponent<PlayerInfo> ();
	}

	// Update is called once per frame
	void Update () {
		AnimationLogic ();
		velocity.x = Input.GetAxis ("Horizontal") * speed;

		if(velocity.y < downVelocityMax){
			velocity.y = downVelocityMax;
		}

		//gravity logic
		if(velocity.y > 0){
			velocity.y -= gravityUp * Time.deltaTime;
		}
		else{
			velocity.y -= gravityDown * Time.deltaTime;
		}


		//jump logic
		if(Input.GetButtonDown("Jump") == true && jumps > 0){
            Jump(1.0f);
		}

		rb.MovePosition(rb.position + velocity * Time.deltaTime);
		animTimer -= Time.deltaTime;
	}

	private void Jump(float power){
        //jump logic
        velocity.y = jumpVelocity * power;
        jumps--;
    }

	void AnimationLogic(){
		SpriteRenderer spriteRend = GetComponent<SpriteRenderer>();
		if (Input.GetMouseButtonDown (0) && infoScript.hasFire) {
			animTimer = maxAnimTimer;
			myAnimator.SetTrigger ("spellTrigger");
		}
		else if (Input.GetMouseButtonDown (1) && infoScript.hasIce) {
			animTimer = maxAnimTimer;
			myAnimator.SetTrigger ("spellTrigger");
		}
		else if (Input.GetKeyDown (KeyCode.LeftShift) && infoScript.hasLightning) {
			animTimer = maxAnimTimer;
			myAnimator.SetTrigger ("spellTrigger");
		}
		else if (velocity.y > 0.1f && velocity.x > 0.1f && animTimer < 0) {
			myAnimator.SetInteger("playerAnimation", 2);
			spriteRend.flipX = false;
		} else if (velocity.y > 0.1f && velocity.x < -0.1f && animTimer < 0){
			myAnimator.SetInteger("playerAnimation", 2);
			spriteRend.flipX = true;
		}else if (velocity.y > 0.1f && animTimer < 0){
			myAnimator.SetInteger("playerAnimation", 2);
		}
		else if (velocity.x > 0.1f && animTimer < 0)
		{
			myAnimator.SetInteger("playerAnimation", 3);
			spriteRend.flipX = false;
		} else if (velocity.x < -0.1f && animTimer < 0){
			myAnimator.SetInteger("playerAnimation", 3);
			spriteRend.flipX = true;
		}
		else if (animTimer < 0)
		{
			myAnimator.SetInteger("playerAnimation", 0);
		}

	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "Water" || collision.gameObject.tag == "Slime" || collision.gameObject.tag == "Spawned Slime" || collision.gameObject.tag == "Wooden Box" || collision.gameObject.tag == "Battery" || collision.gameObject.tag == "Door") {
			foreach (ContactPoint2D contact in collision.contacts) {
				//vertically
				if (Mathf.Abs (contact.normal.y) > (Mathf.Abs (contact.normal.x))) {
					velocity.y = 0;
					if (contact.normal.y >= 0) {
						jumps = maxJumps;
						if(collision.gameObject.tag == "Slime"){
							Slime slimeScript = collision.gameObject.GetComponent<Slime>();
							if(!slimeScript.isFrozen){
								Jump(1.4f);
							}
						}
					}
				} 
			}
		}
	}

	private void OnCollisionStay2D(Collision2D collision){
        if (collision.gameObject.tag == "Slime" || collision.gameObject.tag == "Water" || collision.gameObject.tag == "Wooden Box") {
			foreach (ContactPoint2D contact in collision.contacts) {
				if (Mathf.Abs (contact.normal.y) < (Mathf.Abs (contact.normal.x))) {
					jumps = maxJumps;
				}
			}
		}
	}
}
