using UnityEngine;
using System.Collections;

public class DeathTest : MonoBehaviour {

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

		if (col.tag == "Player")
		{
			if (GetComponent<AudioSource>() != null) {
				GetComponent<AudioSource>().Play ();
			}
			fadeObj.GetComponent<ScreenFading>().Transition(DeathTransition);
		}

		if (col.GetComponent<Platform>() != null)
		{
			for(int i = 0; i < col.transform.childCount; i++)
				if (col.transform.GetChild(i).tag == "Player") {
					col.transform.GetChild(i).parent = null;
				}
			Destroy(col.gameObject);
		}
	}

	public void DeathTransition() {
		Application.LoadLevel("Death");
		AudioController.inGame = false;
		GUICounter.scores = 0;
	}

}
