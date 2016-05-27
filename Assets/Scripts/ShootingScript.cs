using UnityEngine;
using System.Collections;

public class ShootingScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision c){
		if (c.gameObject.tag.Equals ("Player")) {
			GameMananger.instance.perderVida ();
			GameMananger.instance.PlayLaugh ();
		} else if (c.gameObject.tag.Equals ("Finish")) {
			Destroy (gameObject);
		}
	}
}
