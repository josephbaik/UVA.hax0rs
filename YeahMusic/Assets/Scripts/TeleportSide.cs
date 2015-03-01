using UnityEngine;
using System.Collections;

public class TeleportSide : MonoBehaviour {

	public Transform other;
	public float xoffset;
	public bool rightSide;

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
			if (rightSide)
				col.transform.position = new Vector3(other.transform.position.x + xoffset,
					col.transform.position.y, col.transform.position.z);
			else
				col.transform.position = new Vector3(other.transform.position.x - xoffset,
					col.transform.position.y, col.transform.position.z);
		}
	}
}
