using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;

public class Done_GameController : MonoBehaviour
{
	public GameObject[] hazards;
	public GameObject[] OtherAsteroid;

	public Vector3 spawnValues;
	public Vector3 spawnValuesForOther;
	public int hazardCount;

	public float spawnWait;
	public float startWait;
	public float waveWait;

	public float spawnWaitForOther;

	public socketScript _Socket;

	public bool gameOver;
	private bool restart;
	//private int score;

	private int Health = 100;
	public Text HealthText;

	private int Score;
	public Text ScoreText;

	public GameObject Points;
	public GameObject UI;
	public Text PointsText;
	public GameObject ExitButton;

	private int TotalPoints;
	private int CurrPoints = 0;

	public Slider _PointsSlider;
	public Image _SliderColor;
	public Color[] _colorSlider;

	public Animator _StarAnim;

	public Done_PlayerController DonePlayer;
	//public USpinTemplate _UspinFramework;
	public GameObject _GameOverText;
	public GameObject _GameOverPanel;
	public GameEnd _GameEnd;

	void Start ()
	{
		gameOver = false;
		restart = false;

		ScoreText.text = "0";

		StartCoroutine (SpawnWaves ());
		StartCoroutine (SpawnWavesForOther ());
		//StartCoroutine (GameEnd ());
		//StartCoroutine (ChangeColor ());

		//DonePlayer = GameObject.FindGameObjectWithTag ("Player").GetComponent<Done_PlayerController>();
	}
	
	void Update ()
	{
		if (restart)
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
				Application.LoadLevel (Application.loadedLevel);
			}
		}




	}

	private void GameEnd (){
	//	yield return new WaitForSeconds (30);
		GameOver ();
		ShowUI ();
		//StartCoroutine (ShowUI ());

	}

	private void ShowUI (){
		//yield return new WaitForSeconds (4f);
		ExitButton.SetActive (true);
		UI.gameObject.SetActive (true);

		//Points.gameObject.SetActive (true);
	//	_UspinFramework.EndGame ();

		_GameOverText.SetActive(true);
		_GameOverPanel.SetActive (true);
		_GameEnd.ShowGameEnd ();
		StartCoroutine (ShowPoints ());

	}

	IEnumerator ShowPoints(){
		if (CurrPoints <= TotalPoints) {
			PointsText.text = CurrPoints.ToString();
			yield return new WaitForSeconds (0.1f);
			CurrPoints++;

			StartCoroutine (ShowPoints ());
		} else {
			//_GameOverText.SetActive(true);
			//_GameOverPanel.SetActive (true);
			StopCoroutine ("ShowPoints");
		}

	}

	IEnumerator RestartGame(){
		print ("Restart Game");
		yield return new WaitForSeconds (4f);
		//SceneManager.LoadSceneAsync ("Normal_Game");
		Application.LoadLevel ("Normal_Game");
	}

	IEnumerator SpawnWavesForOther ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				GameObject other = OtherAsteroid [Random.Range (0, OtherAsteroid.Length)];

				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValuesForOther.x, spawnValuesForOther.x), Random.Range (-spawnValuesForOther.y, spawnValuesForOther.y), spawnValuesForOther.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (other, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWaitForOther);
			}
			yield return new WaitForSeconds (waveWait);
			
			if (gameOver)
			{
				restart = true;
				break;
			}
		}
	}

	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
			
			if (gameOver)
			{
				
				restart = true;
				break;
			}
		}
	}
	
	public void AddScore (int newScoreValue)
	{
		//score += newScoreValue;
		//UpdateScore ();
	}

	IEnumerator ChangeColor(int temp){
		float time = 0f;
	while (time<=3f) {
			_SliderColor.color = Color.Lerp (_SliderColor.color, _colorSlider[temp], time/5f);
//			yield return new WaitForEndOfFrame();
			yield return new WaitForSeconds(.2f);
			time+=0.2f;

		}

		_SliderColor.color = Color.Lerp (_SliderColor.color, _colorSlider[temp], 1f);
	}

	public void UpdateScore (int points)
	{
		_StarAnim.SetTrigger ("StarPopOnce");
		Score = Score + points;
		ScoreText.text = Score.ToString ();
		_PointsSlider.value += 0.1f;

		//print("Pointer VAlue : "+_PointsSlider.value);
		if (_PointsSlider.value == 0.6f) {
			//StopCoroutine ("ChangeColor");
			print("Color chnaged to golden ");
			StartCoroutine (ChangeColor(1));
		}
		if (_PointsSlider.value == 0.3f) {
			print("Color chnaged to green ");
			StartCoroutine (ChangeColor(0));
		}

		if (_PointsSlider.value == 1.0f) {
			_SliderColor.color=_colorSlider[2];
			_PointsSlider.value=0.0f;
		}




	}
	
	public void GameOver ()
	{
		print ("Stopping all rouotines");
		StopAllCoroutines ();
		TotalPoints = Score + Health;
		DonePlayer.StopAutoShoot (); //add after attaching network componnet
		print ("GAME OVER!");
		gameOver = true;
	}

	public void UpdateHealth(int ded){
		Health = Health - ded; 
		HealthText.text = Health.ToString ();
		if (Health == 0) {
			GameEnd ();
		}
	}

	public void GameReset(){
		StopAllCoroutines ();

		_GameOverText.SetActive (false);
		_GameOverPanel.SetActive (false);
		Health = 100;
		Score = 0;
		HealthText.text = Health.ToString ();
		ScoreText.text = Score.ToString ();

		gameOver = false;
		restart = false;

		StartCoroutine (SpawnWaves ());
		StartCoroutine (SpawnWavesForOther ());
		DonePlayer.AutoShoot ();
	}
}