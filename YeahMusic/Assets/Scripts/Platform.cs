﻿using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {
	public int type;
	private bool playerContact;
	private BoxCollider2D hitbox;
	
	// Use this for initialization
	void Start () {
//		type = Random.
		switch (type){
		case 1:
			renderer.material.color = new Color(1,0.5f,0.5f); //C#
			break;
		case 2:
			renderer.material.color = new Color(0.5f,1,0.5f);
			break;
		case 3:
			renderer.material.color = new Color(0.5f,0.5f,1);
			break;
		default:
			renderer.material.color = new Color(1,1,1);
			break;
		}
		
		playerContact = false;
		hitbox = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(new Vector2(0,-IncrementTowards(0,5,6)));
	}
	
	public static float IncrementTowards (float c, float a, float t){
		if (t == c)
			return c;
		else {
			float dir = Mathf.Sign(t - c);
			c += a * Time.deltaTime * dir;
			return (dir == Mathf.Sign(t-c))? c: t;
        }
    }
	
	void OnCollisionEnter2D(Collision2D collision) {
		playerContact = true;
		MyCoroutine();
		playerContact = false;
	}
	
	IEnumerator MyCoroutine()
	{
		
		
		yield return new WaitForSeconds(2);    //Wait one frame
		
		
    }
}
