using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision c){
		if (c.gameObject.tag.Equals ("Finish")) {
			Eggman.play = true;
		}
	}
}
