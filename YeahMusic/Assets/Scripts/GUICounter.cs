using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUIText))]
public class GUICounter : MonoBehaviour {

	public string prefix1 = "Time:";
	public string prefix2 = "Volume:";
	public string prefix3 = "Score:";
	public static float scores = 0;
	[HideInInspector]
	public static float volume = 0;

	private float time = 0f;
	// Use this for initialization
	void Start () {	
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		int timeSeconds = (int)time;

		//todo: add in volume
		GetComponent<GUIText> ().text = prefix1 + timeSeconds + 
			"\n" + prefix2 + volume +  
				"\n" + prefix3 + string.Format("{0:0.00}", scores);
	}
}
