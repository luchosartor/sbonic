using UnityEngine;
using System.Collections;

public class EnemyDie : MonoBehaviour
{
	Animator animator;
	private AudioSource source;
	public AudioClip metal_crash;
	// Use this for initialization
	void Start ()
	{
		animator = GetComponent<Animator> ();
		source = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
//		if (Input.GetMouseButtonDown (0)) {
//			source.Stop ();
//			source.PlayOneShot (metal_crash, 1f);
//			animator.SetBool ("isAlive", false);
//		}
	}

	void OnCollisionEnter (Collision c)
	{
		if (c.gameObject.tag == "Player" && animator.GetBool("isAlive") && Player.instance.jump) {
			source.Stop ();
			source.PlayOneShot (metal_crash, 1f);
			animator.SetBool ("isAlive", false);	
		}
	}
}
