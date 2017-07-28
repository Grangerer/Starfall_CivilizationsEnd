using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchManager : MonoBehaviour {

	public static ResearchManager instance;

	//spaceship
	Research spaceshipUnlockResearch = new Research("Prototyping","Unlocks a new spaceship" , 10,1,true);

	Research spaceshipCreditResearch = new Research("Optimized Production","Reduces the credit cost of all ships by 5% per tier" , 1,2);
	Research spaceshipBuildPointResearch = new Research("Efficient Production","Reduces the buildpoint cost of all ships by 1 per tier" , 100,9,true);

	Research spaceshipSpeedResearch = new Research("Faster Engine","Increases the speed of all ships by 5% per tier" , 1,2);
	Research spaceshipCombatResearch = new Research("Stronger Weaponry Engine","Increases the combat of all ships by 10% per tier" , 1,2);
	Research spaceshipDurabilityResearch = new Research("Resilience","Increases the durability of all ships by 5% per tier" , 1,2);
	Research spaceshipSightRadiusResearch = new Research("Improved Scanner","Increases the sight radius of all ships by 5% per tier" , 1,2);

	//buildings
	Research buildingCreditResearch = new Research("Structural Improvement","Reduces the credit cost of all buildings by 5% per tier" , 1,2);



	void Awake() {
		if (instance != null) {
			Debug.LogError("More than one ResearchManager in scene!");
			return;
		}
		instance = this;
	}




	//Propertystuff
	public Research SpaceshipUnlockResearch {
		get {
			return spaceshipUnlockResearch;
		}
		set {
			spaceshipUnlockResearch = value;
		}
	}

	public Research SpaceshipCreditResearch {
		get {
			return spaceshipCreditResearch;
		}
		set {
			spaceshipCreditResearch = value;
		}
	}

	public Research SpaceshipBuildPointResearch {
		get {
			return spaceshipBuildPointResearch;
		}
		set {
			spaceshipBuildPointResearch = value;
		}
	}

	public Research SpaceshipSpeedResearch {
		get {
			return spaceshipSpeedResearch;
		}
		set {
			spaceshipSpeedResearch = value;
		}
	}

	public Research SpaceshipCombatResearch {
		get {
			return spaceshipCombatResearch;
		}
		set {
			spaceshipCombatResearch = value;
		}
	}

	public Research SpaceshipDurabilityResearch {
		get {
			return spaceshipDurabilityResearch;
		}
		set {
			spaceshipDurabilityResearch = value;
		}
	}

	public Research SpaceshipSightRadiusResearch {
		get {
			return spaceshipSightRadiusResearch;
		}
		set {
			spaceshipSightRadiusResearch = value;
		}
	}

	public Research BuildingCreditResearch {
		get {
			return buildingCreditResearch;
		}
		set {
			buildingCreditResearch = value;
		}
	}
}
