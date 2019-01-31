using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


public class NetManager : NetworkManager  {
	
	float btnX, btnY, btnW, btnH;
	public string _gameName;
	// Use this for initialization
	void Start () {
		_gameName = "SG_Test";
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnPlayerConnected(NetworkPlayer _player){
		print ("Player Connected : "+_player.ipAddress);
	}
	
	
	/*
	 * void OnGUI(){
		//GUI.Button (Rect (btnX, btnY, btnW,btnH),"Start Server");

		if (GUI.Button (new Rect (10, 10, 120, 40), "Start Server")) {
			print ("You clicked server!");

		}

		if (GUI.Button(new Rect(10, 60, 120, 40), "Refresh Hosts"))
			print("Refreshing!");
		StartCoroutine("refreshHostList");

	}
	*/
}
