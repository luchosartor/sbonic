using UnityEngine;
using System.Collections;

public class girar : MonoBehaviour {
	//public GameObject piso;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround (transform.position, transform.up, Time.deltaTime * 20f);
		//GetComponent<Rigidbody>().AddTorque(new Vector3(0f,Time.deltaTime * 50f,0f));
	}
	void OnCollisionEnter(Collision c){
		if (c.gameObject.tag.Equals ("Player")) {
			c.transform.parent = transform;
		}
	}
	void OnCollisionExit(Collision c){
		if (c.gameObject.tag.Equals ("Player")) {
			c.transform.parent = null;
		}
		
	}
}
