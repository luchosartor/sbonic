using UnityEngine;
using System.Collections;

public class trampa2 : MonoBehaviour {
	public GameObject bloque;
	public Vector3 positionBloque;
	float moveZ = 0.07f;
	bool stop =false;

	// Use this for initialization
	void Start () {
		positionBloque = bloque.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (bloque.transform.position.z < positionBloque.z + 10 && !stop) {
			bloque.transform.position = new Vector3 (bloque.transform.position.x, bloque.transform.position.y, bloque.transform.position.z + moveZ);		
		}
		if (bloque.transform.position.z > positionBloque.z + 10) {
			stop = true;
		}
		if (stop) {
			bloque.transform.position = new Vector3 (bloque.transform.position.x, bloque.transform.position.y, bloque.transform.position.z - moveZ);
		}
		if (bloque.transform.position.z < positionBloque.z) {
			stop = false;
		}
		
	
	}
	void OnCollisionEnter(Collision c){
		if (c.gameObject.CompareTag ("Player")) {
			c.gameObject.transform.parent = bloque.transform;
		}
	}
	void OnCollisionExit(Collision c){
		if (c.gameObject.CompareTag ("Player")) {
			c.gameObject.transform.parent = null;
		}
	}
}