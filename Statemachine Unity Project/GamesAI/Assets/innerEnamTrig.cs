using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class innerEnamTrig : MonoBehaviour {

	//When the player exits the larger range, stop fleeing
	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Player") {
			gameObject.GetComponentInParent<enemTrigger>().isFleeing = false;
		}
	}
}
