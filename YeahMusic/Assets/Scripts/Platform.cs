using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {
	public int type;
	private bool playerContact;
	private BoxCollider hitbox;
	
	// Use this for initialization
	void Start () {
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
		hitbox = GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
