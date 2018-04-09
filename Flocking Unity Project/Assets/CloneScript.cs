using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneScript : MonoBehaviour {

	public GameObject clone, leader, pred, player;

	public bool isFollowing, isDead, isLost;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
	
		checkstates ();
	}

	void checkstates()
	{
		float distance = Vector3.Distance (transform.position, leader.transform.position);
		

		if (distance < 2) {
			isFollowing = true;
			isLost = false;
		} else
			isLost = true;

		if (isLost) 
		{
			isFollowing = false;
			FindLeader ();
		}

		if (isDead) {
			Destroy (clone, 1);
		}
	}

	void FindLeader()
	{
		transform.position = Vector3.MoveTowards(transform.position, leader.transform.position, 5*Time.deltaTime);
		transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward,leader.transform.position, 5*Time.deltaTime,5f));
	}


}
