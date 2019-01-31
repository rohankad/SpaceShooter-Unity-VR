using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag != "Player") {
			print ("Bullet Destroyed : " + other.gameObject.name);
			Destroy (this.gameObject);
		}
	}
}
