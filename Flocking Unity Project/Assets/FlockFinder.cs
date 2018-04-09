using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockFinder : MonoBehaviour {

	public GameObject agent, leader;
	public float distance;
	public Vector3 dist;

	public PreyMovement pm;

	// Use this for initialization
	void Start () {
		pm = pm.GetComponent<PreyMovement> ();
		
	}
	
	// Update is called once per frame
	void Update () 

	{
		
		if (pm.choiceMade == 2)
			FindFlock ();
	
	}


	//Maybe make this its own class
	public void FindFlock()
	{
		//you need to find the distance from this agent to the closest member of the flock
		distance = Vector3.Distance(agent.transform.position, leader.transform.position);
		agent.transform.position += leader.transform.position;
		transform.position.Normalize();

	}//end find flock
}
