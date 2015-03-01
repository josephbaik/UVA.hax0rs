using UnityEngine;
using System.Collections;


public class AudioController : MonoBehaviour {
	
	public AudioClip[] percussion;
	public AudioClip[] bass;
	public AudioClip[] chiptune;
	public AudioClip[] emotive;
	public AudioClip[] noisy;

	private AudioSource soundp;
	private AudioSource soundb;
	private AudioSource soundc;
	private AudioSource sounde;
	private AudioSource soundn;

	public GameObject lastPlatform;

	public GameObject platform;
	public GameObject powerup;
	public GameObject spring;

	private ArrayList platforms;
	public int interval;
	public int finterval;
	private int start;
	private Vector2 pos;
	private Vector2 pos1;

	public float spawnFloor = 0f;
	public float spawnOffset = 2f;
	public float timeUntilTwo = 15f;
	public float timeUntilThree = 30f;
	public float timeUntilFour = 45f;
	public float timeUntilFive = 45f;
	public float probSecond = 0.6f;
	public float probThird = 0.35f;
	public float probFourth = 0.2f;
	public float probFifth = 0.1f;
	private float timer = 0f;
	public float maxYSpawnOffset = 50f;

	public float springChance = 0.07f;
	public float powerupChance = 0.02f;
	public float blastTime = 10f;
	public float blastTimer = 0f;
	public bool blastOn = false;

	private GameObject player;
	private PlayerBallControl playa;
	
	// Use this for initialization
	void Start () {
		start = 0;	
		platforms = new ArrayList();
		Vector2 playerpos = new Vector2(transform.position.x, transform.position.y);
		//player = Instantiate(player, playerpos, Quaternion.identity) as GameObject;
		player = GameObject.FindGameObjectWithTag ("Player");
		playa = player.GetComponent<PlayerBallControl>();
		spawnFloor = player.transform.position.y;

		if (percussion.Length > 0) {
			int i = Random.Range(0, percussion.Length - 1);
			soundp = gameObject.AddComponent<AudioSource>();
			soundp.clip = percussion[i];
			soundp.loop = true;
			soundp.volume = 0f;
			soundp.Play();
		}
		if (bass.Length > 0) {
			int i = Random.Range(0, bass.Length - 1);
			soundb = gameObject.AddComponent<AudioSource>();
			soundb.clip = bass[i];
			soundb.loop = true;
			soundb.volume = 0f;
			soundb.Play();

		}
		if (chiptune.Length > 0) {
			int i = Random.Range(0, chiptune.Length - 1);
			soundc = gameObject.AddComponent<AudioSource>();
			soundc.clip = chiptune[i];
			soundc.loop = true;
			soundc.volume = 0f;
			soundc.Play();
		}
		if (emotive.Length > 0) {
			int i = Random.Range(0, emotive.Length - 1);
			sounde = gameObject.AddComponent<AudioSource>();
			sounde.clip = emotive[i];
			sounde.loop = true;
			sounde.volume = 0f;
			sounde.Play();
		}
		if (noisy.Length > 0) {
			int i = Random.Range(0, noisy.Length - 1);
			soundn = gameObject.AddComponent<AudioSource>();
			soundn.clip = noisy[i];
			soundn.loop = true;
			soundn.volume = 0f;
			soundn.Play();
		}
	}
	
	// Update is called once per frame
	void Update () {
		start += interval;
		timer += Time.deltaTime;

		if(start >= finterval && lastPlatform == null || (lastPlatform != null && lastPlatform.transform.position.y - player.transform.position.y < maxYSpawnOffset)){
			start = 0;

			//pos = new Vector2(transform.position.x + Random.Range(-10, 10), Mathf.Max(transform.position.y, player.transform.position.y + spawnPlatformOffset));
			//pos = new Vector2(transform.position.x + Random.Range(-17, 17), transform.position.y);
			spawnFloor += spawnOffset;
			if (lastPlatform != null) {
				float x = Random.Range(-11f, 11f) + lastPlatform.transform.position.x;
				if (x < -17f)
					x = -17f + (-17f - x);
				else if (x > 17f)
					x = 17f - (x - 17f);
				pos = new Vector2(x, lastPlatform.transform.position.y + spawnOffset);
				
				
			}
			else{
				pos = new Vector2(Random.Range(-11f, 11f) + player.transform.position.x, player.transform.position.y + spawnOffset);
			}
			//platform = Instantiate(platform, pos, Quaternion.identity) as GameObject;
			
			pos1 = new Vector2(player.transform.position.x + Random.Range(-11f, 11f), pos.y);

			GameObject pla;
			float prob2 = Random.value;
			if (prob2 < springChance)
			{
				lastPlatform = Instantiate(spring, pos, Quaternion.identity) as GameObject;
				pla = Instantiate(spring, pos1, Quaternion.identity) as GameObject;
			} else {
				lastPlatform = Instantiate(platform, pos, Quaternion.identity) as GameObject;
				pla = Instantiate(platform, pos1, Quaternion.identity) as GameObject;
			}

			Platform plat = lastPlatform.GetComponent<Platform>();
			Platform plapla = pla.GetComponent<Platform>();
			

			float prob = Random.value;
			if (timer > timeUntilFive && prob < probFifth) {
				plat.type = 5;
				plapla.type = 5;	
			}
			else if (timer > timeUntilFour && prob < probFourth) {
				plat.type = 4;
				plapla.type = 4;

			} else if (timer > timeUntilThree && prob < probThird) {
				plat.type = 3;
				plapla.type = 3;

			} else if (timer > timeUntilTwo && prob < probSecond) {
				plat.type = 2;
				plapla.type = 2;

			} else {
				plat.type = 1;
				plapla.type = 1;
			}
			platforms.Add(platform);

			float prob3 = Random.value;
			if (prob3 < powerupChance)
			{
				Vector2 powerpos = new Vector2(Random.Range(-10f, 10f), pos.y + Random.Range(1f, 3f));
				GameObject powerobj = Instantiate(powerup, powerpos, Quaternion.identity) as GameObject;
			}

			
		}
		
		switch(playa.collisionType){
		case 1:
			soundp.volume = 1;
			break;
		case 2:
			soundb.volume = 1;
			break;
		case 3:
			soundc.volume = 1;
			break;
		case 4:
			sounde.volume = 1;
			break;
		case 5:
			soundn.volume = 1;
			break;
		
        default:
            break;
        }

		if (blastOn)
			blastTimer += Time.deltaTime;
		if (blastTimer > blastTime)
			blastOn = false;

		if(soundp.volume > 0 && !blastOn){
			soundp.volume = soundp.volume -  .1f*Time.deltaTime;
        }
		if(soundb.volume > 0 && !blastOn){
			soundb.volume = soundb.volume -  .1f*Time.deltaTime;
		}        
		if(soundc.volume > 0 && !blastOn){
			soundc.volume = soundc.volume -  .1f*Time.deltaTime;
		}
		if(sounde.volume > 0 && !blastOn){
			sounde.volume = sounde.volume -  .1f*Time.deltaTime;
		}
		if(soundn.volume > 0 && !blastOn){
			soundn.volume = soundn.volume -  .1f*Time.deltaTime;
		}

		float volume = (soundp.volume + soundb.volume + soundc.volume + sounde.volume + soundn.volume)*100/5;
		GUICounter.volume = (int)volume; 
		GUICounter.scores += (volume / 10) *  (Time.deltaTime);
		
		playa.collisionType = 0;
    }

	public void BlastAll() {
		soundp.volume = 1f;
		soundb.volume = 1f;
		soundc.volume = 1f;
		sounde.volume = 1f;
		soundn.volume = 1f;
		blastOn = true;
		blastTimer = 0f;
	}
}
