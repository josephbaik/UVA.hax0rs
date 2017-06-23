using UnityEngine;
using System.Collections;

public class ScrollingWallpaper : MonoBehaviour {
	public Transform parent;
	public GameObject circle, square, triangle;
	public float layerThreshold;
	public int shapesPerLayer;
	private Camera cam;
	private float posY;
	private int shapesCounter = 0;
	private float finterval = 9;
	private float interval = 7;
	private float start = 0;
	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera>();
		posY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		start += interval;

		if (transform.position.y >= posY + layerThreshold) {
			posY = transform.position.y;
			shapesCounter = 0;
		}

		if (shapesCounter < shapesPerLayer && finterval <= start) {
			GameObject elem = null;
			switch(Random.Range(0, 2)){
				case 0:
					elem = Instantiate(circle, new Vector3(Random.Range(-17f, 17f), transform.position.y+14 + Random.Range(-2f, 2f), 10), Random.rotation) as GameObject;
					break;
				case 1:
					elem = Instantiate(square, new Vector3(Random.Range(-17f, 17f), transform.position.y+14 + Random.Range(-2f, 2f), 10), Random.rotation) as GameObject;
                    break;
                case 2:
					elem = Instantiate(triangle, new Vector3(Random.Range(-17f, 17f), transform.position.y+14 + Random.Range(-2f, 2f), 10), Random.rotation) as GameObject;
					break;
				default:
					break;
            }  
            switch(Random.Range(1, 5)){
				case 1:
					elem.GetComponent<Renderer>().material.color = new Color(1,0.5f,0.5f, 0.3f*AudioController.volume/100); //C#
					break;
				case 2:
					elem.GetComponent<Renderer>().material.color = new Color(0.5f,1,0.5f, 0.3f*AudioController.volume/100);
					break;
				case 3:
					elem.GetComponent<Renderer>().material.color = new Color(0.5f,0.5f,1, 0.3f*AudioController.volume/100);
					break;
				case 4:
					elem.GetComponent<Renderer>().material.color = new Color(1f,0.5f,1, 0.3f*AudioController.volume/100);
                    break;
                case 5:
					elem.GetComponent<Renderer>().material.color = new Color(0.5f,1f,1f, 0.3f*AudioController.volume/100);
                    break;
                default:
                    elem.GetComponent<Renderer>().material.color = new Color(1,1,1);
                    break;
            }
			elem.transform.parent = this.parent;

			start = 0;
			shapesCounter += 1;
        }
        
        
	}
}
