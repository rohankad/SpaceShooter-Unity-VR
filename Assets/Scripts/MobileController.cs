using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

[System.Serializable]
public class Done_Boundary_Mobile 
{
	public float xMin, xMax, zMin, zMax;
}

public class MobileController : MonoBehaviour {

	public socketScript _socketScript;

	public Text _AccStatus;
	public int speed;
	public Done_Boundary_Mobile boundary;
	public float tilt;
	public Vector3 movement;

	public Done_PlayerController _PlayerController;
	public Transform _Cube;

	// Use this for initialization
	void Start () {
	//	_AccStatus = GameObject.Find ("AccelerometerMovemnt").GetComponent<Text>();
	}


	// Update is called once per frame
	void Update () {
		//if (Input.GetKeyUp (KeyCode.Q)) {
			//if (!isLocalPlayer) {
				//print("Acclerometer Movement : "+movement);
			//}
		//}
	

		float x = Input.GetAxis ("Horizontal") * Time.deltaTime * 150.0f;
		float z = Input.GetAxis ("Vertical") * Time.deltaTime * 3.0f;
		transform.Rotate (0, x, 0);
		transform.Translate (0, 0, z);

	
		//_Controller ();
	} 



	void _Controller(){
		
		#if UNITY_EDITOR
		
		//#if UNITY_EDITOR
		//Debug.Log("Unity Editor");
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

		#else
		
		movement = new Vector3(Input.acceleration.x, 0.0f, -Input.acceleration.z);
		//_PlayerController.SetMovement(movement);
		#endif
		
		
		print ("Movement : "+movement);
		
		_AccStatus.text = movement.ToString();
		//_socketScript.SendToServer (movement.ToString ());
		
		
		GetComponent<Rigidbody> ().velocity = movement * speed;
		
		GetComponent<Rigidbody> ().position = new Vector3
			(
				Mathf.Clamp (GetComponent<Rigidbody> ().position.x, boundary.xMin, boundary.xMax),
				0.0f,
				Mathf.Clamp (GetComponent<Rigidbody> ().position.z, boundary.zMin, boundary.zMax)
				);
		
		GetComponent<Rigidbody> ().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody> ().velocity.x * -tilt);
		
	}
}
