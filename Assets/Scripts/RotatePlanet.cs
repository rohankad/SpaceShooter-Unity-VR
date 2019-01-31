using UnityEngine;
using System.Collections;

public class RotatePlanet : MonoBehaviour {

	public float _Speed;
	// Use this for initialization
	void Start () {
		//_Speed = 5.0f;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.left *_Speed* Time.deltaTime);
	}


}
