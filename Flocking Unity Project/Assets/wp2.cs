using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wp2 : MonoBehaviour {


	void OnTriggerEnter (Collider other)
	{

		if(other.gameObject.tag=="Prey")
		{
			//print ("Prey at wp2");
			StartCoroutine ("GoNext2");

		}
	}

	void OnTriggerExit (Collider other)
	{

		if(other.gameObject.tag=="Prey")
		{

			//print ("changing waypoints");
			StopCoroutine ("GoNext2");
		}
	}

	IEnumerator GoNext2 ()
	{

		yield return new WaitForSeconds (1f);
		//Change random number to determine next waypoint
		do {PreyMovement.randomNumber = Random.Range (1, 5);} 
		while(PreyMovement.randomNumber == 2);

	}

}
