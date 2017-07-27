using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

	int bp = 0;
	int bpRate = 0;
	int credits = 0;
	int creditRate = 0;
	int researchPoint = 0;
	int researchRate = 0;

	List<Planet> ownedPlanets = new List<Planet>();

	public void SetupNew(){
		credits = 50;	
	}

	public void Build(Building building){
		PayCredits (building.CostCredit);
		creditRate += building.CreditPerTurn;
		bpRate += building.BuildPoints;
		researchRate += building.Research;

	}
	public void DestructBuilding(Building building){
		creditRate -= building.CreditPerTurn;
		bpRate -= building.BuildPoints;
		researchRate -= building.Research;

	}

	public void AddTurnRessources(){
		credits += creditRate;
		bp += bpRate;
		ResearchPoint += researchRate;
	}

	void PayCredits(int amount){
		if (credits < amount) {
			Debug.Log ("This shouldn't happen! @Player.PayCredits");
		} else {
			credits -= amount;
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

	public int ResearchPoint {
		get {
			return researchPoint;
		}
		set {
			researchPoint = value;
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
}
