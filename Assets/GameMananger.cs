using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameMananger : MonoBehaviour { 
	private int totalStartCollected= 0;
	 public Text estrellas;
	public Text vidas;
	private int vidasCount = 3;
	private int eggmanLifes = 3;
	public bool powerUp = false;
	public Text power;
	private AudioSource source;
	public AudioClip success;
	public AudioClip doh;
	public AudioClip jump;
	public AudioClip shield;
	public AudioClip ouch;
	public AudioClip no;
	public AudioClip laugh;
	public bool checkpoint = false;
	public GameObject egg;


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
		source = GetComponent<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (vidasCount == 0) {
			vidas.text = "No hay vidas, perdio";
				Invoke ("Finish", 3);
		}
	



	}
	public void Finish(){
		Debug.Log("fdafasd");
		Application.Quit();
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
		if (!powerUp) {
			source.PlayOneShot (doh, 1f);
			vidasCount = vidasCount - 1;
			vidas.text = "Vidas: " + vidasCount;
		}
	}

	public void PlaySuccess(){
		source.PlayOneShot (success, 0.4f);
	}

	public void PlayShield(){
		source.PlayOneShot (shield, .4f);
	}

	public void PlayJump(){
		source.PlayOneShot (jump, 1f);
	}

	public void PlayLaugh(){
		source.PlayOneShot (laugh, 1f);
	}

	public void PlayNo(){
		source.PlayOneShot (no, .2f);
	}

	public void AddEggmanLifes(){
		estrellas.text = "Eggman: " + eggmanLifes;
		power.enabled = false;
	}

	public void EggmanDecreaseLife(){
		source.PlayOneShot (ouch, 1f);
		eggmanLifes -= 1;
		estrellas.text = "Eggman: " + eggmanLifes;
	}
}
