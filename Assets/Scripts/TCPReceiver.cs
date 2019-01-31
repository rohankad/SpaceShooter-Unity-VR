using UnityEngine;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine.UI;
using System;



public class TCPReceiver : MonoBehaviour {
	Socket ss;
	public string _ip;
	public int _port;

	string test;
	
	private volatile bool mRunning;
	public static string msg = "";
	
	public Thread mThread;
	public TcpListener tcp_Listener = null;

	//

	//the name of the connection, not required but better for overview if you have more than 1 connections running
	public string conName = "Localhost";
	
	//ip/address of the server, 127.0.0.1 is for your own computer
	// public string conHost = "127.0.0.1"; public int conPort = 27015;
	public string conHost = "127.0.0.1"; //"10.2.108.158";
	//public string conHost = "10.2.142.149";
	
	
	//port for the server, make sure to unblock this in your router firewall if you want to allow external connections
	public int conPort = 8001;
	
	//a true/false variable for connection status
	public bool socketReady = false;
	
	TcpClient mySocket;
	NetworkStream theStream;
	StreamWriter theWriter;
	StreamReader theReader;
	
	void Awake() {
		/*mRunning = true;
		ThreadStart ts = new ThreadStart (Receive);
		mThread = new Thread (ts);
		mThread.Start ();
		print ("Thread done..."); */
	}
	
	void Start(){
		try {
			mySocket = new TcpClient(conHost, conPort);
			theStream = mySocket.GetStream();
			theWriter = new StreamWriter(theStream);
			theReader = new StreamReader(theStream);
			socketReady = true;
		}
		catch (Exception e) {
			Debug.Log("Socket error:" + e);
		}
		
	}
	void Update () {
		
		if (Input.GetKeyUp (KeyCode.A)) {
			byte[] bytes = System.Convert.FromBase64String("TrueStory");
			ss.Send(bytes);
		}
	}
	

	
	public void stopListening() {
		mRunning = false;
	}
	
	void Receive() {
		tcp_Listener = new TcpListener(IPAddress.Parse(_ip),_port);
		tcp_Listener.Start();
		print("Server Start");

		while (mRunning)
		{
			// check if new connections are pending, if not, be nice and sleep 100ms
			if (!tcp_Listener.Pending()){
				print ("Sleeping..");
				Thread.Sleep(100);
			}
			else {
				 ss = tcp_Listener.AcceptSocket();
				byte[] tempbuffer = new byte[10000];
				
				while(ss!=null){
					ss.Receive(tempbuffer); // received byte array from client
					test =  System.Text.Encoding.Default.GetString(tempbuffer);
					print("Received : "+test);
					
					//ss.Send(tempbuffer);
					//print("Tempbuffer length "+tempbuffer.Length);
				}
				
				//print (" mRunning : "+mRunning);
				//_Log.text+=test+"\n";
			}
			
		}
		
	}

	
	public void StopServer(){
		mThread.Abort ();
		mRunning = false;
		tcp_Listener.Stop ();
		mThread.Join(500);
	}
	
	void OnApplicationQuit() { // stop listening thread
		if (tcp_Listener != null) {
			tcp_Listener.Stop ();
		}
		stopListening();// wait for listening thread to terminate (max. 500ms)
		mThread.Join(500);
	}
}
