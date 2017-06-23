using UnityEngine;
using System.Collections;

public class DeathTest : MonoBehaviour {

	private bool playerContact = false;
	private GameObject fadeObj;
	// Use this for initialization
	void Start () {
		fadeObj = GameObject.FindGameObjectWithTag ("ScreenFader");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col)
	{

		if (col.tag == "Player" && !playerContact)
		{
			if (GetComponent<AudioSource>() != null) {
				GetComponent<AudioSource>().Play ();
			}
			fadeObj.GetComponent<ScreenFading>().Transition(DeathTransition);
			playerContact = true;
		}
	}

	public void DeathTransition() {
		Application.LoadLevel("Death");
		AudioController.inGame = false;
	}

}
