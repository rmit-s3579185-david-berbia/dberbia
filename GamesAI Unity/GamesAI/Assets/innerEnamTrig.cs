using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class innerEnamTrig : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Player") {
			gameObject.GetComponentInParent<enemTrigger>().isFleeing = false;
		}
	}
}
