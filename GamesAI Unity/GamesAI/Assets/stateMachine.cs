using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using NUnit.Framework.Internal;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using _Scripts.SeekHero;

public class stateMachine : MonoBehaviour {

	public Dictionary<string, IEnumerator> stateList = new Dictionary<string, IEnumerator>();
	public Transform Target;
	public Transform Seeker;
	public Transform Player;

	private SeekHero _seekHero;
	private Grid _grid;

	private void Awake()
	{
		_grid = transform.parent.GetComponent<Grid>();
		_seekHero = gameObject.AddComponent<SeekHero>();
	}

	void Start()
	{

		//Add states to list of states here
		stateList.Add("Idle", IdleState());
		stateList.Add("Patrol", Patrol());
		stateList.Add("Pursue", Pursue());
		stateList.Add("Flee", Flee());
		
		StateSwitch ("Idle");

	}

	private void StateSwitch(string state_ref)
	{
		//Stops all states and runs new state
		StopAllCoroutines ();
		StartCoroutine(stateList[state_ref]);
	}

//	//Just a placeholder to show the states changing
	private IEnumerator TimedStateSwitch(){
		while (true)
		{
			yield return null;
			if (Player.hasChanged)
			{
				_seekHero.Stop();
				Player.hasChanged = false;
			}
		}
	}


	private void Update()
	{
		//Raycast as sight for seeker
		if (!_seekHero.IsFleeing) {
			RaycastHit hit;
			if (Physics.Raycast (Seeker.position, Seeker.TransformDirection (Vector3.forward), out hit, Mathf.Infinity)) {
				if (hit.collider.gameObject.tag == "Player") {
					Debug.DrawRay (Seeker.position, Seeker.TransformDirection (Vector3.forward) * hit.distance, Color.green);
					//Debug.Log ("Did Hit");
					StateSwitch ("Pursue");
				}
			} else {
				Debug.DrawRay (Seeker.position, Seeker.TransformDirection (Vector3.forward) * 1000, Color.yellow);
				//Debug.Log ("Did not Hit");
			}
		}
		/*if (Seeker.GetComponent<enemTrigger> ().isFleeing) {
			StateSwitch ("Flee");
		}*/
	}

	IEnumerator IdleState()
	{
		_seekHero.Stop();
		Debug.Log ("Idling");
		yield return new WaitForSeconds(2f);
		StateSwitch ("Patrol");
	}

	IEnumerator Patrol(){
		while (true) {
			if (!_seekHero.IsWalking) { // && in LOS
				_seekHero.IsWalking = true;
				_seekHero.Patrol (Seeker, 8f, _grid);
			}
			yield return new WaitForSeconds (0.1f);
			Debug.Log ("Patrolling");
		}
	}

	IEnumerator Pursue(){
		while (true) {
			if (!_seekHero.IsWalking) { // && in LOS
				_seekHero.IsWalking = true;
				_seekHero.Seek (Seeker, Target, 8f, _grid);
			}
			yield return new WaitForSeconds (0.1f);
			Debug.Log ("Pursuing");
		}
	}

	IEnumerator Flee(){
		//Find a new position in the opposite direction to the player, and then find a path to there
		while (Seeker.GetComponent<enemTrigger>().isFleeing) {
			Transform desired_position = new GameObject ().transform;
			desired_position.position = Vector3.Normalize(Seeker.position - Target.position) * 5;
			_seekHero.Seek (Seeker, desired_position, 8f, _grid);
			yield return new WaitForSeconds (0.1f);
			Debug.Log ("Fleeing");
		}
	}

	IEnumerator TestState4(){
		yield return new WaitForSeconds (1f);
		Debug.Log ("State 4");
	}
}
