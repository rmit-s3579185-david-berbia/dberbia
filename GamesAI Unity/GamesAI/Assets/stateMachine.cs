using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stateMachine : MonoBehaviour {

	public List<IEnumerator> stateList = new List<IEnumerator>();

	void Start(){
		//Add states to list of states here
		StartCoroutine(timedStateSwitch());
		stateList.Add(IdleState());
		stateList.Add(TestState1());
		stateList.Add(TestState2());
		stateList.Add(TestState3());
		stateList.Add(TestState4());
		Debug.Log (stateList);
	}

	//Call to switch states
	public void stateSwitch(int newState){
		foreach (IEnumerator state in stateList) {
			if (stateList.IndexOf (state) == newState) {
				StopAllCoroutines ();
				StartCoroutine(state);
				return;
			}
		}
		StartCoroutine(stateList [0]);
		return;
	}

	//Just a placeholder to show the states changing
	IEnumerator timedStateSwitch(){
		while (true) {
			yield return new WaitForSeconds (5f);
			stateSwitch (Random.Range (0, 6));
		}
	}

	IEnumerator IdleState(){
		yield return new WaitForSeconds (1f);
		Debug.Log ("Idling");
	}

	IEnumerator TestState1(){
		yield return new WaitForSeconds (1f);
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
