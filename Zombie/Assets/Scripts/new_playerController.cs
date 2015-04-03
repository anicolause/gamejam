using UnityEngine;
using System.Collections;

public class new_playerController : MonoBehaviour {
	Animator anim;
	Animator aZombie;

	public float maxSpeed = 10f;
	bool facingRight = true;

	//floor
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = 700f;

	void Start() {
		anim = GetComponent<Animator> ();
	}

	void FixedUpdate () {

		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);
		anim.SetFloat ("speed", rigidbody2D.velocity.y);

		float move = Input.GetAxis ("Horizontal");

		anim.SetFloat ("speed", Mathf.Abs(move));

		rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);

		if (move > 0 && !facingRight)
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();
	}

	void Update () {
		if (grounded && Input.GetKeyDown(KeyCode.Space)) {
			anim.SetBool("Ground", false);
			rigidbody2D.AddForce (new Vector2(0, jumpForce));
		}

		if (!anim.GetBool("Hit") && Input.GetKeyDown (KeyCode.KeypadEnter)) {
			anim.SetBool("Hit", true);
		}
	}

	void Flip () {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


	public void hitDone() {
		anim.SetBool("Hit", false);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (anim.GetBool("Hit")) {
			if (other.collider2D.tag.Equals("zombie")) {
				aZombie = other.GetComponentInParent<Animator>();
				aZombie.SetBool("Hit", true);
				aZombie.SetFloat("speed", 0);
			}
		}
	}
}