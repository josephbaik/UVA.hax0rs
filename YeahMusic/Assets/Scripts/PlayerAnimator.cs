using UnityEngine;
using System.Collections;

public class PlayerAnimator : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		int contactType = GetComponent<PlayerBallControl>().collisionType;

		switch(contactType){
		case 1:
			foreach(Renderer r in GetComponentsInChildren<Renderer>()){
				r.material.color = new Color(1,0.5f,0.5f);
			}
			break;
		case 2:
			foreach(Renderer r in GetComponentsInChildren<Renderer>()){
				r.material.color = new Color(0.5f,1,0.5f);
			}
			break;
		case 3:
			foreach(Renderer r in GetComponentsInChildren<Renderer>()){
				r.material.color = new Color(0.5f,0.5f,1);
			}
			break;
		case 4:
			foreach(Renderer r in GetComponentsInChildren<Renderer>()){
				r.material.color = new Color(1f,0.5f,1);
			}
			break;
		case 5:
			foreach(Renderer r in GetComponentsInChildren<Renderer>()){
				r.material.color = new Color(0.5f,1f,1f);
			}
			break;
		default:
			break;
		}
	}
}
