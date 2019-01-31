using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEnd : MonoBehaviour {
	

	public Done_GameController _DoneGameController;
	public Text _AutoText;

	int  time;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetMouseButtonUp (0)) {
			print ("mouse button pressed");
			//SceneManager.LoadSceneAsync ("VR_Release");
			_DoneGameController.GameReset();
		}
	}

	public void ShowGameEnd(){
		StartCoroutine ("RestartGame");
		time = 6;
	}

	IEnumerator RestartGame(){
		time = time - 1;
		yield return new WaitForSeconds (1f);
		_AutoText.text="GAME WILL START IN "+time +" SECONDS";
		if (time == 0) {
			_DoneGameController.GameReset();
		
		} else {
			StartCoroutine ("RestartGame");
		}

		//_DoneGameController.GameReset();
	}
}
