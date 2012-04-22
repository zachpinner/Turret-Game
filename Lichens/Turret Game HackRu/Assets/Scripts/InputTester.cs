using UnityEngine;
using System.Collections;
using System.IO.Ports;
using LitJson;
public class InputTester : MonoBehaviour {
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
	// Use this for initialization
	void Start () {
		ports = SerialPort.GetPortNames();
		stream = new SerialPort(ports[0], 57600);
		
		//stream = new SerialPort(/*ports[0]*/"COM13", 57600);
		
		stream.Open();
		stream.Write(" ");
	}
	
	// Update is called once per frame
	void Update () {
		string val = stream.ReadLine ();
		inputFromArduino = JsonMapper.ToObject(val);
		int currentValueOfPot = (int)inputFromArduino["pot1"];
		GameObject.Find ("turret").GetComponent<TurretMovement>().moveTurret(currentValueOfPot);
		int diff = (int)inputFromArduino["accelYval"]-lastAccelY;
		if(diff>10 || diff<-10)
		{
			lastAccelY=(int)inputFromArduino["accelYval"];
			if(count2>600)
			{
				foreach(GameObject currentEnemy in GameObject.FindGameObjectsWithTag("enemy"))
				{
					Destroy(currentEnemy);
				}
				count2=0;
			}
		}
		if((int)inputFromArduino["but1"]==1 && count>=10)
		{
			GameObject.Find("turret").GetComponent<TurretMovement>().shootBullet();
			count=0;
		}
		int diff2 = (int)inputFromArduino["pot2"]-lastPot2;
		if(diff2>10 || diff2<-10)
		{
			lastPot2=(int)inputFromArduino["pot2"];
			Debug.Log (inputFromArduino["pot2"]);
			GameObject.Find ("Main Camera").SendMessage("SlerpCamera", (float)(int)inputFromArduino["pot2"]);
		}
		
		
		
		/* Make it look through an array to change transforms
		 * 
		if(Input.GetKeyDown(KeyCode.A))
		{
			GameObject.Find ("Main Camera").Transform.position();
		}
		if(Input.GetKeyDown(KeyCode.D))
		{
			GameObject.Find ("Main Camera").Transform.position();
		}
		*/
		
		
		
		count2++;
		count++;
	}
	void OnGUI()
	{
		if(count2>600)
		{
			GUI.Box (new Rect(0,60,100,30), "Nuke Armed");
		}
		else
		{
			GUI.Box(new Rect(0,60,100,30), "Arming");
		}
	}
}
