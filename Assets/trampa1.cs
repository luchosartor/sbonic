using UnityEngine;
using System.Collections;

public class trampa1 : MonoBehaviour {
	public GameObject trampa;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision c){
		Invoke ("DestroyObject", 3);
	}
	void DestroyObject(){
		Destroy (trampa);
	}
}
