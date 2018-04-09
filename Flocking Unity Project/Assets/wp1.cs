﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wp1 : MonoBehaviour {


	void OnTriggerEnter (Collider other)
	{

		if(other.gameObject.tag=="Prey")
		{
			//print ("Prey at wp1");
			StartCoroutine ("GoNext1");

		}
	}

	void OnTriggerExit (Collider other)
	{

		if(other.gameObject.tag=="Prey")
		{

			//print ("changing waypoints");
			StopCoroutine ("GoNext1");

		}
	}

	IEnumerator GoNext1 ()
	{

		yield return new WaitForSeconds (1f);
		//Change random number to determine next waypoint
		do {PreyMovement.randomNumber = Random.Range (1, 5);} 
		while(PreyMovement.randomNumber == 1);
	}

}