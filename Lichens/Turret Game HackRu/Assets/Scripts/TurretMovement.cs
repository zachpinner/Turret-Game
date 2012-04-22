using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretMovement : MonoBehaviour {
	public Transform[] trackTransform;
	public float speed=1f;
	public int index=0;
	public GameObject bullet;
	public Transform gem;
	private float[] trackLengths;
	public int health;
	public bool alive = true;
	public int score = 0;
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
	}
	
	// Update is called once per framez
	void Update () {
	
	}
	public void moveTurret(float strength)
	{
		if(alive)
		{
			for(int i=0; i<trackTransform.Length; i++)
			{
				if(strength<trackLengths[i])
				{
					Vector3 frontPosition = trackTransform[i].position;
					Vector3 vectorOfSide = trackTransform[i].position-trackTransform[i-1].position;
					float ratioOfSide = (trackLengths[i]-strength)/(trackLengths[i]-trackLengths[i-1]);
					Vector3 destination = frontPosition - (vectorOfSide*(ratioOfSide));
					this.transform.position=destination;
					this.transform.LookAt(gem);
					this.transform.Rotate(0,180,0); 
					break;
				}
			}
		}
/*		if(strength>0 && trackTransform.Length>(index+1) && Mathf.Approximately(transform.position.y, trackTransform[index+1].position.y) && Mathf.Approximately(transform.position.x, trackTransform[index+1].position.x) && Mathf.Approximately(transform.position.z, trackTransform[index+1].position.z))
		{
			Debug.Log("incrementing strength");
			index++;
		}
		if(strength <0 && index>0 && Mathf.Approximately(transform.position.y, trackTransform[index].position.y) && Mathf.Approximately(transform.position.x, trackTransform[index].position.x) && Mathf.Approximately(transform.position.z, trackTransform[index].position.z))
		{
			Debug.Log("decrementing strength");
			index--;
		}
		if(strength>0 && index<trackTransform.Length-1)
		{
			this.transform.position=Vector3.MoveTowards(this.transform.position, trackTransform[index+1].position, speed*strength);
			this.transform.LookAt(gem);
		}
		else
		{
			this.transform.position=Vector3.MoveTowards(this.transform.position, trackTransform[index].position, speed*-strength);
			this.transform.LookAt(gem);
		}*/
/*		Vector3 directionVector = (trackTransform[index+1].position-trackTransform[index].position).normalized*strength*speed;
		if(directionVector.magnitude>(trackTransform[index+1].position-this.transform.position).magnitude)
		{
			transform.Translate(trackTransform[index+1].position-this.transform.position, Space.World);
		}
		else
		{
			transform.Translate(directionVector, Space.World);
		}*/
	}
	public void shootBullet()
	{
		if(alive)
		{
		 	foreach (AnimationState state in animation) 
			{
	        	state.speed = 3F;
	        }
			this.animation.Play("attack");
			Debug.Log("BAM!");
			Vector3 directionVector = this.transform.position-GameObject.Find ("gem").transform.position;
			directionVector=directionVector.normalized;
			GameObject.Instantiate(bullet, this.transform.position, Quaternion.identity);
		}
	}
	public IEnumerator ouch(int owwieness)
	{
		health-=owwieness;
		if(health<=0)
		{
			alive = false;
			this.animation.Play("die");
			yield return new WaitForSeconds(1.8f);
			Application.LoadLevel ("Medieval Village");
		}
	}
	void OnGUI()
	{
		GUI.Box(new Rect(0,0,100,30), "Health:" + health);
		GUI.Box (new Rect(0,30,100,30), "Score:" + score);
	}
	public void scorePoints(int points)
	{
		score+=points;
	}
}
