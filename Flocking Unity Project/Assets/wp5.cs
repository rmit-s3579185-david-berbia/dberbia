using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wp5 : MonoBehaviour {


	void OnTriggerEnter (Collider other)
	{

		if(other.gameObject.tag=="Prey")
		{
			//print ("Prey at wp5");
			StartCoroutine ("GoNext5");

		}
	}

	void OnTriggerExit (Collider other)
	{

		if(other.gameObject.tag=="Prey")
		{

			//print ("changing waypoints");
			StopCoroutine ("GoNext5");
		}
	}

	IEnumerator GoNext5 ()
	{

		yield return new WaitForSeconds (1f);
		//Change random number to determine next waypoint
		do {PreyMovement.randomNumber = Random.Range (1, 5);} 
		while(PreyMovement.randomNumber == 5);

	}

}
