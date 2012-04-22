using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {
	Vector3 directionToMove;
	public float speed=1f;
	// Use this for initialization
	void Start () {
		directionToMove = this.transform.position-GameObject.Find ("gem").transform.position;
		directionToMove=directionToMove.normalized*speed;
		Destroy(this.gameObject, 10);
	}
	
	// Update is called once per frame
	void Update () {
			this.transform.Translate(directionToMove);

	}
}
