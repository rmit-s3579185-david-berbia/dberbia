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
		
		StateSwitch ("Idle");

	}

	private void StateSwitch(string state_ref)
	{
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
			yield return new WaitForSeconds (1f);
			Debug.Log ("Patrolling");
		}
	}

	IEnumerator Pursue(){
		if (!_seekHero.IsWalking) // && in LOS
		{
			_seekHero.IsWalking = true;
			_seekHero.Seek(Seeker, Target, 8f, _grid);
		}
		yield return new WaitForSeconds (1f);
		Debug.Log ("Pursuing");
	}

	IEnumerator TestState3(){
		yield return new WaitForSeconds (1f);
		Debug.Log ("State 3");
	}

	IEnumerator TestState4(){
		yield return new WaitForSeconds (1f);
		Debug.Log ("State 4");
	}
}
