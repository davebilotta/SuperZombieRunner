using UnityEngine;
using System.Collections;

public class PlayerAnimationManager : MonoBehaviour {

	private Animator animator;
	private InputState inputState;

	void Awake () {
		animator = GetComponent<Animator>();
		inputState = GetComponent<InputState>();


	}
	
	// Update is called once per frame
	void Update () {
		var running = true; // default state of player 

		// player not running if being dragged off screen and not in the air at the time 
		// or on top of obstacle
		if (inputState.absVelX > 0 && inputState.absVelY < inputState.standingThreshold	) {
			running = false;
		}

		animator.SetBool("Running",running);
	}
}
