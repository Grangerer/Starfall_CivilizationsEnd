using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

	int bp = 0;
	int bpRate = 0;
	int bpRateIncrease = 0;
	int credits = 0;
	int creditRate = 0;
	int creditRateIncrease = 0;
	int researchPoints = 0;
	int researchRate = 0;
	int researchRateIncrease = 0;

	List<int> unlockedShipsIds = new List<int> ();
	List<Planet> ownedPlanets = new List<Planet>();
	List<Spaceship> ownedSpaceships = new List<Spaceship>();



	public void SetupNew(){
		credits = 500; //Should be 50	
	}

	public void OnTurnStart(){
		bpRate += bpRateIncrease;
		creditRate += creditRateIncrease;
		researchRate += researchRateIncrease;
		ResetTurnIncreases ();

		foreach (Planet planet in ownedPlanets) {
			planet.OnTurnStart ();
		}
	}
	void ResetTurnIncreases(){
		creditRateIncrease = 0;
		bpRateIncrease = 0;
		researchRateIncrease = 0;
	}
	public void Build(Building building){		
		if (!building.Destructable) {
			Debug.Log ("Gained boni from a bonus building");
		} else {
			PayCredits (building.CostCredit);
		}
		creditRateIncrease += building.CreditPerTurn;
		bpRateIncrease += building.BuildPoints;
		researchRateIncrease += building.Research;
	}

	public void DestructBuilding(Building building){
		creditRate -= building.CreditPerTurn;
		bpRate -= building.BuildPoints;
		researchRate -= building.Research;
	}
	public void Build(Spaceship spaceship){
		PayCredits (spaceship.baseSpaceship.costCredit);
		PayBP (spaceship.baseSpaceship.costBP);
		ownedSpaceships.Add (spaceship);
	}


	public void AddTurnRessources(){
		credits += creditRate;
		bp += bpRate;
		ResearchPoints += researchRate;
	}

	void PayCredits(int amount){
		if (credits < amount) {
			Debug.Log ("This shouldn't happen! @Player.PayCredits");
		} else {
			credits -= amount;
		}
	}
	void PayBP(int amount){
		if (bp < amount) {
			Debug.Log ("This shouldn't happen! @Player.PayBP");
		} else {
			bp -= amount;
		}
	}
	void PayResearchPoints(int amount){
		if (researchPoints < amount) {
			Debug.Log ("This shouldn't happen! @Player.PayResearchPoints");
		} else {
			researchPoints -= amount;
		}
	}

	public void UnlockShip(int id){
		if (unlockedShipsIds.Contains (id)) {
			Debug.Log ("Ship already unlocked");
		} else {
			unlockedShipsIds.Add (id);
		}
	}

	//PropertyStuff
	public int Bp {
		get {
			return bp;
		}
		set {
			bp = value;
		}
	}

	public int BpRate {
		get {
			return bpRate;
		}
		set {
			bpRate = value;
		}
	}

	public int Credits {
		get {
			return credits;
		}
		set {
			credits = value;
		}
	}

	public int CreditRate {
		get {
			return creditRate;
		}
		set {
			creditRate = value;
		}
	}

	public int ResearchPoints {
		get {
			return researchPoints;
		}
		set {
			researchPoints = value;
		}
	}

	public int ResearchRate {
		get {
			return researchRate;
		}
		set {
			researchRate = value;
		}
	}

	public List<Planet> OwnedPlanets {
		get {
			return ownedPlanets;
		}
		set {
			ownedPlanets = value;
		}
	}

	public List<int> UnlockedShipsIds {
		get {
			return unlockedShipsIds;
		}
		set {
			unlockedShipsIds = value;
		}
	}
}
