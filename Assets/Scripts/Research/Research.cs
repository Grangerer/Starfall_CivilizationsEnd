using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Research : MonoBehaviour {

	string researchName;
	string description;
	int tier;
	int cost;
	int costMultiplier;
	bool linearProgression;

	public Research(string name, string description, int baseCost, int costMultiplier,bool linear = false, int tier = 0){
		this.researchName = name;
		this.description = description;
		this.cost = baseCost;
		this.costMultiplier = costMultiplier;
		this.tier = tier;
		this.linearProgression = linear;
	}
	public void NextTier(){
		tier++;
		if (linearProgression) {
			cost *= costMultiplier; 
		} else {
			cost += cost * costMultiplier; 

		}
	}



	public int Tier {
		get {
			return tier;
		}
		set {
			tier = value;
		}
	}
}
