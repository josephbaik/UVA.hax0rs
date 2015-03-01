using UnityEngine;
using System.Collections;


public class AudioController : MonoBehaviour {
	private AudioSource beat;
	private AudioSource synth;
	private AudioSource harp;
	private AudioSource[] sources;
	
	public GameObject platform;
	private ArrayList platforms;
	public int interval;
	public int finterval;
	private int start;
	private Vector2 pos;
	
	public GameObject player;
	private PlayerBallControl playa;
	
	// Use this for initialization
	void Start () {
		sources = GetComponents<AudioSource>();
		synth = sources[0];
		harp = sources[1];
		beat = sources[2];
		start = 0;	
		platforms = new ArrayList();
		Vector2 playerpos = new Vector2(transform.position.x, transform.position.y);
		player = Instantiate(player, playerpos, Quaternion.identity) as GameObject;
		playa = player.GetComponent<PlayerBallControl>();
	}
	
	// Update is called once per frame
	void Update () {
		start += interval;
		
		if(start >= finterval){
			start = 0;
			
			
			pos = new Vector2(transform.position.x + Random.Range(-10, 10), transform.position.y );
			platform = Instantiate(platform, pos, Quaternion.identity) as GameObject;
			Platform plat = platform.GetComponent<Platform>();
			plat.type = 1;
			platforms.Add(platform);
		}
		
		switch(playa.collisionType){
		case 1:
			beat.volume = 1;
			break;
		case 2:
			synth.volume = 1;
			break;
		case 3:
			harp.volume = 1;
            break;
        default:
//            renderer.material.color = new Color(1,1,1);
            break;
        }
		
		if(beat.volume > 0){
			beat.volume = beat.volume -  .0001f*Time.deltaTime;
        }
		if(synth.volume > 0){
			synth.volume -= .0001f*Time.deltaTime;
		}
		if(harp.volume > 0){
			harp.volume -= .0001f*Time.deltaTime;
        }
    }
    
    IEnumerator MyCoroutine()
	{
		yield return new WaitForSeconds(20);    //Wait one frame
    }
}
