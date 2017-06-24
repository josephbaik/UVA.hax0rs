using UnityEngine;
using System.Collections;
/// <summary>
/// Script for a moving platform. As currently implemented, should be attached
/// to an object with a TRIGGER collider attached as a child to the larger platform object
/// with a NON-TRIGGER collider. The trigger goes on the top part of the platform.
///
/// Can also be used for moving things which are not meant to be platformed on..
/// be sure to set moveParent to false then!!
/// </summary>
public class MovingPlatform : MonoBehaviour {
	public Vector2 initialPoint;
	public Vector2 endPoint; //if flag below is set, this specifies an offset
	public bool useCurrentStartPosition = false;
	public float moveTime = 5.0f; //in seconds, from start to end
	public float pauseTime = 2.0f; //in seconds, time at endpoints
	public bool moveParent = true; // if this collider is a "dummy child" to the larger platform
	private float moveTimer = 0.0f;
	private float pauseTimer = 0.0f;
	private bool paused = false;
	private Vector2 mFrom;
	private Vector2 mTo;
	private Vector2 moveVelocity;
	void Start() {
		if (useCurrentStartPosition)
		{
			//initialPoint = rigidbody2D.position;
			//Vector2 moveVector = endPoint-initialPoint;
			if (moveParent)
				mFrom = transform.parent.position;
			else
				mFrom = transform.position;
			mTo = mFrom+endPoint;
			moveVelocity = endPoint / moveTime;
		}else
		{
			transform.position = new Vector3(initialPoint.x, initialPoint.y, 0);
			mFrom = initialPoint;
			mTo = endPoint;
		}
		
	}
	void Update () {
		if (paused)
		{
			pauseTimer += Time.deltaTime;
			if (pauseTimer >= pauseTime)
			{
				pauseTimer = 0.0f;
				paused = false;
			}
		}
		else
		{
			moveTimer += Time.deltaTime;
			if (moveTimer >= moveTime)
			{
				moveTimer = 0.0f;
				Vector2 temp = mFrom;
				mFrom = mTo;
                mTo = temp;
                paused = true;
            }
            if (moveParent) {
				transform.parent.position = new Vector3(transform.parent.position.x + moveVelocity.x * Time.deltaTime, transform.parent.position.y + moveVelocity.y * Time.deltaTime,0f);
			} else {
				transform.position = new Vector3(transform.position.x + moveVelocity.x * Time.deltaTime, transform.position.y + moveVelocity.y * Time.deltaTime,0f);
			}
		}
	}
}
