using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	public AudioClip contactNoise;
	public float noiseVolume = 1f;

	private GameObject obj;
	// Use this for initialization
	void Start () {
		obj = GameObject.FindGameObjectWithTag ("GameController");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			obj.GetComponent<AudioController>().BlastAll();

			//must create another object to play the noise since this one will die.
			if (contactNoise != null) {
				//we create a dummy object for the noise since this one will get killed
				GameObject noiseobj = new GameObject();
				noiseobj.transform.position = this.transform.position;
				AudioSource src = noiseobj.AddComponent<AudioSource>();
				src.clip = contactNoise;
				src.volume = noiseVolume;
				noiseobj.AddComponent<SelfRemove>();	//default 10 s
				src.Play();
			}
			
			Destroy(gameObject);
		}
	}
}
