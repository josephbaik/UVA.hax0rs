﻿using UnityEngine;
using System.Collections;

public class PlayerBallControl : MonoBehaviour {

	// Physics for lateral movement
	public float moveForce = 75f;			// Amount of force for translational movement
	public float translationStoppingMultiplier = 4f;	// Multiplier for translational movement opposite velocity
	public float maxPlayerGeneratedSpeed = 10f;
	public float frictionCoefficient = 0.2f;		//percent magnitude of normal for friction force
	public float frictionThresholdVelocity = 0.3f;	//threshold above which to apply friction
	// Jumping, Boosting
	public float jumpForce = 2800f;
	public float jumpDelay = 0.4f;	//time (in s) delay between jumps
	private float jumpTimer = 0.4f;
	[HideInInspector]
	public int jumpFrame = 0;
	[HideInInspector]
	public int springFrame = 0;
	// Grounded vars
	private bool grounded = false;			// Whether or not the player is grounded.
	private bool hasContact = false;		// Whether the player is touching something
	public float groundedThresholdAngle = 45f;
	public float wallHugThresholdAngle = 30f;
	private float wallHug = 0f;


	void OnCollisionEnter2D(Collision2D collision) {
		foreach (ContactPoint2D contact in collision.contacts)
			Debug.DrawRay (contact.point, contact.normal, Color.white);
		foreach (ContactPoint2D contact in collision.contacts)
		{
			if (Mathf.Abs(Vector2.Angle(Vector2.up, contact.normal)) < groundedThresholdAngle)
				grounded = true;
			if (Mathf.Abs(Vector2.Angle(Vector2.right, contact.normal)) < wallHugThresholdAngle)
				wallHug = Mathf.Sign(contact.normal.x);
			if (Mathf.Abs(Vector2.Angle(-Vector2.right, contact.normal)) < wallHugThresholdAngle)
				wallHug = Mathf.Sign(contact.normal.x);
		}
		hasContact = true;
	}

	void OnCollisionStay2D(Collision2D collision) {
		foreach (ContactPoint2D contact in collision.contacts)
			Debug.DrawRay (contact.point, contact.normal, Color.yellow);
		foreach (ContactPoint2D contact in collision.contacts)
		{
			if (Mathf.Abs(Vector2.Angle(Vector2.up, contact.normal)) < groundedThresholdAngle)
				grounded = true;
			if (Mathf.Abs(Vector2.Angle(Vector2.right, contact.normal)) < wallHugThresholdAngle)
				wallHug = Mathf.Sign(contact.normal.x);
			if (Mathf.Abs(Vector2.Angle(-Vector2.right, contact.normal)) < wallHugThresholdAngle)
				wallHug = Mathf.Sign(contact.normal.x);
		}

		foreach (ContactPoint2D contact in collision.contacts)
		{
			if (rigidbody2D.velocity.magnitude > frictionThresholdVelocity)
			{
				Vector2 frictionDir = -(rigidbody2D.velocity.normalized);
				float frictionMag = contact.normal.magnitude * frictionCoefficient;
				Vector2 frictionVec = frictionDir * frictionMag;
				//maybe some safeguards against wall climbing using the normal here
				rigidbody2D.AddForce(frictionVec);
			}
		}
		if (Mathf.Abs(rigidbody2D.velocity.x) < frictionThresholdVelocity
		    && Input.GetAxis("Horizontal") == 0)
		{
			rigidbody2D.velocity = new Vector2(0f, rigidbody2D.velocity.y);
			//aimed to bring motion to a complete stop
		}
		hasContact = true;

	}

	void OnCollisionExit2D(Collision2D collision) {
		hasContact = false;
	}

	private void ListenForJump() {
		if (Input.GetButton("Jump") && grounded && jumpTimer >= jumpDelay 
		    && Time.frameCount - springFrame > Spring.springJumpFrameThreshold)
		{
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0f);	//gets rid of the "extra high jump"
			this.rigidbody2D.AddForce(Vector2.up * jumpForce);
			jumpTimer = 0.0f;
			hasContact = false;
			jumpFrame = Time.frameCount;
		}
	}

	void FixedUpdate () {

		float h = Input.GetAxis ("Horizontal");
		
		ListenForJump ();
		if(wallHug == 0f || Mathf.Sign(h) == wallHug)
		{
			//recordButtonDelay();
			//translational movement
			if(Mathf.Sign(h) != Mathf.Sign (this.rigidbody2D.velocity.x))
			{
				this.rigidbody2D.AddForce(Vector2.right * h * moveForce * translationStoppingMultiplier);
			}
			else if (Mathf.Abs(this.rigidbody2D.velocity.x) < maxPlayerGeneratedSpeed)
			{
				this.rigidbody2D.AddForce(Vector2.right * h * moveForce);
			}
		}

		grounded = false;
		wallHug = 0f;
		jumpTimer += Time.fixedDeltaTime;
	}
}