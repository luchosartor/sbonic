using UnityEngine;
using System.Collections;

public class powerUp : MonoBehaviour {
	public GameObject obj;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	
	}
	void OnCollisionEnter(Collision c){
		Debug.Log ("fdsfads");
		if (c.gameObject.CompareTag ("Player")) {
			GameMananger.instance.PlayShield ();
			GameMananger.instance.powerUp = true;
			GameMananger.instance.power.text = "Power Up : SI";

			GetComponent<Collider> ().enabled = false;
			GetComponent<MeshRenderer> ().enabled = false;
			Invoke ("FinishPU", 10);


		}
	}
	void Finish(){
	}
	void FinishPU(){
		Debug.Log("fdsafdasfasd");
		GameMananger.instance.powerUp = false;
		GameMananger.instance.power.text = "Power Up : NO";

	}
}
