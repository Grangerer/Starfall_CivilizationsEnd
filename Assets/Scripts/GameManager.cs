using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	UIController uiController;
	Player player = new Player ();

	Planet selectedPlanet;
	Spaceship selectedSpaceship;
	Camera overViewCamera;

	// Use this for initialization
	void Start () {
		uiController = UIController.instance;
		overViewCamera = Camera.main;
		player.SetupNew ();
		uiController.SetRessourcePanel(player);
	}
	
	// Update is called once per frame
	void Update () {
		if (overViewCamera.enabled) {
			if (Input.GetMouseButtonDown (0)) {
				Vector3 v3 = Input.mousePosition;
				//Need to insert camera angle
				v3.z = Camera.main.transform.position.y;
				Debug.Log ("Mouse: " + v3 + " Camera: " + Camera.main.ScreenToWorldPoint (v3));
				CheckMouseTarget ();
			} else if (Input.GetMouseButtonDown (1)) {
				selectedPlanet = null;
				uiController.SetPlanetStatPanel ();
			}
		}
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
					selectedSpaceship.StartLaunchSequence();
				}
			}
		}
	}

	public void NextTurn(){
		//Fly all spaceships
		//foreach spaceship Move();
		//Calculate new Ressources
		uiController.SetRessourcePanel(player);

		//Add new ships and researches

		//Start next turn
	}
}
