using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {
	public Animator aPlayer;

	public float SPEED = 100f;

	//Player States
	private bool bJumpKey;
	private bool bLeftKey;
	private bool bRightKey;
	private bool bHitKey;

	//Jump
	public float JUMP_HEIGHT = 200;
	public float JUMP_VEL = 10f;
	private float yJump;
	private bool bJump;
	private float initPosJump;
	private bool bFalling;

	//Hit
	private bool bHit;

	//lives
	private bool bAlive;

	void Start () {
		aPlayer.Play("player_idle");
	}

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
		checkHit ();
	}
	
	void playerMovement () {
		if (Input.GetKey (KeyCode.Space)) {
			bJumpKey = true;
		} else {
			bJumpKey = false;
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			bLeftKey = true;
		} else {
			bLeftKey = false;
		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			bRightKey = true;
		} else {
			bRightKey = false;
		}

		if (Input.GetKey (KeyCode.KeypadEnter)) {
        	bHitKey = true;
		} else {
			bHitKey = false;
		}

		if (bJumpKey) {
			if (!bJump)
				initJump();
		}

		if (bLeftKey) {
			left();
		}

		if (bRightKey) {
			right ();
		}

		if (bHitKey) {
			initHit ();
		}

		if (!bHit && !bJump && !bLeftKey && !bRightKey) {
			aPlayer.Play ("player_idle");
		}

	}

	void initJump() {
		aPlayer.Play ("player_jump");
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

	void initHit() {
		if (!bHit) {
			bHit = true;
			aPlayer.Play ("player_hit");
		}
	}

	void checkHit(){

	}

	public void hitDone(){
		bHit = false;
	}

	void left(){
		Debug.Log("walk left");
		aPlayer.Play ("player_walk");

		if (this.transform.localScale.x > 0)
			this.transform.localScale = new Vector3 (this.transform.localScale.x * -1, this.transform.localScale.y, this.transform.localScale.z);

		this.transform.Translate(Vector2.right * -SPEED * Time.deltaTime);
	}

	void right(){
		Debug.Log("walk right");
		aPlayer.Play ("player_walk");

		if (this.transform.localScale.x < 0)
			this.transform.localScale = new Vector3 (this.transform.localScale.x * -1, this.transform.localScale.y, this.transform.localScale.z);
		this.transform.Translate(Vector2.right * SPEED * Time.deltaTime);
	}
}