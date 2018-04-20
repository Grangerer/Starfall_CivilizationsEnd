using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{

	public static UIController instance;

	public TMP_Text nameText;
	public TMP_Text sizeText;
	public TMP_Text defenseText;

	public GameObject buildingUI;
	public GameObject buildUI;
	public GameObject constructSpaceshipUI;

	public GameObject shipInfoUI;
	public GameObject planetNameUI;
	public GameObject planetUI;
	public GameObject researchUI;

	public TMP_Text creditsPointText;
	public TMP_Text buildPointText;
	public TMP_Text researchPointText;

	public List<Button> SpaceshipButton;
	public List<TMP_Text> spaceshipStatUI;
	// Use this for initialization
	void Start ()
	{
		DeactivateUI ();
	}

	void Awake ()
	{
		if (instance != null) {
			Debug.LogError ("More than one UIController in scene!");
			return;
		}
		instance = this;
	}

	public void DeactivateHighestUI ()
	{
		if (buildUI.activeSelf || constructSpaceshipUI.activeSelf) {
			buildUI.SetActive (false);
			constructSpaceshipUI.SetActive (false);
		} else {
			DeactivateUI ();
		}

	}

	void DeactivateUI ()
	{
		shipInfoUI.SetActive (false);
		planetNameUI.SetActive (false);
		planetUI.SetActive (false);
	}

	public void SetRessourcePanel (Player player)
	{
		creditsPointText.text = "Credit:\n" + player.Credits + " (" + player.CreditRate + "/turn)";
		buildPointText.text = "Build:\n" + player.Bp + " (" + player.BpRate + "/turn)";
		researchPointText.text = "Research:\n" + player.ResearchPoints + " (" + player.ResearchRate + "/turn)";
	}

	public void SetPlanetStatPanel (Planet planet = null)
	{
		shipInfoUI.SetActive (false);
		if (planet != null) {
			planetNameUI.SetActive (true);
			nameText.text = planet.name;
			sizeText.text = "Size:\n " + planet.Size;
			if (planet.OwnedByPlayer) {
				planetUI.SetActive (true);
				defenseText.gameObject.SetActive (false);
				SetBuildingUI (planet);
				SetStationedSpaceshipUI (planet);
			} else {
				planetUI.SetActive (false);
				DisableBuildingUI ();
				defenseText.gameObject.SetActive (true);
				defenseText.text = "Defense:\n " + planet.DefenseDescriptor;
			}
		} else {
			DeactivateUI ();
		}
	}
	//Building
	void SetBuildingUI (Planet planet)
	{
		for (int i = 0; i < 3; i++) {
			buildingUI.transform.Find ("Building" + i).gameObject.SetActive (true);
			buildingUI.transform.Find ("Building" + i).Find ("BuildingPic").gameObject.SetActive (true);
			buildingUI.transform.Find ("Building" + i).Find ("BuildButton").gameObject.SetActive (false);
			buildingUI.transform.Find ("Building" + i).Find ("BuildingUI").gameObject.SetActive (false);
			buildingUI.transform.Find ("Building" + i).Find ("UnderConstruction").gameObject.SetActive (false);
			if (i >= planet.BuildSpace) {
				buildingUI.transform.Find ("Building" + i).Find ("BuildingPic").gameObject.SetActive (false);
			} else if (planet.BuildingsNextTurn [i] != planet.Buildings [i]) {
				buildingUI.transform.Find ("Building" + i).Find ("UnderConstruction").gameObject.SetActive (true);
			} else if (planet.Buildings [i] == null) {
				buildingUI.transform.Find ("Building" + i).Find ("BuildButton").gameObject.SetActive (true);
			} else {
				//Debug.Log ("I shouldn't be here: " + planet.BuildingsNextTurn [i].Name + " new " + planet.Buildings [i].Name);
				buildingUI.transform.Find ("Building" + i).Find ("BuildingUI").gameObject.SetActive (true);
				SetBuilding (planet, i);
			}
		}
	}

	void SetBuilding (Planet planet, int buildposition)
	{
		//Debug.Log ("Size: " + planet.Buildings.Count + " - Position: " + buildposition);
		GameObject buildingUIElement = buildingUI.transform.Find ("Building" + buildposition).Find ("BuildingUI").gameObject;
		GameObject constructionButton = buildingUIElement.transform.Find ("ConstructionButton").gameObject;
		//Industry Complex
		if (planet.Buildings [buildposition].Id == 0) {
			constructionButton.SetActive (false);
		}//Laboratory
		else if (planet.Buildings [buildposition].Id == 1) {
			constructionButton.transform.Find ("TextMeshPro Text").GetComponent<TMP_Text> ().text = "Research";
			constructionButton.SetActive (true);
		}//Construction Bay
		else if (planet.Buildings [buildposition].Id == 2) {
			constructionButton.transform.Find ("TextMeshPro Text").GetComponent<TMP_Text> ().text = "Construct";
			constructionButton.SetActive (true);
		}
		buildingUIElement.transform.Find ("Name").GetComponent<TMP_Text> ().text = planet.Buildings [buildposition].Name;
		buildingUIElement.transform.Find ("Effect").GetComponent<TMP_Text> ().text = planet.Buildings [buildposition].Description;
	}
	void DisableBuildingUI ()
	{
		for (int i = 0; i < 3; i++) {
			buildingUI.transform.Find ("Building" + i).gameObject.SetActive (false);
		}
	}
	//Spaceship
	void SetStationedSpaceshipUI (Planet planet)
	{
		for (int i = 0; i < SpaceshipButton.Count; i++) {
			if (i < planet.Spaceships.Count) {
				SpaceshipButton [i].gameObject.SetActive (true);
				//Insert Picture
				SpaceshipButton[i].image.sprite = planet.Spaceships[i].uiIcon;
			} else {
				SpaceshipButton [i].gameObject.SetActive (false);
			}
		}
	}


	public void Deselect ()
	{
		DeactivateUI ();
	}

	public void SetShipInfo (Spaceship spaceship)
	{
		shipInfoUI.SetActive (true);
		spaceshipStatUI [4].text = spaceship.baseSpaceship.ShipName;
		//SetStats
		spaceshipStatUI [0].text = "" + spaceship.baseSpaceship.Speed;
		spaceshipStatUI [1].text = "" + spaceship.baseSpaceship.CurrentDurability + "/" + spaceship.baseSpaceship.Durability;
		spaceshipStatUI [2].text = "" + spaceship.baseSpaceship.Combat;
		spaceshipStatUI [3].text = "" + spaceship.baseSpaceship.SightRadius;
		shipInfoUI.transform.Find ("Image").gameObject.GetComponent<Image>().sprite = spaceship.uiIcon;

		if (spaceship.Launched) {
			shipInfoUI.transform.Find ("LaunchButton").gameObject.SetActive (false);
		} else {
			shipInfoUI.transform.Find ("LaunchButton").gameObject.SetActive (true);
		}
	}

	public void ShowBuildUI (bool building, bool spaceship)
	{
		constructSpaceshipUI.SetActive (spaceship);
		buildUI.SetActive (building);
	}
	//ResearchUI
	public void ShowResearchUI(){}

	public void DisableThisUI(GameObject uiElement){
		uiElement.SetActive (false);
	}

	public void ToggleNextTurnButton(bool visible){
		this.transform.Find ("NextTurnButton").gameObject.SetActive (visible);
	}
}
