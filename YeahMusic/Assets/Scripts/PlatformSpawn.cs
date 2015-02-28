using UnityEngine;
using System.Collections;
//using System;

public class PlatformSpawn : MonoBehaviour {

	//assigned from editor
	public Transform platform;
	public Transform movingPlatform;
	public Transform spring;
	//and other stuff

	public float springChance = 0.1f;
	public float movingChance = 0.2f;
	public float movingXAxisChance = 0.5f;	//else y axis
	public float movingMinRange = 3f;
	public float movingMaxRange = 5f;

	public float minXDist = 4f;
	public float minYDist = 4f;
	public float maxXDist = 10f;
	public float maxYDist = 10f;

	private System.Random rand;
	// Use this for initialization
	void Start () {
		rand = new System.Random ();
		GenerateBlock (-10f, 10f, 0f, 10f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GenerateBlock(float xmin, float xmax, float ymin, float ymax)
	{
		float yfloor = ymin;
		float xfloor = 0f;
		while (yfloor < ymax) {

			int s1 = (rand.Next() % 2) == 0 ? 1 : -1;
			float x = (float)(((rand.NextDouble() * (maxXDist - minXDist)) + minXDist) * s1 + xfloor);
			float y = (float)(((rand.NextDouble() * (maxYDist - minYDist)) + minYDist) + yfloor);
			double c1 = rand.NextDouble();
			if (c1 < springChance) {
				Transform obj = Instantiate(spring, new Vector3(x, y, 0f), Quaternion.identity) as Transform;
			}
			else if (c1 < movingChance) {
				double c2 = rand.NextDouble();
				Transform obj = Instantiate(movingPlatform, new Vector3(x, y, 0f), Quaternion.identity) as Transform;
				MovingPlatform objscript = obj.GetChild(0).GetComponent<MovingPlatform>();
				int s2 = (rand.Next() % 2) == 0 ? 1 : -1;
				float endcoord = (float)((rand.NextDouble() * (movingMaxRange - movingMinRange) + movingMinRange) * s2);				                      
				if (c2 < movingXAxisChance) {
					objscript.useCurrentStartPosition = true;
					objscript.endPoint = new Vector2(endcoord, 0f);
				}
				else {
					objscript.useCurrentStartPosition = true;
					objscript.endPoint = new Vector2(0f, endcoord);
				}
			}
			else {
				Transform obj = Instantiate(platform, new Vector3(x, y, 0f), Quaternion.identity) as Transform;
			}

			//embed type information here

			yfloor = y;
			xfloor = x;
		}
	}
}
