using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour {

    //2D movement from class
    Rigidbody2D rb;
    public Vector2 velocity;
    public float speed, gravityUp, gravityDown, downVelocityMax;
    public GameObject player;
    public bool isFrozen;
    public float iceTimerMax;
    float iceTimer;
	public float playerDistance;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector2.Distance (player.transform.position, transform.position) < playerDistance) {
			if (player.transform.position.x > transform.position.x) {
				velocity.x = speed;
			} else {
				velocity.x = speed * -1;
			}

			if (velocity.y < downVelocityMax) {
				velocity.y = downVelocityMax;
			}

			//gravity logic
			if (!isFrozen) {
				rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX | ~RigidbodyConstraints2D.FreezePositionY;
				if (velocity.y > 0) {
					velocity.y -= gravityUp * Time.deltaTime;
				} else {
					velocity.y -= gravityDown * Time.deltaTime;
				}
				rb.MovePosition (rb.position + velocity * Time.deltaTime);
			} else {
				rb.constraints &= RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
			}
			iceTimer -= Time.deltaTime;

			if (iceTimer < 0) {
				isFrozen = false;
			}
		}
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "Water")
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                //vertically
                if (Mathf.Abs(contact.normal.y) > (Mathf.Abs(contact.normal.x)))
                {
                    velocity.y = 0;
                }
            }
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
