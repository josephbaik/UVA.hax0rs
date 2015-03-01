using UnityEngine;
using System.Collections;

public class ScrollingWallpaper : MonoBehaviour {
	public GameObject circle, square, triangle;
	private Camera cam;
	private Vector2 pos;
	public int threshold = 0;
	GameObject elem;
	float finterval = 9, interval = 7, start = 0;
	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera>();
		pos = new Vector2(transform.position.x, transform.position.y);
        
	}
	
	// Update is called once per frame
	void Update () {
		start += interval * Time.deltaTime;
		if(transform.position.y >= pos.y + threshold && finterval <= start){
			
			switch(Random.Range(0, 2)){
				case 0:
					elem = Instantiate(circle, new Vector3(Random.Range(-17f, 17f), transform.position.y+14, 10), Random.rotation) as GameObject;
					break;
				case 1:
					elem = Instantiate(square, new Vector3(Random.Range(-17f, 17f), transform.position.y+14, 10), Random.rotation) as GameObject;
                    break;
                case 2:
					elem = Instantiate(triangle, new Vector3(Random.Range(-17f, 17f), transform.position.y+14, 10), Random.rotation) as GameObject;
					break;
				default:
					break;
            }  
            switch(Random.Range(1, 5)){
				case 1:
					elem.renderer.material.color = new Color(1,0.5f,0.5f, 0.5f); //C#
					break;
				case 2:
					elem.renderer.material.color = new Color(0.5f,1,0.5f, 0.5f);
					break;
				case 3:
					elem.renderer.material.color = new Color(0.5f,0.5f,1, 0.5f);
					break;
				case 4:
					elem.renderer.material.color = new Color(1f,0.5f,1, 0.5f);
                    break;
                case 5:
                    elem.renderer.material.color = new Color(0.5f,1f,1f, 0.5f);
                    break;
                default:
                    elem.renderer.material.color = new Color(1,1,1);
                    break;
            }
            start = 0;
        }
        
        
	}
}
