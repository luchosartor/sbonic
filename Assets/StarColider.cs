using UnityEngine;
using System.Collections;

public class StarColider : MonoBehaviour {
	
	public GameMananger gm;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.left*200*Time.deltaTime);



	}
	void OnTriggerEnter(Collider c){
		GameMananger.instance.PlaySuccess ();
		Destroy (gameObject);
		gm.updateLabel ();
	}
}
