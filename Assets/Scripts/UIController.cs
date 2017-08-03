using System.Collections;
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

	public List<Button> SpaceshipButton;
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
		for (int i = 0; i < 3; i++) {
			buildingUI.transform.Find ("Building" + i).gameObject.SetActive (true);
			buildingUI.transform.Find ("Building" + i).Find ("BuildingPic").gameObject.SetActive (true);
			buildingUI.transform.Find ("Building" + i).Find ("BuildButton").gameObject.SetActive (false);
			buildingUI.transform.Find ("Building" + i).Find("BuildingUI").gameObject.SetActive (false);
			buildingUI.transform.Find ("Building" + i).Find("UnderConstruction").gameObject.SetActive (false);
			if(i>=planet.BuildSpace){
				buildingUI.transform.Find ("Building" + i).Find ("BuildingPic").gameObject.SetActive (false);
			}else if (planet.BuildingsNextTurn [i] != planet.Buildings [i]) {
				buildingUI.transform.Find ("Building" + i).Find("UnderConstruction").gameObject.SetActive (true);
			}else if (planet.Buildings [i] == null) {
				buildingUI.transform.Find ("Building" + i).Find ("BuildButton").gameObject.SetActive (true);
			} else {
				Debug.Log ("I shouldn't be here: " + planet.BuildingsNextTurn [i].Name + " new " + planet.Buildings [i].Name);
				buildingUI.transform.Find ("Building" + i).Find("BuildingUI").gameObject.SetActive (true);
				SetBuilding (planet, i);
			}
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
		Debug.Log("Setting spaceshipUI: "+spaceship.baseSpaceship.Speed);
		spaceshipStatUI [0].text = ""+spaceship.baseSpaceship.Speed;
		spaceshipStatUI [1].text = ""+spaceship.baseSpaceship.Durability;
		spaceshipStatUI [2].text = ""+spaceship.baseSpaceship.Combat;
		spaceshipStatUI [3].text = ""+spaceship.baseSpaceship.SightRadius;


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
