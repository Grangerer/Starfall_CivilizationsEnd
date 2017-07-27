﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour {

	public static UIController instance;

	public TMP_Text nameText;
	public TMP_Text sizeText;
	public TMP_Text defenseText;

	public GameObject buildingUI; 
	public GameObject buildUI;

	public GameObject shipInfoUI;
	public GameObject planetNameUi;
	public GameObject planetUi;

	public TMP_Text creditsPointText;
	public TMP_Text buildPointText;
	public TMP_Text researchPointText;

	public List<TMP_Text> spaceshipStatUI;
	// Use this for initialization
	void Start () {
		DeactivateUI ();
	}
	void Awake() {
		if (instance != null) {
			Debug.LogError("More than one UIController in scene!");
			return;
		}
		instance = this;
	}


	void DeactivateUI(){
		shipInfoUI.SetActive(false);
		planetNameUi.SetActive(false);
		planetUi.SetActive(false);
	}
	public void SetRessourcePanel(Player player){
		creditsPointText.text = "Credit:\n" + player.Credits + " ("+ player.CreditRate+"/turn)";
		buildPointText.text = "Build:\n" + player.Bp + " ("+ player.BpRate+"/turn)";
		researchPointText.text = "Research:\n" + player.ResearchPoint+ " ("+ player.ResearchRate+"/turn)";
	}

	public void SetPlanetStatPanel(Planet planet = null){
		shipInfoUI.SetActive (false);
		if (planet != null) {
			planetNameUi.SetActive (true);
			nameText.text = planet.name;
			sizeText.text = "Size:\n " + planet.Size;
			if (planet.OwnedByPlayer) {
				planetUi.SetActive (true);
				defenseText.gameObject.SetActive (false);
				SetBuildingUI (planet);
			} else {
				DisableBuildingUI ();
				defenseText.gameObject.SetActive (true);
				defenseText.text = "Defense:\n " + planet.DefenseDescriptor;
			}
		} else {
			DeactivateUI ();
		}
	}
	void SetBuildingUI(Planet planet){
		for (int i = 0; i < planet.BuildSpace; i++) {
			buildingUI.transform.Find ("Building" + i).gameObject.SetActive (true);
			if (planet.Buildings [i] == null) {
				ActivateBuildButton (i);
			} else {
				ActivateBuildButton (i, false);
				SetBuilding (planet, i);
			}
		}
	}
	void ActivateBuildButton(int id, bool activate = true){
		if (activate) {
			buildingUI.transform.Find ("Building" + id).Find ("BuildButton").gameObject.SetActive (true);
			buildingUI.transform.Find ("Building" + id).Find("BuildingUI").gameObject.SetActive (false);
		} else {
			buildingUI.transform.Find ("Building" + id).Find ("BuildButton").gameObject.SetActive (false);
			buildingUI.transform.Find ("Building" + id).Find("BuildingUI").gameObject.SetActive (true);
		}
	}
	void SetBuilding (Planet planet, int buildposition)
	{
		//Debug.Log ("Size: " + planet.Buildings.Count + " - Position: " + buildposition);
		GameObject buildingUIElement = buildingUI.transform.Find ("Building" + buildposition).Find ("BuildingUI").gameObject;

		buildingUIElement.transform.Find ("Name").GetComponent<TMP_Text> ().text = planet.Buildings [buildposition].Name;
		buildingUIElement.transform.Find ("Effect").GetComponent<TMP_Text> ().text = planet.Buildings [buildposition].Description;
	}

	void DisableBuildingUI(){
		for (int i = 0; i < 3; i++) {
			buildingUI.transform.Find ("Building" + i).gameObject.SetActive (false);
		}
	}
	public void Deselect(){
		DeactivateUI ();
	}

	public void SetShipInfo(Spaceship spaceship){
		shipInfoUI.SetActive (true);
		//SetStats
		Debug.Log("Setting spaceshipUI: "+spaceship.baseSpaceship.speed);
		spaceshipStatUI [0].text = ""+spaceship.baseSpaceship.speed;
		spaceshipStatUI [1].text = ""+spaceship.baseSpaceship.durability;
		spaceshipStatUI [2].text = ""+spaceship.baseSpaceship.combat;
		spaceshipStatUI [3].text = ""+spaceship.baseSpaceship.sightRadius;


		if (spaceship.Launched) {
			shipInfoUI.transform.Find ("LaunchButton").gameObject.SetActive (false);
		} else {
			shipInfoUI.transform.Find ("LaunchButton").gameObject.SetActive (true);
		}
	}

	public void ShowBuildUI(bool activate = true){
		buildUI.SetActive (activate);
	}


}
