using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Networking;


[System.Serializable]
public class Done_Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class Done_PlayerController : MonoBehaviour
{
	public float speed;
	public float tilt;
	public Done_Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	 
	private float nextFire;
	private Quaternion calibrationQuaternion;
	public Done_GameController _GameController;
	public bool _AutoShoot;

	public float _ShipXAxis;
	public float moveHorizontal=0f;
	public float moveVertical=0f;
	public bool _RevertHoriz;

	//public Vector3 movement;

	float lerpTime = 1f;
	float currentLerpTime;
	public bool _GameReady;

	public GameObject _RotateAroundPlanet;

	public MobileController _MobileController;
	public GameObject _Cube;

	public bool _IsAndroidApp;

	public Vector3 _oppPos;
	public Quaternion _oppRot;

	public Vector3 _StartPos;
	public Quaternion _StartRot;
	//public Rigidbody _PLayerRigidBody;
	void Start(){
		_AutoShoot = true;

		_RevertHoriz = false;
		//_GameReady = false;


			if (_IsAndroidApp) {
				print ("_IsAndroidApp is true");
				_GameReady = true;

			}

	}

	public void GameReadyToPlay(){
		_GameReady = true;

	}


	public void AutoShootTrigger(){

		_AutoShoot = !_AutoShoot;
		if (_AutoShoot) {
			AutoShoot ();
		} else {
			StopAutoShoot ();
		}
	}
		


	public void AutoShoot(){
		
		StartCoroutine ("Shoot");
	}

	public void StopAutoShoot(){
		print ("Stop auto shoot");
		StopCoroutine ("Shoot");
	}


	IEnumerator Shoot(){
		if (Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource>().Play ();
		//	print ("Shooting.......");
		}

		yield return new WaitForSeconds (0.5f);
		StartCoroutine ("Shoot");
	}

	private void _Reset(){
	}



	void Update ()
	{

		#if UNITY_STANDALONE_WIN
		if(Input.GetMouseButtonUp(0)){
			StopCoroutine("MoveSmoothly");
			StartCoroutine("MoveSmoothly");
			//_Reset();
		}

		if(Input.GetMouseButton(0)){
			 _ShipXAxis=  (Input.mousePosition.x - Screen.width/2f)/(Screen.width/2f);
			moveHorizontal = _ShipXAxis;
			moveVertical = Input.GetAxis("Vertical");

		}
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
			
		#elif UNITY_EDITOR

		//#if UNITY_EDITOR
		//Debug.Log("Unity Editor");
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		//print ("Executing Editor part");
		#else

		Vector3 movement  = new Vector3(Input.acceleration.x, 0.0f, -Input.acceleration.z);

		#endif

		if (_GameReady) {
			GetComponent<Rigidbody> ().velocity = movement * speed;
			GetComponent<Rigidbody> ().position = new Vector3
			(
				Mathf.Clamp (GetComponent<Rigidbody> ().position.x, boundary.xMin, boundary.xMax),
				0.0f,
				Mathf.Clamp (GetComponent<Rigidbody> ().position.z, boundary.zMin, boundary.zMax)
			);

			GetComponent<Rigidbody> ().rotation = Quaternion.Euler (0.0f, 0f, GetComponent<Rigidbody> ().velocity.x * -tilt);

			//GetComponent<Rigidbody> ().rotation = Quaternion.Euler ( GetComponent<Rigidbody> ().velocity.x * -tilt,0.0f, 0.0f);
			//float t =  GetComponent<Rigidbody> ().velocity.x * -tilt;
			//GetComponent<Rigidbody> ().rotation.z = t;
		}
	}

	IEnumerator MoveSmoothly()
	{
		float time = 0f;
		while(time<=0.5f)
		{
			moveHorizontal=Mathf.Lerp(moveHorizontal,0,time*2f);
			yield return new WaitForEndOfFrame();
			time+=Time.deltaTime;
		}
		moveHorizontal = 0;
	
	}



}
