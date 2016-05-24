using UnityEngine;
using System.Collections;

public class Drone : MonoBehaviour {
	
	public GameObject bullet;
	public GameObject mouth;
	public GameObject drone;
	public GameObject bladeR;
	public GameObject bladeL;
	public GameObject pivotL;
	public GameObject pivotR;
	public AudioClip bladeSound;
	public AudioClip shootSound;

	private AudioSource source;
	private float volLowRange = .5f;
	private float volHighRange = 1f;

	private Vector3 startingPos;
	Animator animator;
	private float moveZ = 0.1f;
	private float rotationSpeed = 500f;
	private float distance = 0;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Fire", 0, 1.5f);
		startingPos = drone.transform.position;
		animator = GetComponent<Animator> ();
		source = GetComponent<AudioSource> ();
		source.loop = true;
		source.clip = bladeSound;
		source.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		bladeL.transform.RotateAround (pivotL.transform.position, bladeL.transform.up, Time.deltaTime * rotationSpeed);
		bladeR.transform.RotateAround (pivotR.transform.position, bladeR.transform.up, Time.deltaTime * rotationSpeed);
		if (animator.GetBool ("isAlive")) {
			drone.transform.localPosition += drone.transform.forward * moveZ;
			animator.SetFloat ("distanceToStart", Mathf.Abs (drone.transform.position.z - startingPos.z));
			if (distance >= 10) {
				moveZ *= -1;
				distance = 0;
			}
			distance += Mathf.Abs(moveZ);
		} else {
			drone.transform.position = new Vector3 (drone.transform.position.x, drone.transform.position.y - 0.1f, drone.transform.position.z);
			Destroy (drone, 2);
			Destroy (this, 2);
		}
	}

	void Fire () {
		GameObject obj = Instantiate(bullet, mouth.transform.position, Quaternion.identity) as GameObject;   
		obj.GetComponent<Rigidbody> ().AddForce (mouth.transform.forward * 500);
		source.PlayOneShot (shootSound, 0.7f);
		//destroy object when hits the ground.
	}
}