using UnityEngine;
using System.Collections;

public class dieAnimationAfterSec : MonoBehaviour 
{
	
	int count = 0;
	
	// Update is called once per frame
	void Update () {
		if(count==45)
		{
			this.animation.Play ("die");
		}
		count++;
	}
}
