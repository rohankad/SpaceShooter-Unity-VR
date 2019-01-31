using UnityEngine;
using System.Collections;

public class Done_DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private Done_GameController gameController;


	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <Done_GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
		StartCoroutine ("SelfDestroy");
	}

	private IEnumerator SelfDestroy(){
	
		yield return new WaitForSeconds (6);
		Destroy (this.gameObject);
//		print ("Self destory");
	}

	void OnTriggerEnter (Collider other)
	{
	//	print ("Other : "+other.name);
		if (other.tag == "Boundary" || other.tag == "Enemy")
		{
			return;
		}

		if (explosion != null)
		{
			Instantiate(explosion, transform.position, transform.rotation);
		}

		if (other.tag == "Player")
		{
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);

		/*	if (this.gameObject.name == "Done_Enemy Ship(Clone)") {
				//print ("Update Score : ");
				gameController.UpdateScore (10);
			} */


		/*	if (this.gameObject.name == "Done_Enemy Ship(Clone)" ||
				this.gameObject.name == "Done_Asteroid 01(Clone)" || 
				this.gameObject.name == "Done_Asteroid 02(Clone)" || 
				this.gameObject.name == "Done_Asteroid 03(Clone)") {
	
				gameController.UpdateHealth (5);
			} */

			if (this.gameObject.tag == "Enemy") {

				gameController.UpdateHealth (50);
			}

		} 

		if (other.tag == "Bullet") {
			gameController.UpdateScore (5);
		}


		
		//gameController.AddScore(scoreValue);
		//Destroy (other.gameObject);
		Destroy (gameObject);
//		print (" destory by collider");
	}
}