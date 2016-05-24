using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	public GameObject bullet;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision c){
		if(c.gameObject.tag.Equals("Piso") && !GameMananger.instance.powerUp){
			Destroy (bullet);
		}else if (c.gameObject.tag.Equals("Player")){
			GameMananger.instance.perderVida ();
		}


	}
}
