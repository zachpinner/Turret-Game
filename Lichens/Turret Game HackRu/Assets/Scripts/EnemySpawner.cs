using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemySpawner : MonoBehaviour {
	public GameObject enemyPrefab;
	public float timer;
	public float spawnTimer=800;
	// Use this for initialization
	void Start () {
		timer=Random.value*spawnTimer;
	}
	
	// Update is called once per frame
	void Update () {
	timer--;
		if(timer<1)
		{
			GameObject.Instantiate(enemyPrefab, this.transform.position, this.transform.rotation);
			timer=Random.value*spawnTimer;
			spawnTimer-=3;
		}
		if(Input.GetKeyDown(KeyCode.C))
		{
			GameObject.Instantiate(enemyPrefab, this.transform.position, this.transform.rotation);
		}
	}
}
