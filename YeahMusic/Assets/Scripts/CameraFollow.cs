﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour 
{
	//Vars for following the player
	public float yMargin = 1f;		// Distance in the y axis the player can move before the camera follows.
	public float ySmooth = 8f;		// How smoothly the camera catches up with it's target movement in the y axis.
	public Vector2 maxXAndY;		// The maximum x and y coordinates the camera can have.
	public Vector2 minXAndY;		// The minimum x and y coordinates the camera can have.

	private Transform player;		// Reference to the player's transform.

	void Awake ()
	{
		// Setting up the reference.
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void LateUpdate ()
	{
		TrackPlayer ();
	}

	void TrackPlayer ()
	{
		// By default the target x and y coordinates of the camera are it's current x and y coordinates.
		float targetY = transform.position.y;
		
		if (transform.position.y < minXAndY.y && player.position.y < minXAndY.y)
			targetY = Mathf.Lerp(transform.position.y, minXAndY.y, ySmooth * Time.deltaTime);
		else if (transform.position.y > maxXAndY.y && player.position.y > maxXAndY.y)
			targetY = Mathf.Lerp(transform.position.y, maxXAndY.y, ySmooth * Time.deltaTime);
		// If the player has moved beyond the y margin...
		//only move up, not down
		else if(player.position.y - transform.position.y > yMargin)
		{
			// ... the target y coordinate should be a Lerp between the camera's current y position and the player's current y position.
			targetY = Mathf.Lerp(transform.position.y, player.position.y, ySmooth * Time.deltaTime);
		}
		
		// The target x and y coordinates should not be larger than the maximum or smaller than the minimum.
		//targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
		//targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

		// Set the camera's position to the target position with the same z component.
		transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
	}
}
