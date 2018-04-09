using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockController : MonoBehaviour {

	public float minVelocity =5;
	public float maxVelocity	= 20;
	public float randomness = 1; 
	public int flockSize = 10;
	public GameObject prefab,chasee ;

	public Vector3 flockCenter, flockVelocity ;

	private GameObject [] flock = new GameObject[10];

	public void Start() {
		
		for (var i=0; i<flockSize; i++) {
			Vector3 position = new Vector3(
				Random.value*GetComponent<Collider>().bounds.size.x,
				Random.value*GetComponent<Collider>().bounds.size.y,
				Random.value*GetComponent<Collider>().bounds.size.z)-GetComponent<Collider>().bounds.extents;
			GameObject newAgent = Instantiate(prefab, transform.position, transform.rotation);
			newAgent.transform.parent = transform;
			newAgent.transform.localPosition = position;

			flock[i] = newAgent;
		}
	}

	public void FixedUpdate () {   
		Vector3 theCenter = Vector3.zero;
		Vector3 theVelocity = Vector3.zero;
		foreach (GameObject newAgent in flock) {
			theCenter       = theCenter + newAgent.transform.localPosition;
			theVelocity     = theVelocity + newAgent.GetComponent<Rigidbody>().velocity;
		}
		flockCenter = theCenter/(flockSize);	
		flockVelocity = theVelocity/(flockSize);
	}
}
