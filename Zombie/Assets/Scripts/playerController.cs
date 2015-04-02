using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	private float Speed = 0.5f;

	void OnCollisionEnter(Collision col){
//		Debug.Log ("1");
//		this.transform.position.Set (
//			this.transform.position.x, 
//			this.transform.position.y - this.transform.collider.bounds.size.y/2, 
//			this.transform.position.z);
	}

	void OnCollisionStay(Collision col){
//		Debug.Log ("2");
//		this.transform.position.Set (
//			this.transform.position.x, 
//			this.transform.position.y - this.transform.collider.bounds.size.y/2, 
//			this.transform.position.z);
	}

	void Update() {
		playerMovement ();
		
	}
	
	void playerMovement () {
		if (Input.GetKey("space")) {
			Debug.Log ("space key was pressed");
		}

		if (Input.GetKey ("left")) {
			Debug.Log ("left key was pressed");

//			this.transform.position = new Vector3 (
//				this.transform.position.x - (Speed * Time.deltaTime), 
//				this.transform.position.y, 
//				this.transform.position.z);
			transform.Translate(Vector3.right * -Speed * Time.deltaTime);

		}

		if (Input.GetKey ("right")) {
			Debug.Log ("right key was pressed");

//			this.transform.position = new Vector3 (
//				this.transform.position.x + (Speed * Time.deltaTime), 
//				this.transform.position.y, 
//				this.transform.position.z);

			transform.Translate(Vector3.right * Speed * Time.deltaTime);
		}
	}
}