using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class TcpSender : MonoBehaviour {

	private TcpClient client = null;
	private NetworkStream Stream;
	private string ipString = "127.0.0.1";
	private int port = 10021;
	private int count = 0;
	private string text;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			text = "Hello:" + (count++);
			Debug.Log ("SEND:"+text);
			client = new TcpClient(ipString, port);
			Stream = client.GetStream ();
			byte[] msg = System.Text.Encoding.ASCII.GetBytes (text);
			Stream.Write (msg, 0, msg.Length);
			Stream.Flush ();
			Stream.Close ();
			client.Close ();
		}
	}
}
