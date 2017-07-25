﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{

	public BaseSpaceship baseSpaceship;

	[SerializeField]
	Camera shipCamera;
	Camera mainCamera;

	bool launching = false;
	bool launched = false;
	float launchGap = 0.2f;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (launching) {
			Targeting ();
		} else if (launched) {
			//Show stats
		}

	}

	void EndLaunch ()
	{		
		shipCamera.enabled = false;
		mainCamera.enabled = true;
		launching = false;
	}

	void Targeting ()
	{
		if (Input.GetKey (KeyCode.D)) {
			RotateAroundCurrentPlanet (1);
		} else if (Input.GetKey (KeyCode.A)) {
			RotateAroundCurrentPlanet (-1);
		} else if (Input.GetKey (KeyCode.Escape)) {
			EndLaunch ();
		} else if (Input.GetKey (KeyCode.Space)) {
			Launch ();
		}
	}

	void RotateAroundCurrentPlanet (int rotation)
	{
		this.transform.Rotate (new Vector3 (0, rotation));
		float yAngle = this.transform.rotation.eulerAngles.y * Mathf.PI / 180;
		Vector3 positionCorrection = new Vector3 (Mathf.Sin (yAngle) * (baseSpaceship.CurrentPlanet.transform.localScale.x / 2 + launchGap), 0, Mathf.Cos (yAngle) * (baseSpaceship.CurrentPlanet.transform.localScale.x / 2 + launchGap));
		this.transform.position = baseSpaceship.CurrentPlanet.transform.position + positionCorrection;
	}

	void Launch ()
	{
		launching = false;
		launched = true;
		Move ();
		EndLaunch ();
	}


	void Move ()
	{
		StartCoroutine(MoveForward());

	}
	IEnumerator MoveForward(){
		this.GetComponent<Rigidbody> ().isKinematic = false;
		this.GetComponent<Rigidbody>().AddForce(this.transform.forward * baseSpaceship.speed);
		yield return new WaitForSeconds(5f);
		this.GetComponent<Rigidbody> ().isKinematic = true;
		//No planet was reached, check and reduce durability
		if(!baseSpaceship.DurabilityCheck()){
			//Destroy ship
			Debug.Log("I should be destroyed!");
		}
		yield return 0;
	}
	void OnCollisionEnter(Collision collision){
		this.GetComponent<Rigidbody> ().isKinematic = true;
		if (collision.gameObject.tag == "Planet") {	
			if (collision.gameObject.GetComponent<Planet> ().Land (this)) {
				//Disable Ship
			}
		}else{
			//Destroy ship
		}
	}

	public void StartLaunchSequence ()
	{
		mainCamera = Camera.main;
		mainCamera.enabled = false;
		shipCamera.enabled = true;
		launching = true;
	}
}
