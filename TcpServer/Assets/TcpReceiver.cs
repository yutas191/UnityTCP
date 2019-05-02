using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class TcpReceiver : MonoBehaviour {
	private TcpListener server;
	private byte[] msg = new byte[256];
	private int i;
	private string text = null;

	private Thread thread;

	// Use this for initialization
	void Start () {
		thread = new Thread (ThreadReceive);
		thread.Start ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void ThreadReceive()
	{
		Debug.Log("START");

		string ipString = "127.0.0.1";
		System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse(ipString);
		int port = 10021;

		server = new TcpListener(ipAdd, port);

		server.Start();
		Debug.Log("Listenを開始しました "+
			((System.Net.IPEndPoint)server.LocalEndpoint).Address+":"+
			((System.Net.IPEndPoint)server.LocalEndpoint).Port);
		
		while (true) {
			TcpClient client = server.AcceptTcpClient ();
			NetworkStream stream = client.GetStream ();
			while ((i = stream.Read (msg, 0, msg.Length)) != 0) {
				text = System.Text.Encoding.ASCII.GetString (msg, 0, i);
			}
			Debug.Log ("RECEIVE:" + text);
			client.Close ();
		}
	}
}
