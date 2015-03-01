using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

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
			Destroy(gameObject);
		}
	}
}
