using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	public int health=1;
	public Transform gem;
	Vector3 directionToMove;
	public float speed=5;
	// Use this for initialization
	void Start () {
		//Debug.Log("Spawned enemy with speed:" + speed);
		foreach(GameObject currentEnemy in GameObject.FindGameObjectsWithTag("enemy"))
		{
			if(currentEnemy!=this.gameObject)
			{
				Physics.IgnoreCollision(this.collider, currentEnemy.collider);
			}
		}
		gem=GameObject.Find("gem").transform;
		directionToMove = this.transform.position-gem.position;
		directionToMove=-directionToMove.normalized*speed;
	}

	// Update is called once per frame
	void Update () {
		this.transform.Translate(directionToMove, Space.World);
		if(health>0)
		{
			this.animation.Play("run");
		}
	}
	
	void OnTriggerEnter(Collider theCollision)
	{
		Debug.Log("hit something");
		if(theCollision.transform.name.Equals("bullet(Clone)"))
		{
			health--;
			Destroy(theCollision.gameObject);
			GameObject.Find ("turret").SendMessage("scorePoints", 1);
			if(health==0)
			{
				Destroy (this.gameObject, 0f);
				/*Destroy(this.gameObject, 5f);
				this.animation.Play("Die");
				directionToMove=Vector3.zero;*/
			}
		}
		if(theCollision.transform.name.Equals("gem"))
		{
			Debug.Log("hit gem");
			GameObject.Find ("turret").SendMessage("ouch", 1);
			Destroy(this.gameObject, 15f);
			this.transform.LookAt(GameObject.Find("EndEnemyRoute").transform);
			directionToMove=this.transform.position-GameObject.Find ("EndEnemyRoute").transform.position;
			directionToMove=-directionToMove.normalized*speed;
		}
	}
}
