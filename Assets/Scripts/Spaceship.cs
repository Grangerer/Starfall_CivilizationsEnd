using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
	public BaseSpaceship baseSpaceship;
    public GameObject spaceshipModel;
	ResearchManager researchManager;
	GameManager gameManager;

	public Sprite uiIcon;
	[SerializeField]
	Camera shipCamera;
	Camera mainCamera;

	bool launching = false;
	bool launched = false;
	[SerializeField]
	bool rotating = false;
	float launchGap;

	public Spaceship(Spaceshiptypes type){
		gameManager = GameManager.instance;
		researchManager = ResearchManager.instance;
		mainCamera = Camera.main;
		launchGap = 0.5f;

		baseSpaceship = new BaseSpaceship (type);
		DisableThis ();
        
	}
	public void BasicInitialize(){
		gameManager = GameManager.instance;
		researchManager = ResearchManager.instance;
		mainCamera = Camera.main;
		launchGap = 0.5f;
		DisableThis ();
		this.baseSpaceship.ShipName = this.baseSpaceship.GenerateName ();
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
				hitColliders [i].GetComponent<Planet> ().Visible = true;
				hitColliders [i].GetComponent<Planet> ().ToogleHighlightLightSphere (gameManager.Highlighted);

				Debug.Log ("Planet " + hitColliders [i].GetComponent<Planet> ().name + " found!");
			}
		}
		hitColliders = Physics.OverlapSphere(this.transform.position, baseSpaceship.baseSightRadius *2);
		for (int i = 0; i < hitColliders.Length; i++) {
			if (hitColliders [i].tag == "Sun" && hitColliders [i].GetComponentInChildren<ParticleSystem> ().isStopped) {
				Debug.Log ("Sun discovered!");
				hitColliders [i].GetComponentInChildren<ParticleSystem> ().Play ();
			}
		}
	}
	public void Spawn(){
		baseSpaceship.ApplyResearch (researchManager);
		StartLaunchSequence ();
	}
	public void BuildSpaceship(){
		
	}

	void EndLaunch (bool destroy = false)
	{		
		shipCamera.enabled = false;
		this.transform.Find("CockpitCanvas").gameObject.SetActive (false);
		mainCamera.enabled = true;
		launching = false;
		if (destroy) {
			this.gameObject.SetActive (false);
		}
	}

	void Targeting ()
	{
		if (Input.GetKey (KeyCode.D)) {
			RotateAroundCurrentPlanet (1);
		} else if (Input.GetKey (KeyCode.A)) {
			RotateAroundCurrentPlanet (-1);
		} else if (Input.GetKey (KeyCode.Escape)) {
			EndLaunch (true);
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
		//Remove from currentPlanet
		baseSpaceship.CurrentPlanet.Spaceships.Remove(this);
		EndLaunch ();
	}


	public void Move ()
	{
		StartCoroutine(MoveForward());
	}
	IEnumerator MoveForward(){
		int rotationspeed = 10;
		Rigidbody Rigidbody = this.GetComponentInChildren<Rigidbody> ();
		Rigidbody.isKinematic = false;
		if (rotating) {
			Rigidbody.AddTorque(this.transform.forward * rotationspeed * Time.fixedDeltaTime);
		}
		Rigidbody.AddForce(this.transform.forward * baseSpaceship.Speed);
		yield return new WaitForSeconds(5f);
		Rigidbody.isKinematic = true;
		//No planet was reached, check and reduce durability
		if(!baseSpaceship.DurabilityCheck()){
			//Destroy ship
			DestroyThis();
		}
		yield return 0;
	}
	void OnCollisionEnter(Collision collision){
		Rigidbody Rigidbody = this.GetComponentInChildren<Rigidbody> ();
		Rigidbody.isKinematic = true;
		if (collision.gameObject.tag == "Planet") {	
			if (collision.gameObject.GetComponent<Planet> ().Land (this)) {
				//Disable Ship
				this.launched = false;
				baseSpaceship.CurrentPlanet = collision.gameObject.GetComponent<Planet> ();
				DisableThis ();
			} else if(this.baseSpaceship.Combat==0){
				DestroyThis();
			}
		}else{
			//Destroy ship
			DestroyThis();
		}
	}

	public void StartLaunchSequence ()
	{	
		DebugLogStats ();
	    if (this.gameObject == null)
	    {
            Debug.Log("Instantiating missing model");
	        Instantiate(spaceshipModel);
	    }
		this.gameObject.transform.rotation = new Quaternion (0, 0, 0, 0);
		this.gameObject.SetActive (true);
		mainCamera.enabled = false;
		shipCamera.enabled = true;
		this.transform.Find("CockpitCanvas").gameObject.SetActive (true);
		launching = true;
		RotateAroundCurrentPlanet (0);
	}

    void DisableThis()
    {
        //Debug.Log("I am getting disabled!");
        gameManager.Spaceships.Remove(this);
        this.gameObject.SetActive(false);
    }

	void DestroyThis(){
		Debug.Log("I am getting destroyed!");
		gameManager.Spaceships.Remove (this);
		Destroy (this.gameObject);

	}

	//Debug
	void DebugLogStats(){
		Debug.Log ("Spaceship: \n" +
		"Base dur: " + this.baseSpaceship.baseDurability + "\n" +
		"Dur: " + this.baseSpaceship.Durability + "\n" +
		"Base speed: " + this.baseSpaceship.baseSpeed + "\n" +
		"Speed: " + this.baseSpaceship.Speed + "\n" +
		"Base combat: " + this.baseSpaceship.baseCombat + "\n" +
		"Combat: " + this.baseSpaceship.Combat + "\n" +
		"Base sightradius: " + this.baseSpaceship.baseSightRadius + "\n" +
		"Sightradius: " + this.baseSpaceship.SightRadius);
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
