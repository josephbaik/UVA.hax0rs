using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {
	private AudioSource beat;
	private AudioSource synth;
	private AudioSource harp;
	private AudioSource[] sources;
	
	public GameObject platform;
	private GameObject[] platforms;
	public int interval;
	public int finterval;
	private int start;
	private Vector2 pos;
	
	// Use this for initialization
	void Start () {
		sources = GetComponents<AudioSource>();
		synth = sources[0];
		harp = sources[1];
		beat = sources[2];
		start = 0;	
	}
	
	// Update is called once per frame
	void Update () {
		start += interval;
		
		if(start >= finterval){
			start = 0;
			pos = new Vector2(transform.position.x + Random.Range(-10, 10), transform.position.y );
			platform = Instantiate(platform, pos, Quaternion.identity) as GameObject;
		}
	}
}
