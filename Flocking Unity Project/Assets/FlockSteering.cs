using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockSteering : MonoBehaviour
{

	public FlockController controller;
	public GameObject leader,chasee;
	public bool initiated = false;
	public float minV, maxV, randomness, clumping;
	public Vector3 flockCenter, flockV;




	void Start () 
	{
	
		controller = controller.GetComponent<FlockController>();


		StartCoroutine("flockSteering");	
	
	}



	public Vector3 calc () 
	{
		Vector3 randomize = new Vector3((Random.value *2) -1, (Random.value * 2) -1, (Random.value * 2) -1);

		randomize.Normalize();
		randomize *= randomness;

		flockCenter = controller.flockCenter; 
		flockV = controller.flockVelocity;
		Vector3 follow = chasee.transform.localPosition;

		flockCenter = flockCenter - leader.transform.localPosition;
		flockV = flockV - leader.GetComponent<Rigidbody>().velocity;
		follow = follow - leader.transform.localPosition;

		return (flockCenter+flockV+follow*2+randomize);
	}

	public void setController (FlockController theCon) 
	{
		controller = theCon;
		minV = controller.minVelocity; 
		maxV = controller.maxVelocity;
		randomness 	= controller.randomness;
		chasee = controller.chasee;
		initiated = true;
	}



	IEnumerator flockSteering() 
	{
		while(true) 
		{
			if (initiated) 
			{
				chasee.GetComponent<Rigidbody>().velocity = leader.GetComponent<Rigidbody>().velocity + calc() * Time.deltaTime;

				// enforce minimum and maximum speeds for the boids
				float speed = leader.GetComponent<Rigidbody>().velocity.magnitude;
				if (speed > maxV) 
				{
					chasee.GetComponent<Rigidbody>().velocity = leader.GetComponent<Rigidbody>().velocity.normalized * maxV;
				} 
				else if (speed < minV) 
				{
					chasee.GetComponent<Rigidbody>().velocity = leader.GetComponent<Rigidbody>().velocity.normalized * minV;
				}
			}

			float waitTime = Random.Range(0.3f, 0.5f);

			yield return new WaitForSeconds(waitTime);
		}
	}

}