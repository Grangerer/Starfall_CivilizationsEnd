using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{

	public BaseSpaceship baseSpaceship;

	ResearchManager researchManager;
	GameManager gameManager;

	[SerializeField]
	Camera shipCamera;
	Camera mainCamera;

	bool launching = false;
	bool launched = false;
	float launchGap = 0.2f;

	// Use this for initialization
	void Start ()
	{
		gameManager = GameManager.instance;
		researchManager = ResearchManager.instance;
	}

	// Update is called once per frame
	void Update ()
	{
		if (launching) {
			Targeting ();
		} else if (launched && gameManager.NextTurnProcess) {
			Discover ();
		}
	}
	void Discover() {
		Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, baseSpaceship.baseSightRadius);
		for (int i = 0; i < hitColliders.Length; i++) {
			if (hitColliders [i].tag == "Planet" && hitColliders [i].GetComponent<Renderer> ().enabled == false) {
				hitColliders [i].GetComponent<Renderer> ().enabled = true;
				Debug.Log ("Planet " + hitColliders [i].GetComponent<Planet> ().name + " found!");
			}
		}
	}

	public void BuildSpaceship(){
		
	}

	void EndLaunch ()
	{		
		shipCamera.enabled = false;
		this.transform.Find("Canvas").gameObject.SetActive (false);
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
		launched = true;
		//Add to gameManager spaceshiplist
		gameManager.Spaceships.Add(this);
		EndLaunch ();
	}


	public void Move ()
	{
		StartCoroutine(MoveForward());

	}
	IEnumerator MoveForward(){
		this.GetComponent<Rigidbody> ().isKinematic = false;
		this.GetComponent<Rigidbody>().AddForce(this.transform.forward * baseSpaceship.baseSpeed);
		yield return new WaitForSeconds(5f);
		this.GetComponent<Rigidbody> ().isKinematic = true;
		//No planet was reached, check and reduce durability
		if(!baseSpaceship.DurabilityCheck()){
			//Destroy ship
			Destroy();
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
			Destroy();
		}
	}

	public void StartLaunchSequence ()
	{
		mainCamera = Camera.main;
		mainCamera.enabled = false;
		shipCamera.enabled = true;
		this.transform.Find("Canvas").gameObject.SetActive (true);
		launching = true;
	}

	void Destroy(){
		Debug.Log("I should be destroyed!");
	}

	//Propertystuff
	public bool Launched {
		get {
			return launched;
		}
		set {
			launched = value;
		}
	}
}
