using UnityEngine;
using System.Collections;
public class TwilioController : MonoBehaviour {
	// Use this for initialization
	void Start () {
		StartCoroutine("RequestForUpdates");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	IEnumerator RequestForUpdates()
	{
		WWWForm wwwForm = new WWWForm();
		wwwForm.AddField("name", "value");
		System.Collections.Hashtable headers = wwwForm.headers;
		headers["Authorization"] = "Basic" + System.Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes("AC684e72900fd44b85adefb5091a8ee21a:a0a81dfc258992429b072f94e71f1f69"));
		WWW www = new WWW("https://api.twilio.com/2010-04-01/Accounts/AC684e72900fd44b85adefb5091a8ee21a/SMS/Messages.json",new byte[1], headers);
		yield return www;
		Debug.Log (www.text);
	}
}
