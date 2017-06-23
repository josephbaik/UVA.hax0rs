using UnityEngine;
using System.Collections;

public class SelfRemove : MonoBehaviour {

	private Transform cam;
	private float threshold = 25f;

	void Start() {
		cam = GameObject.FindGameObjectWithTag ("MainCamera").transform;
	}

	// Update is called once per frame
	void Update () {
		if (cam.position.y > this.transform.position.y + this.threshold) {
			Destroy (gameObject);
		}
	}
}
