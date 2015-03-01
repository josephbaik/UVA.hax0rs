using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	private GameObject fadeObj;
	// Use this for initialization
	void Start () {
		fadeObj = GameObject.FindGameObjectWithTag ("ScreenFader");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnMouseDown(){
		if (GetComponent<AudioSource> () != null)
			GetComponent<AudioSource> ().Play ();
		fadeObj.GetComponent<ScreenFading>().Transition(GameTransition);

	}

	public void GameTransition() {
		Application.LoadLevel("JoeCopy");
		AudioController.inGame = true;
		//AudioController.Instance.Reset ();
	}
}
