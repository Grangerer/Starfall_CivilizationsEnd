using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour {

	public static UIController instance;

	public TMP_Text nameText;
	public TMP_Text sizeText;

	// Use this for initialization
	void Start () {

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

	public void SetPlanetStatPanel(Planet planet){
		if (planet != null) {
			nameText.text = "Name: " + planet.name;
			sizeText.text = "Size: " + planet.Size;
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
