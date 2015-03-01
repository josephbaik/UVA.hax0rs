using UnityEngine;
using System.Collections;

public class TeleportSide : MonoBehaviour {

	public Transform other;
	public float xoffset;

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
			col.transform.position = new Vector3(other.transform.position.x + xoffset,
				col.transform.position.y, col.transform.position.z);
		}
	}
}
