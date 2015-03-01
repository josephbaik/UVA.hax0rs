using UnityEngine;
using System.Collections;

public class DeathTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			Application.LoadLevel("Death");
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

}
