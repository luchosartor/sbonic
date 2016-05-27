using UnityEngine;
using System.Collections;

public class Eggman : MonoBehaviour
{
	public Light[] leftLights;
	public Light[] rightLights;
	public GameObject eggman;
	public AudioClip turbine;
	public GameObject[] cannons;
	public GameObject bullet;
	public AudioClip shootSound;
	public static bool play = false;

	private Vector3 starting;
	private int currentIndex = 0;
	private float maxSpeed = 1f;
	private float speed = 0f;
	private float accel = 0.01f;
	private bool tackle = false;
	private bool rotate180 = false;
	private float currentRotation = 0f;
	private float rotationSpeed = 1f;
	private float distance = 0;
	private AudioSource source;
	Animator animator;
	private bool fire = false;
	private bool resetPos = false;
	private bool shooting = false;
	private float fireSpeed = .5f;
	private int attacks = 0;
	private bool attacking = false;
	private bool vulnerable = false;
	private int lifes = 3;

	// Use this for initialization
	void Start ()
	{
		InvokeRepeating ("Throttle", 0f, .05f);
		animator = GetComponent<Animator> ();
		starting = eggman.transform.localPosition;
		source = GetComponent<AudioSource> ();
		source.clip = turbine;
		source.loop = true;
		source.Play ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (lifes <= 0 && animator.GetCurrentAnimatorStateInfo(0).IsName("Destroyed")) {
			GameMananger.instance.PlayNo ();
			animator.SetBool ("Dead", true);
			Destroy (eggman, 3f);
			Destroy (this, 3f);
			return;
		}
		if(fire){
			speed = 0.1f;
			if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Right")) {
				if (distance < 2) {
					if (!shooting) {
						InvokeRepeating ("Fire", 0, fireSpeed);
						shooting = true;
					}
					eggman.transform.localPosition += eggman.transform.right * speed;
					distance += speed;
					} else {
					animator.SetBool ("Right", false);
					animator.SetBool ("Left", true);
				}
			} else if (animator.GetCurrentAnimatorStateInfo (0).IsName ("From_Right")) {
				distance = 0;
				shooting = false;
				CancelInvoke ("Fire");
			} else if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Left")) {
				if (distance < 4) {
					if (!shooting) {
						InvokeRepeating ("Fire", 0, fireSpeed);
						shooting = true;
					}
					eggman.transform.localPosition -= eggman.transform.right * speed;
					distance += speed;
				} else {
					animator.SetBool ("Left", false);
					animator.SetBool ("Right", true);
				}
			} else if (animator.GetCurrentAnimatorStateInfo (0).IsName ("From_Left")) {
				distance = 0;
				fire = false;
				resetPos = true;
				shooting = false;
				CancelInvoke ("Fire");
			}
		}

		if(resetPos){
			if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Right")) {
				if (distance < 2) {
					if (!shooting) {
						InvokeRepeating ("Fire", 0, fireSpeed);
						shooting = true;
					}
					eggman.transform.localPosition += eggman.transform.right * speed;
					distance += speed;
				} else {
					animator.SetBool ("Right", false);
					animator.SetBool ("Idle", true);
				}
			}else if (animator.GetCurrentAnimatorStateInfo(0).IsName("From_Right")){
				distance = 0;
				speed = 0;
				resetPos = false;
				shooting = false;
				CancelInvoke ("Fire");
				attacks++;
				attacking = false;
			}
		}

		if (tackle) {
			if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Lean_Forward")) {
				if (distance < 30) {
					eggman.transform.localPosition += eggman.transform.forward * speed;
					//eggman.transform.localPosition = new Vector3 (eggman.transform.localPosition.x, eggman.transform.localPosition.y, eggman.transform.localPosition.z + speed);
					distance += Mathf.Abs(speed);
					if (Mathf.Abs(speed) < maxSpeed) {
						speed += accel;
					}
				} else {
					speed = 0;
					animator.SetBool ("Stop", true);
					animator.SetBool ("Fwd", false);
				}
			} else {
				if (!animator.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {
					animator.SetBool ("Stop", false);
					animator.SetBool ("Right", true);
					tackle = false;
					rotate180 = true;
					distance = 0;
				}
			}
				
		}
		if (rotate180) {
			if (currentRotation < 180f) {
				currentRotation += rotationSpeed;
				eggman.transform.eulerAngles = new Vector3 (eggman.transform.eulerAngles.x, eggman.transform.eulerAngles.y + rotationSpeed, eggman.transform.eulerAngles.z);
			} else {
				attacks++;
				currentRotation = 0f;
				rotate180 = false;
				animator.SetBool ("Right", false);
				animator.SetBool ("Idle", true);
				attacking = false;
			}
		}
		//	Input.GetKeyDown (KeyCode.A)	
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && play) {
			starting = eggman.transform.localPosition;
			int rnd = (int)Random.Range (0, 2f);
			Debug.Log (rnd);
			if (attacks < 2 && !attacking) {
				Debug.Log ("attacks: " + attacks);
				Attack (rnd);
				attacking = true;
			} else if(attacks >= 2){
				if (!vulnerable && !animator.GetCurrentAnimatorStateInfo(0).IsName("Vulnerable")) {
					Debug.Log ("vulnerable");
					animator.SetBool ("Vulnerable", true);
					Invoke ("ResetAttacks", 5f);
					vulnerable = true;
				}
			}
		}
	}

	void ResetAttacks(){
		Debug.Log ("reset");
		attacks = 0;
		vulnerable = false;
		animator.SetBool ("Vulnerable", false);
	}

	void Throttle ()
	{
		for (int i = 0; i < leftLights.Length; i++) {
			Light leftlight = leftLights [i];
			Light rightlight = rightLights [i];
			if (i == currentIndex) {
				leftlight.intensity = 8;
				rightlight.intensity = 8;
			} else {
				leftlight.intensity = 0;
				rightlight.intensity = 0;
			}	
		}
		currentIndex++;
		if (currentIndex == leftLights.Length) {
			currentIndex = 0;
		}
	}

	void Attack (int attackNum)
	{
		switch (attackNum) {
		case 0: 
			animator.SetBool ("Fwd", true);
			animator.SetBool ("Idle", false);
			animator.SetBool ("Stop", false);
			tackle = true;
			break;
		case 1:
			animator.SetBool ("Right", true);
			animator.SetBool ("Idle", false);
			animator.SetBool ("Stop", false);
			fire = true;
			break;
		default: 
			Debug.Log (attackNum);
			break;
		}
	}

	void Fire(){
		for (int i = 0; i < cannons.Length; i++) {
			GameObject obj = Instantiate (bullet, cannons [i].transform.position, Quaternion.identity) as GameObject;   
			obj.transform.eulerAngles = new Vector3(0, 0, 90f);
			obj.GetComponent<Rigidbody> ().AddForce (cannons [i].transform.forward * 800);
			source.PlayOneShot (shootSound, 0.7f);
			Destroy (obj, 3f);
		}
	}

	void OnCollisionEnter(Collision c){
		if (c.gameObject.tag.Equals ("Player")) {
			if (vulnerable) {
				lifes -= 1;
				if (lifes <= 0) {
					animator.SetBool ("Destroyed", true);
				}
				Debug.Log ("lifes = " + lifes);
				GameMananger.instance.EggmanDecreaseLife ();
			} else {
				GameMananger.instance.PlayLaugh ();
				GameMananger.instance.perderVida ();
			}
		}
	}
		
}

