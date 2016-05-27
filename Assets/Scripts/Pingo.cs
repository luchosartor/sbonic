using UnityEngine;
using System.Collections;

public class Pingo : MonoBehaviour {
	Animator animator;
	public GameObject pingo;
	public AudioClip walk;
	public AudioClip scream;

	private Vector3 startingPos;
	private float moveX = 0.074f;
	private bool stop = false;
	private float currentRotation;
	private float rotationSpeed = 1f;
	private AudioSource source;
	private float distance = 0f;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		startingPos = pingo.transform.position;
		currentRotation = 0f;
		source = GetComponent<AudioSource> ();
		source.clip = walk;
		source.loop = true;
		source.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (animator.GetBool ("isAlive")) {
			if (!stop) {
				pingo.transform.position += pingo.transform.forward * moveX;
				distance += moveX;
			}
			if (distance > 8) {
				stop = true;
				animator.SetBool ("stop", stop);
				bool s = animator.GetCurrentAnimatorStateInfo (0).IsName ("Left");
				bool shouldScream = animator.GetCurrentAnimatorStateInfo (0).IsName ("Attack");
				if (s) {
					pingo.transform.eulerAngles = new Vector3 (pingo.transform.eulerAngles.x, pingo.transform.eulerAngles.y + rotationSpeed, pingo.transform.eulerAngles.z);	
					currentRotation += rotationSpeed;
					if (currentRotation >= 180f) {
						currentRotation = 0f;
						stop = false;
						animator.SetBool ("stop", stop);		
						distance = 0;
					}
				}
				if (shouldScream) {
					if (source.clip == walk) {
						source.Stop ();
						source.clip = scream;
						source.Play ();
					}
				} else {
					if (source.clip != walk) {
						source.Stop ();
						source.clip = walk;
						source.loop = true;
						source.Play ();
					}
				}
			}
		} else {
			Destroy (pingo, 1f);
			Destroy (this, 1f);
		}
	}
	void OnCollisionEnter(Collision c){
		if (c.gameObject.tag.Equals ("Player") &&!GameMananger.instance.powerUp && !Player.instance.jump) {
			GameMananger.instance.perderVida ();
		}
	}
}