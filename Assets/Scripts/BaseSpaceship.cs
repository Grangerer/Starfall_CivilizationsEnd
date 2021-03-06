﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
// ReSharper disable All


public enum Spaceshiptypes{
	DiscoveryDrone = 0,
	ColonisationShip = 1,
	Mothership = 2,
	Fighter = 3,
	Destroyer = 4,
	InterplanetaryMissile = 5,
	Spaceflare = 6
};
[System.Serializable]
public class BaseSpaceship {

	public Spaceshiptypes shipType;
	string shipName = "Unnamed";
    public bool civil;

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
	public bool canCollonade;

	private int bounceCount = 0;

	public int costBP;
	public int costCredit;

	Vector3 movementDirection;
	Planet currentPlanet;

	static List <string> spaceshipNames = new List<string> {"The Galactic Tempest","The Red Thunder", "The Lightning Capsule","Rockefeller", "The Red Crusader", "The Space Lord", "The Rising Sun", "Explorer", "The Escape Artist", "The Blue Marlin", "The Lucky VII", "The Seeker", "Empires Glory", "The James Cook", "The Ferdinand Magellan", "The Hernan Cortes", "The Vasco da Gama", "The Ponce de Leon", "The Wisdom Bringer"};
	static List <string> spaceshipPreNames = new List<string>{"Red","Velvet","Black","Dark","Blue", "Green", "White", "Golden", "Cold", "Old", "New", "Lost", "Hidden", "Holy", "Second", "Shiny", "Dark", "Last", "Rising","Encroaching","First"};
	static List <string> spaceshipPostWeatherNames = new List<string>{"Shroud","Tempest","Thunder", "Tempest","Storm", "Hail", "Sunshine","Dawn","Dusk", "Flare", "Eclipse","Sunrise","Tide","Void", "Hurricane", "Lightning", "Wave"};
	static List <string> spaceshipPostAnimalNames = new List<string>{"Snake","Cat","Dog", "Raccoon","Eagle","Dolphin","Lion","Wolf","Kraken","Cerberus","Pegasus","Hydra", "Dragon", "Gorilla","Shark","Pelican","Fox","Bear", "Barracuda"};
	 
	public BaseSpaceship(Spaceshiptypes type, string name = null){
		if (name == null) {
			this.shipName = GenerateName();
		}
		shipType = type;
		SetBaseStatsDependingOnType ();
	}
	void SetBaseStatsDependingOnType(){
		switch(shipType){
		case Spaceshiptypes.DiscoveryDrone:
			SetBaseStats (120, 200, 0, 10, false);
			break;
		case Spaceshiptypes.ColonisationShip:
			SetBaseStats (40, 125, 0, 5, true);
			break;
		case Spaceshiptypes.Mothership:
			SetBaseStats (25, 250, 0, 5, true);
			break;
		case Spaceshiptypes.Fighter:
			SetBaseStats (80, 100, 20, 5, true);
			break;
		case Spaceshiptypes.Destroyer:
			SetBaseStats (60, 125, 80, 5, true);
			break;
		case Spaceshiptypes.InterplanetaryMissile:
			SetBaseStats (280, 60, 60, 0, false);
			break;
		case Spaceshiptypes.Spaceflare:
			SetBaseStats (360, 100, 0, 0, false);
			break;
		default:
			Debug.LogError ("Error in SetBaseStatsDependingOnType");
			break;
		}
	}
	void SetBaseStats(int baseSpeed, int baseDurability, int baseCombat, int baseSightRadius, bool canCollonade){
			this.baseSpeed = baseSpeed;
			this.baseDurability = baseDurability;
			this.baseCombat = baseCombat;
			this.baseSightRadius = baseSightRadius;
			this.canCollonade = canCollonade;
		}
			

	public string GenerateName(){
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

    public string GetShipTypeString()
    {
        switch (shipType)
        {
            case Spaceshiptypes.DiscoveryDrone:
                return "Discovery Drone";
            case Spaceshiptypes.ColonisationShip:
                return "Colonisation Ship";
            case Spaceshiptypes.Mothership:
                return "Mothership";
            case Spaceshiptypes.Fighter:
                return "Fighter";
            case Spaceshiptypes.Destroyer:
                return "Destroyer";
            case Spaceshiptypes.InterplanetaryMissile:
                return "Interplanetary Missile";
            case Spaceshiptypes.Spaceflare:
                return "Spaceflare";
        }
        return "No shiptType";
    }


    //Propertystuff
	public string ShipName {
		get {
			return shipName;
		}
		set {
			shipName = value;
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

	public Spaceshiptypes ShipType {
		get {
			return shipType;
		}
		set {
			shipType = value;
		}
	}

	public int BounceCount {
		get {
			return bounceCount;
		}
		set {
			bounceCount = value;
		}
	}
}
