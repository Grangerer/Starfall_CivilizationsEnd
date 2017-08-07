using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseSpaceship {

	public string shipType;
	string name ="Unnamed";

	public int modelId;
	public int baseSpeed;
	int speed;
	public int baseDurability;
	int durability;
	int currentDurability;
	public int baseCombat;
	int combat;
	public int baseSightRadius;
	int sightRadius;

	public int costBP;
	public int costCredit;

	Vector3 movementDirection;
	[SerializeField]
	Planet currentPlanet;

	List <string> spaceshipNames = new List<string> {"The Galactic Tempest","The Red Thunder", "The Lightning Capsule","Rockefeller", "The Red Crusader", "The Space Lord", "The Rising Sun", "Explorer", "The Escape Artist", "The Blue Marlin", "The Lucky VII", "The Seeker", "Empires Glory", "The James Cook", "The Ferdinand Magellan", "The Hernan Cortes", "The Vasco da Gama", "The Ponce de Leon", "The Wisdom Bringer"};
	List <string> spaceshipPreNames = new List<string>{"Red","Velvet","Black","Dark","Blue", "Green", "White", "Golden", "Cold", "Old", "New", "Lost", "Hidden", "Holy", "Second", "Shiny", "Dark", "Last", "Rising","Encroaching","First"};
	List <string> spaceshipPostWeatherNames = new List<string>{"Shroud","Tempest","Thunder", "Tempest","Storm", "Hail", "Sunshine","Dawn","Dusk", "Flare", "Eclipse","Sunrise","Tide","Void", "Hurricane", "Lightning", "Wave"};
	List <string> spaceshipPostAnimalNames = new List<string>{"Snake","Cat","Dog", "Raccoon","Eagle","Dolphin","Lion","Wolf","Kraken","Cerberus","Pegasus","Hydra", "Dragon", "Gorilla","Shark","Pelican","Fox","Bear", "Barracuda"};
	 
	public BaseSpaceship(string name = null){
		if (name == null) {
			name = GenerateName();
		}
	}
	string GenerateName(){
		string returnName = "The ";
		float decider = Random.value;
		//5% chance for predetermined name
		if (decider <= 0.05f) {
			returnName = spaceshipNames[Random.Range (0, spaceshipNames.Count-1)];
		} //50% chance for spaceshipPreNames+spaceshipPostWeatherNames
		else if (decider <= 0.55f){
			returnName += spaceshipPreNames[Random.Range (0, spaceshipPreNames.Count-1)] +" "+ spaceshipPostWeatherNames[Random.Range (0, spaceshipPostWeatherNames.Count-1)];
		}//45% chance for spaceshipPreNames+spaceshipPostAnimalNames
		else if (decider <= 1f){
			returnName += spaceshipPreNames[Random.Range (0, spaceshipPreNames.Count-1)] +" "+ spaceshipPostAnimalNames[Random.Range (0, spaceshipPostAnimalNames.Count-1)];
		}
		return returnName;
	
	}
	public void GenerateNew(){
		
	}

	public bool DurabilityCheck(){
		if (Random.Range (1, 100) > currentDurability) {
			return false;
		}
		if (currentDurability <= 25) {
			currentDurability = 0;
		} else {
			currentDurability -= 25;
		}
		return true;

	}

	public void ApplyResearch(ResearchManager rm){
		Debug.Log ("Applying Research!");
		if (rm != null) {
			speed = baseSpeed + (int)((baseSpeed * rm.SpaceshipSpeedResearch.Tier) * 0.05);
			durability = baseDurability + (int)((baseDurability * rm.SpaceshipSpeedResearch.Tier) * 0.05);
			currentDurability = durability;
			combat = baseCombat + (int)((baseCombat * rm.SpaceshipSpeedResearch.Tier) * 0.1);
			sightRadius = baseSightRadius + (int)((baseSightRadius * rm.SpaceshipSpeedResearch.Tier) * 0.05);
		}
	}

	//Propertystuff
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

	public int Speed {
		get {
			return speed;
		}
		set {
			speed = value;
		}
	}

	public int Durability {
		get {
			return durability;
		}
		set {
			durability = value;
		}
	}

	public int Combat {
		get {
			return combat;
		}
		set {
			combat = value;
		}
	}

	public int SightRadius {
		get {
			return sightRadius;
		}
		set {
			sightRadius = value;
		}
	}

	public int CurrentDurability {
		get {
			return currentDurability;
		}
		set {
			currentDurability = value;
		}
	}
}
