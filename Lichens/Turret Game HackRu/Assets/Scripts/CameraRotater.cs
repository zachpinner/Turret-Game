using UnityEngine;
using System.Collections;

public class CameraRotater : MonoBehaviour {
	public Transform[] trackTransform;
	private float[] trackLengths;
	public Transform gem;
	public Transform lookAtMe;
	// Use this for initialization
	void Start () {
		trackLengths= new float[trackTransform.Length];
		float totalDistanceTraveled=0f;
		for(int i =0; i<trackTransform.Length-1; i++)
		{
			float distanceOfThisPoint=(trackTransform[i+1].position-trackTransform[i].position).magnitude;
			if(distanceOfThisPoint<0)
			{
				distanceOfThisPoint=-distanceOfThisPoint;
			}
			totalDistanceTraveled+=distanceOfThisPoint;
		}
		float distanceSoFar=0f;
		for(int i =0; i<trackTransform.Length-1; i++)
		{
			float distanceOfThisPoint=(trackTransform[i+1].position-trackTransform[i].position).magnitude;
			if(distanceOfThisPoint<0)
			{
				distanceOfThisPoint=-distanceOfThisPoint;
			}
			trackLengths[i]=(int)(distanceSoFar/totalDistanceTraveled*1023f);
			distanceSoFar+=distanceOfThisPoint;
		}
		trackLengths[trackLengths.Length-1]=1023;
		foreach(int current in trackLengths)
		{
			Debug.Log (current);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void SlerpCamera(float strength)
	{
		Debug.Log("slerping");
		for(int i=0; i<trackTransform.Length; i++)
		{
			if(strength<trackLengths[i])
			{
				Vector3 frontPosition = trackTransform[i].position;
				Vector3 vectorOfSide = trackTransform[i].position-trackTransform[i-1].position;
				float ratioOfSide = (trackLengths[i]-strength)/(trackLengths[i]-trackLengths[i-1]);
				Vector3 destination = frontPosition - (vectorOfSide*(ratioOfSide));
				this.transform.position=destination;
				int num = trackTransform.Length-1;
				if(i<num)
				{
					this.transform.LookAt(gem);
				}
				else
				{
					this.transform.LookAt(lookAtMe);
				}
				break;
			}
		}
	}
}
