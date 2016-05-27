using UnityEngine;
using System.Collections;

public class checkpoint : MonoBehaviour {
	public GameObject piso;
	public GameMananger gm;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision c){
		if (c.gameObject.CompareTag ("Player")) {
			gm.checkpoint = true;
		}
	}
}
