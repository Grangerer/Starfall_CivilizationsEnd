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
		for (int i = 1; i <= planet.BuildSpace; i++) {
			buildingUI.transform.Find ("Building" + i).gameObject.SetActive (true);
			if (planet.Buildings.Count >= i) {
				buildingUI.transform.Find ("Building" + i).Find ("BuildButton").gameObject.SetActive (false);
				//Set building
				SetBuilding(planet,i);
			} else if (planet.Buildings.Count + 1  == i) {
				buildingUI.transform.Find ("Building" + i).Find ("BuildButton").gameObject.SetActive (true);
			} else {
				buildingUI.transform.Find ("Building" + i).Find ("BuildButton").gameObject.SetActive (false);
			}

		}
	}
	void SetBuilding(Planet planet, int buildposition){
		Debug.Log ("Size: " + planet.Buildings.Count + " - Position: " + buildposition);
		GameObject buildingUIElement = buildingUI.transform.Find ("Building" + buildposition).Find("BuildingUI").gameObject;
		if (buildingUIElement == null) {
			Debug.Log ("i am null!");
		}
		buildingUIElement.SetActive(true);

		buildingUIElement.transform.Find ("Name").GetComponent<TMP_Text> ().text = planet.Buildings [buildposition-1].Name;
		buildingUIElement.transform.Find ("Effect").GetComponent<TMP_Text> ().text = planet.Buildings [buildposition-1].Description;
	}

	void DisableBuildingUI(){
		for (int i = 1; i <= 3; i++) {
			buildingUI.transform.Find ("Building" + i).gameObject.SetActive (false);
		}
	}
	public void Deselect(){
		DeactivateUI ();
	}

	public void SetShipInfo(Spaceship spaceship){
		shipInfoUI.SetActive (true);
		if (spaceship.Launched) {
			shipInfoUI.transform.Find ("LaunchButton").gameObject.SetActive (false);
		} else {
			shipInfoUI.transform.Find ("LaunchButton").gameObject.SetActive (false);
		}
	}

	public void ShowBuildUI(bool activate = true){
		buildUI.SetActive (activate);
	}

	void RecolorText(int currentValue, int baseValue, TMP_Text recolorText){
		if (currentValue < baseValue) {
			//Recolor damage.text red
			recolorText.color = Color.red;
		}else if(currentValue > baseValue){
			//Recolor text green
			recolorText.color = Color.green;
		}else{
			//Recolor black
			recolorText.color = new Color(50f/255f,50f/255f,50f/255f);
		}
	}

}
