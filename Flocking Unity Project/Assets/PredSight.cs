using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredSight : MonoBehaviour {

	public GameObject sightStart;
	public float lengthInMeters = 2f;
	public float theDistance;


	void FixedUpdate () 
	{

		RaycastHit hit;
		float theDistance;

		//Debug raycast in the editor _ So we can see it
		Vector3 forward = transform.TransformDirection(Vector3.forward) * lengthInMeters;
		Debug.DrawRay(transform.position,forward,Color.green);

		if(Physics.Raycast(transform.position,(forward), out hit ))
		{
			theDistance = hit.distance;
			//print (theDistance+ " from "  + hit.collider.gameObject.name);

			//You want to say
			// if you hit an object
			// the distance you see is the length of the ray and the new distance between you and the object
			// meaning if your looking at a wall and your are behind that wall
			// i only see the wall

			lengthInMeters = theDistance;


		}
		else if(lengthInMeters <1.2f)
			lengthInMeters = 3f;


		if(Physics.Raycast(transform.position,(forward),out hit))
		{
			if(hit.collider.gameObject.name == "Prey")
			{
				Debug.DrawRay(transform.position,forward,Color.gray);
				//print ("Predator has seen prey");
			}
		}
	}
}
