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
		}
	}
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.GetComponent<MovingPlatform>() != null) 
		{
			onMovingPlatform = false;
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		foreach (ContactPoint2D contact in collision.contacts)
			Debug.DrawRay (contact.point, contact.normal, Color.white);

		if (collision.gameObject.GetComponent<Spring>() != null) {
			//must initiate from here to avoid some race condition
			collision.gameObject.GetComponent<Spring>().SpringCollide(gameObject);
			if (collision.transform.parent.GetComponent<Platform>() != null)
				collisionType = collision.transform.parent.GetComponent<Platform>().type;
			return;
		}
		
		if(collision.gameObject.GetComponent<Platform>() != null){
			collisionType = collision.gameObject.GetComponent<Platform>().type;
		}

		foreach (ContactPoint2D contact in collision.contacts)
		{
			if (Mathf.Abs (Vector2.Angle (Vector2.up, contact.normal)) < groundedThresholdAngle) {
				grounded = true;
				GetComponent<Animator>().SetBool("jumping", false);
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
			if (Mathf.Abs (Vector2.Angle (Vector2.up, contact.normal)) < groundedThresholdAngle) {
				grounded = true;
			}
			if (Mathf.Abs(Vector2.Angle(Vector2.right, contact.normal)) < wallHugThresholdAngle)
				wallHug = Mathf.Sign(contact.normal.x);
			if (Mathf.Abs(Vector2.Angle(-Vector2.right, contact.normal)) < wallHugThresholdAngle)
				wallHug = Mathf.Sign(contact.normal.x);
		}

		foreach (ContactPoint2D contact in collision.contacts)
		{
			if (GetComponent<Rigidbody2D>().velocity.magnitude > frictionThresholdVelocity)
			{
				Vector2 frictionDir = -(GetComponent<Rigidbody2D>().velocity.normalized);
				float frictionMag = contact.normal.magnitude * frictionCoefficient;
				Vector2 frictionVec = frictionDir * frictionMag;
				//maybe some safeguards against wall climbing using the normal here
				GetComponent<Rigidbody2D>().AddForce(frictionVec);
			}
		}
		if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) < frictionThresholdVelocity
		    && Input.GetAxis("Horizontal") == 0)
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(0f, GetComponent<Rigidbody2D>().velocity.y);
			//aimed to bring motion to a complete stop
		}
		hasContact = true;

	}

	void OnCollisionExit2D(Collision2D collision) {
		hasContact = false;
		if (!onMovingPlatform && transform.parent != null) 
		{
			transform.parent = null;	//get off the platform
			GetComponent<Animator>().SetBool("jumping", true);
		}
	}

	private void ListenForJump() {
		if (Input.GetButton("Jump") && grounded && jumpTimer >= jumpDelay 
		    && Time.frameCount - springFrame > Spring.springJumpFrameThreshold)
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0f);	//gets rid of the "extra high jump"
			this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce);
			GetComponent<Animator>().SetBool("jumping", true);
			jumpTimer = 0.0f;
			hasContact = false;
			jumpFrame = Time.frameCount;

		}
	}

	void FixedUpdate () {
		if(this.GetComponent<Rigidbody2D>().velocity.y >= 0){
			this.gameObject.layer = 8;
        }
        if(this.GetComponent<Rigidbody2D>().velocity.y < 0 || onMovingPlatform){
        	this.gameObject.layer = 0;
        }
		float h = Input.GetAxis ("Horizontal");
		
		ListenForJump ();
		if(wallHug == 0f || Mathf.Sign(h) == wallHug)
		{
			//recordButtonDelay();
			//translational movement
			if(Mathf.Sign(h) != Mathf.Sign (this.GetComponent<Rigidbody2D>().velocity.x))
			{
				this.GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce * translationStoppingMultiplier);
			}
			else if (Mathf.Abs(this.GetComponent<Rigidbody2D>().velocity.x) < maxPlayerGeneratedSpeed)
			{
				this.GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);
			}
		}

		if (onMovingPlatform && transform.parent == null) {
			transform.parent = platformParent;
		}

		grounded = false;
		wallHug = 0f;
		jumpTimer += Time.fixedDeltaTime;
	}
}
