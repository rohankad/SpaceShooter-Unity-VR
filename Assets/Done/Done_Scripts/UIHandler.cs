using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UIHandler : MonoBehaviour {

	public GameObject GameController;
	public GameObject DonePlayer;

	public Text TimeText;
	private int StringCounter;
	public GameObject UI;
	public GameObject GameReady;
	public GameObject _BeginButton;

	//public Text _AccX, _AccZ;
	//private float x, z;

	//public Text CameraPos;
	//public GameObject Cam;

	private string[] _time = { "5", "4", "3", "Get Ready", "Go" };
	// Use this for initialization

	void Start () {

		StringCounter = 0;
		//BeginGame ();
	}
	

	public void ApplicationExit(){
		Application.Quit ();
	}

	public void BeginGame(){
		Debug.Log("wowowo called Begin game :");
		//DonePlayer = GameObject.FindGameObjectWithTag ("Player");
		_BeginButton.SetActive (false);
		GameReady.SetActive (true);
		StartCoroutine ("StartTimer");

		//GameController.gameObject.SetActive (true);
		//DonePlayer.gameObject.SetActive (true);
	}
	IEnumerator StartTimer(){
		if (StringCounter == 5) {
			UI.SetActive (false);
			//StopCoroutine ("StartTimer");
			GameController.gameObject.SetActive (true);

			//DonePlayer.gameObject.SetActive (true);
			//DonePlayer.GetComponent<Done_PlayerController> ().AutoShoot ();
			//DonePlayer.GetComponent<Done_PlayerController> ().GameReadyToPlay();
			GameReady.SetActive (false);
			print ("Game started");

			//DonePlayer.SetActive(false);
		}
		if (StringCounter < 5) {
			TimeText.text = _time[StringCounter];
			yield return new WaitForSeconds (1);
			StringCounter++;
			StartCoroutine ("StartTimer");
		}

	}

}
