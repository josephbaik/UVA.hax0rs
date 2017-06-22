using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(GUITexture))]
public class ScreenFading : MonoBehaviour {

	public Color opaqueColor = Color.black;
	public float fadeSpeed = 4.0f;
	public bool fadeOutOnStart = false;

	public bool fadeMusic = false;
	public GameObject musicObj = null;

	private bool fadingIn = false;
	private bool fadingOut = false;
	private float inThreshold = 0.95f;
	private float outThreshold = 0.05f;
	private Action transitionFunc = null;
	
	void Awake()
	{
		GetComponent<GUITexture>().pixelInset = new Rect (0f, 0f, Screen.width, Screen.height);
		GetComponent<GUITexture>().color = Color.clear;
		if (fadeOutOnStart)
		{
			fadeMusic = true;
			if (musicObj != null)
				musicObj.GetComponent<AudioSource>().volume = 0.0f;
			GetComponent<GUITexture>().color = opaqueColor;
			fadingOut = true;
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if (fadingIn)
		{
			GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, opaqueColor, fadeSpeed * Time.deltaTime);
			if (fadeMusic && musicObj != null)
			{
				musicObj.GetComponent<AudioSource>().volume = Mathf.Lerp(musicObj.GetComponent<AudioSource>().volume, 0.0f, fadeSpeed * Time.deltaTime);
			}
			if (GetComponent<GUITexture>().color.a >= inThreshold)
			{
				GetComponent<GUITexture>().color = opaqueColor;
				fadingIn = false;
				if (transitionFunc != null)
				{
					transitionFunc();
					fadingOut = true;
				}
			}
		}
		else if (fadingOut)
		{
			GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.clear, fadeSpeed * Time.deltaTime);
			if (fadeMusic && musicObj != null)
			{
				musicObj.GetComponent<AudioSource>().volume = Mathf.Lerp(musicObj.GetComponent<AudioSource>().volume, 1.0f, fadeSpeed * Time.deltaTime);
			}
			if (GetComponent<GUITexture>().color.a <= outThreshold)
			{
				GetComponent<GUITexture>().color = Color.clear;
				fadingOut = false;
			}
		}
	}

	public void Transition(Action transitionAction, bool music = false)
	{
		if (fadingIn)
			return;		//deny a new fade in
		fadingIn = true;
		fadeMusic = music;
		transitionFunc = transitionAction;
	}

	public bool IsTransitioning()
	{
		return fadingIn;
	}
}
