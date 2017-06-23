using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	private GameObject fadeObj;
	private bool isTransitioning;

	// Use this for initialization
	void Start () {
		fadeObj = GameObject.FindGameObjectWithTag ("ScreenFader");
		isTransitioning = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Submit")) {
			StartTransition();
		}
		
		if (Input.GetButtonDown("Cancel")) {
			Application.Quit();
		}
	}

	public void OnMouseDown(){
		StartTransition();
	}

	private void StartTransition() {
		if (!isTransitioning) {
			if (GetComponent<AudioSource> () != null)
				GetComponent<AudioSource> ().Play ();
			fadeObj.GetComponent<ScreenFading> ().Transition (GameTransition);
			isTransitioning = true;
		}
	}

	public void GameTransition() {
		Application.LoadLevel("JoeCopy");
		AudioController.inGame = true;
	}
}
