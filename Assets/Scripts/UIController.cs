using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour {

	public static UIController instance;

	public GameObject infoUi;
	public TMP_Text nameText;
	public TMP_Text sizeText;


	public TMP_Text creditsPointText;
	public TMP_Text buildPointText;
	public TMP_Text researchPointText;

	// Use this for initialization
	void Start () {
		infoUi.SetActive(false);
	}
	void Awake() {
		if (instance != null) {
			Debug.LogError("More than one UIController in scene!");
			return;
		}
		instance = this;
	}
	// Update is called once per frame
	void Update () {

	}
	public void SetRessourcePanel(Player player){
		creditsPointText.text = "Credit:\n" + player.Credits + " ("+ player.CreditRate+"/turn)";
		buildPointText.text = "Build:\n" + player.Bp + " ("+ player.BpRate+"/turn)";
		researchPointText.text = "Research:\n" + player.ResearchPoint+ " ("+ player.ResearchRate+"/turn)";
	}

	public void SetPlanetStatPanel(Planet planet = null){
		if (planet != null) {
			infoUi.SetActive(true);
			nameText.text = "Name: " + planet.name;
			sizeText.text = "Size: " + planet.Size;
		} else {
			infoUi.SetActive(false);
		}
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
