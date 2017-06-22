using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class OneWay : MonoBehaviour {

	public int collideLayer = 0;
	public int invisibleLayer = 8;
	private GameObject player;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	void Update () 
		//Has to check player velocity every update, else additional triggers would be necessary
	{
		//if the player is still or moving down, and his position is above this platform's position, then collide
		//Note that player must have a circle collider and platform must have box collider
//		if (player.rigidbody2D.velocity.y <= 0 && 
//		    player.transform.position.y - player.GetComponent<CircleCollider2D>().radius >= 
//		    transform.position.y + GetComponent<BoxCollider2D>().center.y + GetComponent<BoxCollider2D>().size.y / 2) {
//			this.gameObject.layer = collideLayer;
//			//switches to the default (collidable) layer
//		}
		
		Debug.Log("player bruh" + player.GetComponent<Rigidbody2D>().velocity.y);
		if (player.GetComponent<Rigidbody2D>().velocity.y <= 0.1f && 
		    player.transform.position.y - player.GetComponent<BoxCollider2D>().size.y/2 >= 
		    transform.position.y + GetComponent<BoxCollider2D>().offset.y + GetComponent<BoxCollider2D>().size.y / 2) {
			this.gameObject.layer = collideLayer;
			//switches to the default (collidable) layer
		}  
		//otherwise, don't collide
		else {
			this.gameObject.layer = invisibleLayer;
			//switches to a special layer the player can pass through
		}
	}
}
