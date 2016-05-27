using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	public bool jump = false;
	Vector3 starting = new Vector3(0f, 0f, 0f);
	public static Player instance;
	AudioSource source;
	public AudioClip song;
	public AudioClip eggman_song;
	public Image[] panels;
	public GameObject pivot;
	public Canvas canvas;
	public Camera main;
	public AudioClip slash;
	private AudioSource camSource;

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
		camSource = main.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown ("space")) {
			camSource.clip = slash;
			camSource.loop = true;
			camSource.Play ();
			jump = true;
			GameMananger.instance.PlayJump ();
		}
		if (jump == true) {
			foreach (Image panel in panels) {
				panel.color = new Color (panel.color.r, panel.color.g, panel.color.b, 1f);
			}
			canvas.transform.RotateAround (pivot.transform.position, pivot.transform.forward, Time.deltaTime * 600f);
		} else {
			foreach (Image panel in panels) {
				panel.color = new Color (panel.color.r, panel.color.g, panel.color.b, 0f);
			}
		}
	}

	void OnCollisionEnter(Collision c){
		if (c.gameObject.tag.Equals ("Finish") && !Eggman.play) {
			Eggman.play = true;
			GameMananger.instance.AddEggmanLifes ();
			PlayEggmanSong ();
		}
		if (c.gameObject.CompareTag ("Respawn")) {
			gameObject.transform.parent = null;
			if (jump) {
				jump = false;
				camSource.Stop ();
			}
		}
		if (c.gameObject.tag.Equals ("Finish") && jump) {
			jump = false;
			camSource.Stop ();
			gameObject.transform.parent = null;
		}
		if (c.gameObject.tag.Equals ("Piso") && jump) {
			jump = false;
			camSource.Stop ();
			gameObject.transform.parent = null;
		}
		if (c.gameObject.CompareTag ("Giratorio")) {
			if (jump) {
				jump = false;
				camSource.Stop ();
			}
			gameObject.transform.parent = c.transform;
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
