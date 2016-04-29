﻿using UnityEngine;
using System.Collections;

public class InputState : MonoBehaviour {

	public bool actionButton;
	public float absVelX = 0f;
	public float absVelY = 0f;
	public bool standing;
	public float standingThreshold = 1f;

	private Rigidbody2D body2d;

	void Awake() { 
		body2d = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
		actionButton = Input.anyKeyDown;
	}

	// Physics calculations should go in FixedUpdate instead of Update
	void FixedUpdate() { 
		absVelX = System.Math.Abs(body2d.velocity.x);
		absVelY = System.Math.Abs(body2d.velocity.y);

		standing = absVelY <= standingThreshold;

	}
}