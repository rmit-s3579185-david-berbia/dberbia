using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyMovement : MonoBehaviour {


	public float speed, distance, step;
	public Vector3 movement;
	public GameObject leader, prefab, predator, playerInGame, wp1,wp2,wp3,wp4,wp5;
	public bool isAlone, isIdle, isFine, isRunning, canSeePred, canSeePlayer, canSeeFlock, isWandering, isWithFlock, isDead;
	//use static bools to change in other classes to affect non static bools in this class
	public static bool agentAlone, agentIdle, agentFine, agentRunning, agentCanSeePred, agentCanSeePlayer,agentCanSeeFlock, agentWandering, agentIsWithFlock, agentIsDead;
	public int choiceMade, rand;
	public static int randomNumber;
	public string [] choices = {"good","bad","safe"};
	public GameObject[] allPrey = new GameObject[10];
	public GameObject[] allPreyPos = new GameObject[10];
	public FlockFinder ff;
	public Rigidbody rb;
	
	// Use this for initialization
	void Start () {
		movement = Vector3.forward;
		speed = 5f;
		//number 5 will never evaluate we need 1-4 and when dead we use 0, and change to 5 when number is not required
		randomNumber = Random.Range (1,5);
		rb = GetComponent<Rigidbody> ();
		ff = GetComponent<FlockFinder> ();
		step = speed * Time.deltaTime;
		startFlock ();
	}

	public void OnTriggerEnter(Collider other)
	{
	
		if (other.gameObject.tag == ("Player")) 
		{
			canSeePlayer = true;

		
		}

	
	
	}
	// Update is called once per frame
	void FixedUpdate () 
	{
		
		ChooseWhichWay ();
	
		DecideAboutTheWorld();
		//checkStates ();
		
	}

	//Not used here
	public void startFlock()
	{
	
		for (int i = 0; i < 9; i++) 
		{
			GameObject newPrefab =Instantiate (prefab, allPreyPos [i].transform.position, transform.rotation);
			allPrey[i] = prefab;
		}
	}

	public void DecideAboutTheWorld()
	{
		
		/*print (choices[0]);
		print (choices[1]);
		print (choices[2]);*/
		//we need to check through our list of states to make decisions
		if (canSeePred) 
		{
			//print ("Predators are : " + choices [1]);
			choiceMade = 3;

		}
		if (canSeePlayer) 
		{
			//print ("Player is : " + choices [2]);
			choiceMade = 4;
		}
		if (canSeeFlock) 
		{
			//print ("flock members are : " + choices [0]);
			choiceMade = Random.Range(1,2);
		}

	}

	public void checkStates()
	{
		
		if (choiceMade == 1) 
		{
		//isIdle and is ok being alone for a while
			ChooseWhichWay();
		
		}
		if (choiceMade == 2) 
		{
			//is lost and looking for a flock and is not Fine
			FindFlock();

		}
		if (choiceMade == 3) 
		{
			//is running from predator, agent has seen Predator
			Vector3 direction = transform.position - predator.transform.position;
			direction.Normalize ();


			RunFromPred ();
			float distance = Vector3.Distance (transform.position, predator.transform.position);

			if (distance >= 5) 
			{
			//predator is gone
				FindFlock();
			
			}

		}
		if (choiceMade == 4) 
		{
			// agent is repelled by player
			//is running from predator, agent has seen Predator
			Vector3 direction = transform.position - playerInGame.transform.position;
			direction.Normalize ();

			float distance = Vector3.Distance (transform.position, playerInGame.transform.position);

			if (distance <=2) 
			{
				//we want to repel away from player a small distance
				//prefab.GetComponent<Rigidbody>().AddForce(direction*3);
				GetComponent<Rigidbody>().AddForce(direction*3);
			}


		}
		if (choiceMade == 5) 
		{
			//agent has scattered to find a hiding spot and avoid predator

		}
		if (choiceMade == 6) 
		{

			//agent is not happy

		}
		if (choiceMade == 7) 
		{


		}
		if (choiceMade == 0) 
		{
			Dead ();

		}
		if (agentCanSeePred)
			canSeePred = true;
		if (agentCanSeePlayer)
			canSeePlayer = true;
	
	}


	public void ChooseWhichWay()
	{
	
		if (randomNumber == 1) 
		{
			rand = 1;
			//Go to wp1
			//transform.position += wp1.transform.position * step;
			transform.position = Vector3.MoveTowards(transform.position, wp1.transform.position, step);
			transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward,wp1.transform.position, step,5f));
			//transform.LookAt (wp1.transform.position);
			//transform.position.Normalize ();
			//rb.velocity *= speed;
			//transform.position.normalized;
		}
		if (randomNumber == 2) 
		{
			rand = 2;
			//Go to wp2
			//transform.position += wp2.transform.position * step;
			transform.position = Vector3.MoveTowards(transform.position, wp2.transform.position, step);
			//rb.velocity *= speed;
			transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward,wp2.transform.position, step,5f));
			//transform.LookAt (wp2.transform.position);
			//transform.position.Normalize ();
			//rb.velocity *= speed;
			//transform.position.normalized;

		}
		if (randomNumber == 3) 
		{
			rand = 3;
			//Go to wp3
			//transform.position += wp3.transform.position * step;
			transform.position = Vector3.MoveTowards(transform.position, wp3.transform.position, step);
			transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward,wp3.transform.position, step,5f));
			//transform.LookAt (wp3.transform.position);
			//transform.position.Normalize ();
			//rb.velocity *= speed;
			//transform.position.normalized;
		}
		if (randomNumber == 4) 
		{
			rand = 4;
			//Go to wp4
			//transform.position += wp4.transform.position * step;
			transform.position = Vector3.MoveTowards(transform.position, wp4.transform.position, step);
			transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward,wp4.transform.position, step,5f));
			//transform.LookAt (wp4.transform.position);
			//transform.position.Normalize ();fffff
			//rb.velocity *= speed;
			//transform.position.normalized;
		}
	}//end choosewhichway

	public void Wandering()
	{

	//	transform.position = //anywhere on the board /*;
		transform.position.Normalize();

	}//end wandering

	//Maybe make this its own class
	public void FindFlock()
	{
		//you need to find the distance from this agent to the closest member of the flock
		float distance = Vector3.Distance(prefab.transform.position,leader.transform.position);
		if (distance > 5) 
		{


			prefab.transform.position += leader.transform.position;
			prefab.transform.position.Normalize ();
		}
	}//end find flock

	public void RunFromPred()
	{
		transform.position -= predator.transform.position * speed * Time.deltaTime;

	}//end runfrompred

	public void FollowFlockLeader()

	{

		transform.position += leader.transform.position;
		transform.position.Normalize();

	}//end followleader

	public void Dead()
	{
		//Destroy(this,1);
	}//enddead






}//end class
