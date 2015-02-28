using UnityEngine;
using System.Collections;

public class Spring : MonoBehaviour {
	public float springForce = 4000f;
	public AudioClip contactNoise;
	public float noiseVolume = 1f;

	public const int springJumpFrameThreshold = 10;

	private float orientation;
	private Vector2 direction;
	private AudioSource noiseSrc;

	void Awake() {
		orientation = (this.gameObject.transform.rotation.eulerAngles.z);
		direction = new Vector2(- Mathf.Sin(orientation * Mathf.PI / 180), Mathf.Cos(orientation * Mathf.PI / 180));
		if (contactNoise != null){
			noiseSrc = gameObject.AddComponent<AudioSource> ();
			noiseSrc.clip = contactNoise;
			noiseSrc.volume = noiseVolume;
		}
	}


	public void SpringCollide(GameObject player) {
		if (Time.frameCount - player.GetComponent<PlayerBallControl>().jumpFrame > springJumpFrameThreshold)
		{
			player.rigidbody2D.AddForce (springForce * direction);
			player.GetComponent<PlayerBallControl>().springFrame = Time.frameCount;
			//Debug.Log ("Spring bounce " + Time.frameCount);
			if (noiseSrc != null)
				noiseSrc.Play();
		}
	}

}