using UnityEngine;
using System.Collections;


public class AudioController : MonoBehaviour {
	private AudioSource beat  ;
	private AudioSource synth;
	private AudioSource harp;
	private AudioSource[] sources;
	private AudioListener listener;
	int volume;
	
	public GameObject platform;
	private ArrayList platforms;
	public int interval;
	public int finterval;
	private int start;
	private Vector2 pos;

	public float spawnFloor = 0f;
	private float spawnOffset = 2f;
	public float timeUntilTwo = 15f;
	public float timeUntilThree = 30f;
	public float timeUntilFour = 45f;
	public float probSecond = 0.6f;
	public float probThird = 0.35f;
	public float probFourth = 0.2f;
	private float timer = 0f;
	public float maxYSpawnOffset = 50f;

	private GameObject player;
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
		//player = Instantiate(player, playerpos, Quaternion.identity) as GameObject;
		player = GameObject.FindGameObjectWithTag ("Player");
		playa = player.GetComponent<PlayerBallControl>();
		spawnFloor = player.transform.position.y;
		listener = GetComponent<AudioListener>();
	}
	
	// Update is called once per frame
	void Update () {
		start += interval;
		timer += Time.deltaTime;

		if(start >= finterval && spawnFloor - player.transform.position.y < maxYSpawnOffset){
			start = 0;

			//pos = new Vector2(transform.position.x + Random.Range(-10, 10), Mathf.Max(transform.position.y, player.transform.position.y + spawnPlatformOffset));
			//pos = new Vector2(transform.position.x + Random.Range(-17, 17), transform.position.y);
			spawnFloor += spawnOffset;
			pos = new Vector2(transform.position.x + Random.Range(-10, 10), spawnFloor);

			platform = Instantiate(platform, pos, Quaternion.identity) as GameObject;
			Platform plat = platform.GetComponent<Platform>();

			float prob = Random.value;
			if (timer > timeUntilFour && prob < probFourth) {
				plat.type = 4;

			} else if (timer > timeUntilThree && prob < probThird) {
				plat.type = 3;

			} else if (timer > timeUntilTwo && prob < probSecond) {
				plat.type = 2;

			} else {
				plat.type = 1;
			}
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
            break;
        }
		
		if(beat.volume > 0){
			beat.volume = beat.volume -  .1f*Time.deltaTime;
        }
		if(synth.volume > 0){
			synth.volume = synth.volume -  .1f*Time.deltaTime;
		}
		if(harp.volume > 0){
			harp.volume = harp.volume - .1f*Time.deltaTime;
        }
        
        playa.collisionType = 0;
        float volume = (beat.volume + synth.volume + harp.volume)*100/3;
        GUICounter.volume = (int)volume; 
        GUICounter.scores += (volume / 10) *  (Time.deltaTime);
    }
    
    IEnumerator MyCoroutine()
	{
		yield return new WaitForSeconds(20);    //Wait one frame
    }
}
