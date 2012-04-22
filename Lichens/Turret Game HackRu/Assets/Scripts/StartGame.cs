using UnityEngine;
using System.Collections;
using System.IO.Ports;
using LitJson;

public class StartGame : MonoBehaviour {
	string[] ports; 
	SerialPort stream;
	int count = 0;
	int count2 = 0;
	JsonData inputFromArduino;
	int lastAccelY=0;
	int lastPot2=0;
	bool nuke = false;
	GameObject nukeLight;
	int countNukes = 0;
	int nukelightcount = 0;
	void Start()
	{
		ports = SerialPort.GetPortNames();
		stream = new SerialPort(ports[0], 57600);
		//stream = new SerialPort(/*ports[0]*/"COM13", 57600);
		stream.Open();
		stream.Write(" ");
	}
	
	void Update()
	{
		string val = stream.ReadLine ();
		inputFromArduino = JsonMapper.ToObject(val);
		if((int)inputFromArduino["but1"]==1)
		{
			Application.LoadLevel ("MainScene");
		}
	}
	
	// Update is called once per frame
	void OnMouseDown()
		{
			Application.LoadLevel ("MainScene");
		}
	}
