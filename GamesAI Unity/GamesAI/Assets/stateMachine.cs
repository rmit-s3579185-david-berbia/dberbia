using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using NUnit.Framework.Internal;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using _Scripts.SeekHero;

public class stateMachine : MonoBehaviour {

	public List<IEnumerator> stateList = new List<IEnumerator>();
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
		stateList.Add(IdleState());
		stateList.Add(TestState1());
		stateList.Add(TestState2());
		stateList.Add(TestState3());
		stateList.Add(TestState4());
		
		StartCoroutine(TimedStateSwitch());
		// Go to default state
		//StartCoroutine(StateHandler());

	}

	private void StateSwitch(int state_ref)
	{
		StopCoroutine(stateList[state_ref]);
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
		if (!_seekHero.IsWalking) // && in LOS
		{
			_seekHero.IsWalking = true;
			_seekHero.Seek(Seeker, Target, 8f, _grid);
		}
		
	}

	IEnumerator StateHandler()
	{
		while (true)
		{
			StartCoroutine(IdleState());
			yield return new WaitForSeconds(1f);
		}
		
		
	}

	IEnumerator IdleState()
	{
		_seekHero.Stop();
		yield return null;
		Debug.Log ("Idling");
	}

	IEnumerator TestState1(){
		yield return new WaitForSeconds(1f);
		//yield return new WaitForSeconds (1f);
		Debug.Log ("State 1");
	}

	IEnumerator TestState2(){
		yield return new WaitForSeconds (1f);
		Debug.Log ("State 2");
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
