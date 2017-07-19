using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {


	private Spaceship spaceship;
	bool moving = false;

	// Use this for initialization
	void Start () {
		spaceship = this.GetComponent<Spaceship> ();
	}

	public void Launch(Vector2 direction){
		moving = true;
	}

	IEnumerator Move(){
		//Lerp forward

		yield return 0;
	}

	void OnCollisionEnter(Collision collision){
		moving = false;
	}

}
