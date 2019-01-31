using UnityEngine;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine.UI;


public class TCPListener : MonoBehaviour {
	public Text _IPAddr;
	public Text _PortAddr;
	public Toggle _Default;



	public string _ip;
	public int _port;
	public Text _Log;
	string test;

	private volatile bool mRunning;
	public static string msg = "";
	
	public Thread mThread;
	public TcpListener tcp_Listener = null;
	
	void Awake() {

	}

	void Start(){


	}

	public void StartClicked(){
		if (_Default.isOn) {
			mRunning = true;
			ThreadStart ts = new ThreadStart (Receive);
			mThread = new Thread (ts);
			mThread.Start ();
			print ("Thread done...");
		} else {
			_ip = _IPAddr.text;
			_port = int.Parse(_PortAddr.text);
			mRunning = true;
			ThreadStart ts = new ThreadStart (Receive);
			mThread = new Thread (ts);
			mThread.Start ();
			print ("Thread done...");
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
				Socket ss = tcp_Listener.AcceptSocket();
				byte[] tempbuffer = new byte[10000];

				while(ss!=null){
				ss.Receive(tempbuffer); // received byte array from client
				test =  System.Text.Encoding.Default.GetString(tempbuffer);
				print("Received : "+test);
				
			   ss.Send(tempbuffer);
					print("Tempbuffer length "+tempbuffer.Length);
				}

				print (" mRunning : "+mRunning);
				//_Log.text+=test+"\n";
			}

		}

	}

	//void OnGUI() {
		//GUI.Label (new Rect (10, 150, 100, 20), "Receiving : "+test);

	//}

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
