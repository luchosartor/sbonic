using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public bool jump = false;
	Vector3 starting = new Vector3(0f, 0f, 0f);
	public static Player instance;
	AudioSource source;
	public AudioClip song;
	public AudioClip eggman_song;
	// Use this for initialization
	void Awake(){
		if (!instance) {
			instance = this;
		} else {
			Destroy (this);
		}
	}
	void Start () {
		source = GetComponent<AudioSource> ();
		source.clip = song;
		source.volume = .05f;
		source.loop = true;
		source.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {
			jump = true;
			GameMananger.instance.PlayJump ();
		}
	}

	void OnCollisionEnter(Collision c){
		if (c.gameObject.tag.Equals ("Finish") && !Eggman.play) {
			Eggman.play = true;
			GameMananger.instance.AddEggmanLifes ();
			PlayEggmanSong ();
		}
		if (c.gameObject.tag.Equals ("Piso") && jump) {
			jump = false;
		}
	}

	public void PlayEggmanSong(){
		source.Stop ();
		source.clip = eggman_song;
		source.loop = true;
		source.volume = .05f;
		source.Play ();
	}
}
