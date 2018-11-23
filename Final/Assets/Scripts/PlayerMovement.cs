using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

	Rigidbody2D rb;
	public Vector2 velocity;
	public float speed, gravityUp, gravityDown, jumpVelocity, downVelocityMax;
	public int jumps;
	public int maxJumps;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
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
			velocity.y = jumpVelocity;
			jumps--;
		}

		rb.MovePosition(rb.position + velocity * Time.deltaTime);


	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Platform") {
			foreach (ContactPoint2D contact in collision.contacts) {
				//vertically
				if (Mathf.Abs (contact.normal.y) > (Mathf.Abs (contact.normal.x))) {
					velocity.y = 0;
					if (contact.normal.y >= 0) {
						jumps = maxJumps;
					}
				} 
			}
		}
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Platform")
		{
			foreach (ContactPoint2D contact in collision.contacts)
			{
				//vertically
				if (Mathf.Abs(contact.normal.y) > (Mathf.Abs(contact.normal.x)))
				{
					if (contact.normal.y >= 0)
					{

					}
				}
			}
		}
	}
}
