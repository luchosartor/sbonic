using UnityEngine;
using System.Collections;



public class volverAlPrincipio : MonoBehaviour {
	public GameObject fps;
	public GameObject cilinder;
	 GameObject currentCilinder;
	public GameMananger gm;
	// Use this for initialization
	void Start () {
		currentCilinder = (GameObject)GameObject.Instantiate(cilinder,cilinder.transform.position,cilinder.transform.rotation);

		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision c){
		if (c.gameObject.tag.Equals ("Player")) {
			fps.transform.position = new Vector3 (97.58f, 7.11f, 5.2f);
			Destroy (currentCilinder);
			currentCilinder = (GameObject)GameObject.Instantiate (cilinder, cilinder.transform.position, cilinder.transform.rotation);
			gm.powerUp = false;
			gm.perderVida ();
		}

	}
}
