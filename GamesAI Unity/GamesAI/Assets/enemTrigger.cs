using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemTrigger : MonoBehaviour {

	public bool isFleeing;

	//When the player is within range, begin fleeing
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {
			isFleeing = true;
		}
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Player") {
			isFleeing = false;
		}
	}
}
