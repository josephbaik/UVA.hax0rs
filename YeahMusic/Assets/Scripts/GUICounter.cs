using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUIText))]
public class GUICounter : MonoBehaviour {

	public string prefix1 = "Time:";
	public string prefix2 = "Volume:";
	public string prefix3 = "Score:";
	public static int scores = 0;

	private float time = 0f;
	// Use this for initialization
	void Start () {	
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		int timeSeconds = (int)time;

		//todo: add in volume
		GetComponent<GUIText> ().text = prefix1 + timeSeconds + "\n" + prefix2 + "\n" + prefix3 + scores;
	}
}
