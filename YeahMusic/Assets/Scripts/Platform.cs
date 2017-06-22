using UnityEngine;
using System.Collections;

//PLATFORM FOR HACK.UVA
public class Platform : MonoBehaviour {
	public int type;
	private bool platContact = false;
	// Use this for initialization
	void Start () {
		switch (type){
		case 1:
			GetComponent<Renderer>().material.color = new Color(1,0.5f,0.5f); //C#
			break;
		case 2:
			GetComponent<Renderer>().material.color = new Color(0.5f,1,0.5f);
			break;
		case 3:
			GetComponent<Renderer>().material.color = new Color(0.5f,0.5f,1);
			break;
		case 4:
			GetComponent<Renderer>().material.color = new Color(1f,0.5f,1);
			break;
		case 5:
			GetComponent<Renderer>().material.color = new Color(0.5f,1f,1f);
			break;
		default:
			GetComponent<Renderer>().material.color = new Color(1,1,1);
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
//		transform.Translate(new Vector2(0,-IncrementTowards(0,5,6)));
	}
	
	public static float IncrementTowards (float c, float a, float t){
		if (t == c)
			return c;
		else {
			float dir = Mathf.Sign(t - c);
			c += a * Time.deltaTime * dir;
			return (dir == Mathf.Sign(t-c))? c: t;
        }
    }
	
	void OnCollisionEnter2D(Collision2D collision) {
		if (!platContact) {
			platContact = true;
			GUICounter.scores += 5;
		}
	}
	

}
