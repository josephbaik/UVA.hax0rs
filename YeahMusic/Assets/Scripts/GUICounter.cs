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

	private Color[] rainbow = {Color.red, Color.magenta, Color.blue, Color.green, Color.yellow};
	private int rainbowIndex;
	private float rainbowTimer = 0f;
	private float rainbowTime = 0.5f;

	private float time = 0f;
	private GameObject controllerobj;
	// Use this for initialization
	void Start () {	
		controllerobj = GameObject.FindGameObjectWithTag ("GameController");
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		int timeSeconds = (int)time;

		if (controllerobj.GetComponent<AudioController> ().blastOn) {
			rainbowTimer += Time.deltaTime;
			if (rainbowTimer > rainbowTime) {
				rainbowTimer = 0f;
				rainbowIndex = (rainbowIndex + 1) % rainbow.Length;
			}
			GetComponent<GUIText>().color = rainbow[rainbowIndex];
		} else {
			GetComponent<GUIText>().color = Color.white;
		}

		//todo: add in volume
		GetComponent<GUIText> ().text = prefix1 + timeSeconds + 
			"\n" + prefix2 + volume +  
				"\n" + prefix3 + string.Format("{0:0}", scores);
	}
}
