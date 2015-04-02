using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	public float SPEED = 100f;

	//Jump
	public float JUMP_HEIGHT = 200;
	public float JUMP_VEL = 10f;
	private float yJump;
	private bool bJump;
	private float initPosJump;

	private bool bFalling;

	private bool bAlive;

	void OnCollisionEnter(Collision col){
		if (!bJump && col.transform.tag.Equals ("floor")) {
			bJump = false;
		}

		if (col.transform.tag.Equals ("dead")) {
			bAlive = false;
		}
	}

	void OnCollisionStay(Collision col){

	}

	void Update() {
		playerMovement ();
		checkJump();

	}
	
	void playerMovement () {
		if (Input.GetKey("space")) {
			if (!bJump)
				initJump();
		}

		if (Input.GetKey ("left")) {
			left();

		}

		if (Input.GetKey ("right")) {
			right ();
		}
	}

	void initJump() {
		bFalling = false;
		bJump = true;
		yJump = 0;
		initPosJump = this.transform.position.y;
	}

	void checkJump() {
		if (bJump) {
			if (!bFalling && yJump < JUMP_HEIGHT) {
				yJump+=JUMP_VEL;
				this.transform.Translate(Vector2.up * yJump * Time.deltaTime);
			} else {
				bFalling = true;
				yJump-=JUMP_VEL;
				this.transform.Translate(Vector2.up * yJump * Time.deltaTime);

				if (this.transform.position.y < initPosJump) {
					this.transform.position = new Vector3 (this.transform.position.x, initPosJump, this.transform.position.z);
					bJump = false;
				}
			}
		}
	}

	void left(){
		this.transform.Translate(Vector2.right * -SPEED * Time.deltaTime);
	}

	void right(){
		this.transform.Translate(Vector2.right * SPEED * Time.deltaTime);
	}
}