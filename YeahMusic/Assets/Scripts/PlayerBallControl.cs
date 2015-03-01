using UnityEngine;
using System.Collections;

public class PlayerBallControl : MonoBehaviour {

	// Physics for lateral movement
	public float moveForce = 75f;			// Amount of force for translational movement
	public float translationStoppingMultiplier = 4f;	// Multiplier for translational movement opposite velocity
	public float maxPlayerGeneratedSpeed = 10f;
	public float frictionCoefficient = 0.2f;		//percent magnitude of normal for friction force
	public float frictionThresholdVelocity = 0.3f;	//threshold above which to apply friction

	// Jumping
	public float jumpForce = 2800f;
	public float jumpDelay = 0.4f;	//time (in s) delay between jumps
	private float jumpTimer = 0.4f;
	[HideInInspector]
	public int jumpFrame = 0;
	[HideInInspector]
	public int springFrame = 0;

	// Grounded vars
	[HideInInspector]
	public bool grounded = false;
	[HideInInspector]			// Whether or not the player is grounded.
	public bool hasContact = false;		// Whether the player is touching something
	public float groundedThresholdAngle = 45f;
	public float wallHugThresholdAngle = 30f;
	public bool groundedScore = false;
	private float wallHug = 0f;

	//moving platform detection vars
	private bool onMovingPlatform = false;
	private Transform platformParent;
	
	//uva game platform type
	[HideInInspector]
	public int collisionType;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.GetComponent<MovingPlatform>() != null) 
		{
			onMovingPlatform = true;
			platformParent = col.transform.parent;
			//Debug.Log ("Player entered");
		}
	}
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.GetComponent<MovingPlatform>() != null) 
		{
			onMovingPlatform = false;
			//Debug.Log ("Player exited");
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		groundedScore = true;
		foreach (ContactPoint2D contact in collision.contacts)
			Debug.DrawRay (contact.point, contact.normal, Color.white);

		if (collision.gameObject.GetComponent<Spring>() != null) {
			//must initiate from here to avoid some race condition
			collision.gameObject.GetComponent<Spring>().SpringCollide(gameObject);
			return;
		}
		
		if(collision.gameObject.GetComponent<Platform>() != null){
			collisionType = collision.gameObject.GetComponent<Platform>().type;
		}

		foreach (ContactPoint2D contact in collision.contacts)
		{
			if (Mathf.Abs(Vector2.Angle(Vector2.up, contact.normal)) < groundedThresholdAngle){
				grounded = true;		
				GUICounter.scores += 10;
			}
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
		//if (collision.gameObject.GetComponent<MovingPlatform>() != null
		//    && dState == DeformationState.Normal)
		//	transform.parent.parent = null;	//get off the platform
		if (!onMovingPlatform
		    && transform.parent != null)
			transform.parent = null;	//get off the platform
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
		if(this.rigidbody2D.velocity.y >= 0){
			this.gameObject.layer = 8;
        }
        if(this.rigidbody2D.velocity.y < 0){
        	this.gameObject.layer = 0;
        }
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

		if (onMovingPlatform && transform.parent == null)
			transform.parent = platformParent;

		grounded = false;
		wallHug = 0f;
		jumpTimer += Time.fixedDeltaTime;
	}
}
