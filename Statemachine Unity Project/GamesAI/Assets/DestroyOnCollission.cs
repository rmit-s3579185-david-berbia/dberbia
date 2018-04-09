using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollission : MonoBehaviour {

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.name == "Enemy")
		{
			Destroy(gameObject);
		}
	}
}
