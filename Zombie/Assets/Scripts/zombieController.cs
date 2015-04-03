using UnityEngine;
using System.Collections;

public class zombieController : MonoBehaviour {
	Animator anim;
	public float maxSpeed = 10f;
	bool facingRight = true;
	float dir = 1f;
	
	//floor
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = 700f;

	void Start() {
		anim = GetComponent<Animator> ();
		anim.SetFloat ("speed",dir);
	}
	
	void FixedUpdate () {
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
	}

	void Update () {
		checkTurnAround ();

		//move zombie
		checkZombieState ();
	}

	void checkZombieState () {
		if (anim.GetBool("Hit") == false)
			rigidbody2D.velocity = new Vector2 (dir * maxSpeed, rigidbody2D.velocity.y);
	}

	void checkTurnAround() {
		Collider2D c = GetComponent<BoxCollider2D> ();
		int layerMask = (1 << LayerMask.NameToLayer("floor"));

		//Ray
		Vector2 v = new Vector2 (this.transform.position.x + ((c.bounds.size.x) * dir), this.transform.position.y - c.bounds.size.y);
		Ray2D ray = new Ray2D (v, new Vector2(0,-1));
		Debug.DrawRay(ray.origin,(ray.direction).normalized);
		RaycastHit2D hit = Physics2D.Raycast (v, ray.direction, 3, layerMask);

		if (hit.collider == null) {
			dir = -dir;

			if (dir > 0 && !facingRight)
				Flip ();
			else if (dir < 0 && facingRight)
				Flip ();
		}
	}

	
	void Flip () {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
