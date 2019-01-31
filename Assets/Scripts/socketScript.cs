using UnityEngine;

using System.Collections;

using System.Collections.Generic;

//using CielaSpike;

using System.Text;

using System;
using UnityEngine.UI;



public class socketScript : MonoBehaviour {

	

	//variables

	private TCPConnection myTCP;

	private string serverMsg;

	public string msgToServer;

	//public UIHandler _UIHandler;
	//Temp
	string _str = "Game2-Video3";

	public Text _ConnStatus;

	void Awake() {

		//add a copy of TCPConnection to this game object

		myTCP = gameObject.AddComponent<TCPConnection>();

	}



	void Start () {
	//	StartCoroutine ("StartConnection");
		
	}

	IEnumerator StartConnection(){
		_ConnStatus.text="Connecting.....";
		if (myTCP.socketReady == false) {
			Debug.Log("Attempting to connect..");

			myTCP.setupSocket();
		}

		yield return new WaitForSeconds (1f);

		if (myTCP.socketReady == true) {
			_ConnStatus.text="Connected";
		}


	}


	void Update () {

		

		//keep checking the server for messages, if a message is received from server, it gets logged in the Debug console (see function below)

		SocketResponse ();

		if (Input.GetKeyDown (KeyCode.Escape)) {
			print ("Game Exit");
			Application.Quit ();
		}

		if (Input.GetKeyUp (KeyCode.A)) {
			SendToServer("TrueStory");
		}
	}



	void OnGUI() {
		/*
		//if connection has not been made, display button to connect
		if (myTCP.socketReady == false) {
			if (GUILayout.Button ("Connect")) {
				//try to connect
				Debug.Log("Attempting to connect..");
				myTCP.setupSocket();
			}
		}
		//once connection has been made, display editable text field with a button to send that string to the server (see function below)
		if (myTCP.socketReady == true) {
			msgToServer = GUILayout.TextField(msgToServer);
			if (GUILayout.Button ("Write to server", GUILayout.Height(30))) {
					SendToServer(msgToServer);
			}
}
		*/
	}
		

	//socket reading script

	void SocketResponse() {

		string serverSays = myTCP.readSocket();
		serverSays.Trim ();

		if (serverSays == "") {

		} else {
			Debug.Log ("[SERVER]" + serverSays);
		}


		}







	//send message to the server

	public void SendToServer(string str) {

		myTCP.writeSocket(str);

		//Debug.Log ("[CLIENT] -> " + str);

	}



}

