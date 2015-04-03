using UnityEngine;
using System.Collections;

public class animatorState : MonoBehaviour {
	private Animator anim;

	public void animStateTrue (string state) {
		anim = GetComponent<Animator> ();
		anim.SetBool (state, true);
	}


	public void animStateFalse (string state) {
		anim = GetComponent<Animator> ();
		anim.SetBool (state, false);
	}
}
