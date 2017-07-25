using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseSpaceship {

	string name ="Unnamed";

	public int speed;
	public int durability;
	public int combat;
	public int sightRadius;
	public int costBP;
	public int costCredit;

	Vector3 movementDirection;
	[SerializeField]
	Planet currentPlanet;

	List <string> spaceshipNames = new List<string> {"Rockefeller", "The Red Crusader", "The Space Lord", "The Rising Sun", "The Tardist", "Explorer", "The Escape Artist", "The Blue Marlin", "The Lucky VII" }; 
	 
	public void GenerateNew(){
	}

	public bool DurabilityCheck(){
		if (Random.Range (1, 100) > durability) {
			return false;
		}
		if (durability <= 25) {
			durability = 0;
		} else {
			durability -= 25;
		}
		return true;

	}


	public string Name {
		get {
			return name;
		}
		set {
			name = value;
		}
	}
	public Planet CurrentPlanet {
		get {
			return currentPlanet;
		}
		set {
			currentPlanet = value;
		}
	}
}
