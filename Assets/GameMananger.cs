using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameMananger : MonoBehaviour { 
	private int totalStartCollected= 0;
	 public Text estrellas;
	public Text vidas;
	private int vidasCount = 3;
	public bool powerUp = false;
	public Text power;

	public static GameMananger instance;

	void Awake(){
		if (!instance) {
			instance = this;
		} else {
			Destroy (this);
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	
	}
	public void updateLabel(){
		totalStartCollected++;
		estrellas.text = "Estrellas: " +totalStartCollected;
		if (totalStartCollected == 3) {
			vidasCount = vidasCount + 1;
			vidas.text = "Vidas: " + vidasCount;
			totalStartCollected = 0;
			estrellas.text = "Estrellas: " +totalStartCollected;
		}
	}
	public void perderVida(){
		vidasCount = vidasCount - 1;
		vidas.text = "Vidas: " + vidasCount;

	}
}
