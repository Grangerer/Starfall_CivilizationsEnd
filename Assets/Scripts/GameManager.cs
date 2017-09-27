using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	ResearchManager researchManager;

	Data data;
	UIController uiController;
	Player player = new Player ();

	Planet selectedPlanet;
	int selectedBuildSpace;
	Spaceship selectedSpaceship;
	Camera overViewCamera;

	bool nextTurnProcess= false;

	List<Spaceship> spaceships = new List<Spaceship>();

	//Gamestats
	int maxTurns = 50;
	int currentTurn = 0;

	// Use this for initialization
	void Start () {
		uiController = UIController.instance;
		researchManager = ResearchManager.instance;
		data = Data.instance;
		overViewCamera = Camera.main;
		player.SetupNew ();
		uiController.SetRessourcePanel(player);
	}
	void Awake() {
		if (instance != null) {
			Debug.LogError("More than one GameManager in scene!");
			return;
		}
		instance = this;
	}
	// Update is called once per frame
	void Update () {
		if (overViewCamera.enabled) {
			if (Input.GetMouseButtonDown (0)) {
				//Vector3 v3 = Input.mousePosition;
				//Need to insert camera angle
				//v3.z = Camera.main.transform.position.y;
				//Debug.Log ("Mouse: " + v3 + " Camera: " + Camera.main.ScreenToWorldPoint (v3));
				CheckMouseTarget ();
			} else if (Input.GetMouseButtonDown (1)) {
				selectedPlanet = null;
				uiController.SetPlanetStatPanel ();
			}
			//Temp
			if(Input.GetKeyDown(KeyCode.L) && selectedPlanet != null){
				selectedPlanet.StartSpaceship (0);
			}
			//Temp

		}
		uiController.SetRessourcePanel(player);
	}
	void CheckMouseTarget ()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		GameObject selectedTile = null;
		if (Physics.Raycast (ray, out hit, 1000)) {
			if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject ()) {
				if (hit.transform.gameObject.tag == "Planet") {
					selectedPlanet = hit.transform.gameObject.GetComponent<Planet>();
					//Display stats
					uiController.SetPlanetStatPanel(selectedPlanet);
					Debug.Log ("HitPlanet");

				} else if (hit.transform.gameObject.tag == "Spaceship") {
					selectedSpaceship = hit.transform.root.gameObject.GetComponent<Spaceship>();
					//Display stats
					if (!selectedSpaceship.Launched) {
						selectedSpaceship.baseSpaceship.ApplyResearch (researchManager);
						selectedSpaceship.StartLaunchSequence ();
					} else {
						uiController.SetShipInfo (selectedSpaceship);
					}
				}
			}
		}
	}
	public void SettlePlanet(Planet planet){
		player.OwnedPlanets.Add (planet);
	}

	public void InitiateBuildingCreation(int buildspace){
		selectedBuildSpace = buildspace;
		uiController.ShowBuildUI ();
	}


	public void ChooseBuilding(int id){
		Debug.Log ("Building chosen: " + id);
		Building building = new Building (id);
		if (building.CostCredit <= player.Credits) {
			player.Build (building);
			selectedPlanet.Build (building, selectedBuildSpace);
			uiController.SetPlanetStatPanel (selectedPlanet);
			uiController.ShowBuildUI (false);
		}
	}

	public void DestroyBuilding(int id){
		player.DestructBuilding (selectedPlanet.Buildings[id]);
		selectedPlanet.DestroyBuilding (id);
		uiController.SetPlanetStatPanel (selectedPlanet);
	}

	public void SelectSpaceship(int i){
		if (selectedPlanet != null && i < selectedPlanet.Spaceships.Count) {			
			selectedSpaceship = selectedPlanet.Spaceships [i];
			selectedSpaceship.baseSpaceship.ApplyResearch(researchManager);
			uiController.SetShipInfo (selectedSpaceship);
		}
	}
	public void LaunchSpaceship(){
		if (selectedSpaceship != null) {
			//Turn all highlight arrows to look towards spaceship
			selectedSpaceship.StartLaunchSequence ();

			//Turn all hightlight arrows back
		}
	}

	public void ShowHightlightArrow(){
		if (!selectedPlanet.HighlightArrow.activeSelf) {
			selectedPlanet.SetHighlightArrowRotation ();
			selectedPlanet.HighlightArrow.SetActive (true);
		} else {
			selectedPlanet.HighlightArrow.SetActive (false);
		}
	}

	//Turnstuff
	public void NextTurn(){
		if (currentTurn == maxTurns) {
			EndGame ();
		} else if (!nextTurnProcess) {			
			StartCoroutine (ProcessTurn ());
		} else {
			Debug.Log ("NextTurn button should not be visible");
		}
	}

	IEnumerator ProcessTurn(){
		nextTurnProcess = true;
		Debug.Log ("NextTurn process started");
		uiController.Deselect ();
		//Fly all spaceships
		foreach (Spaceship spaceship in spaceships) {
			spaceship.Move ();
		}
		yield return new WaitForSeconds(5f);
		//Count up 10 years during each turn
		//Start from CurrentTurn*10 to (currentTurn+1)*10


		//Calculate new Ressources
		player.AddTurnRessources();
		//Add new ships, buildings and researches
		player.OnTurnStart();
		//Start next turn
		currentTurn++;

		nextTurnProcess = false;
		Debug.Log ("NextTurn process ended");
	}

	void EndGame(){
		Debug.Log ("The universe collapsed. *Show Summary of this run*");
	}

		//Propertystuff
	public List<Spaceship> Spaceships {
		get {
			return spaceships;
		}
		set {
			spaceships = value;
		}
	}

	public bool NextTurnProcess {
		get {
			return nextTurnProcess;
		}
		set {
			nextTurnProcess = value;
		}
	}
}
