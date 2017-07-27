using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building {

	int id;
	string name;
	string description;

	int costCredit;

	//Generatorstats
	int creditPerTurn =0;
	int research = 0;
	int buildPoints = 0;

	public Building(int id){
		switch (id) {
		case 0:
			SetupIndustry ();
			break;
		case 1:
			SetupConstructionBay ();
			break;
		case 2:
			SetupLaboratory ();
			break;
		default:
			break;
		}
	}
	void SetupIndustry(){
		id = 0;
		name = "Industry Complex";
		description = "Generates 25 credits per decade.";
		costCredit = 5;
		creditPerTurn = 25;
	}

	void SetupLaboratory(){
		id = 1;
		name = "Laboratory";
		description = "Increases your research by 1.";
		costCredit = 50;
		research = 1;
	}
	void SetupConstructionBay(){
		id = 2;
		name = "Construction Bay";
		description = "Increases your buildpoints and the spaceport of this planet by 2.";
		costCredit = 50;
		buildPoints = 1;
	}



	//Propertystuff
	public int CostCredit {
		get {
			return costCredit;
		}
		set {
			costCredit = value;
		}
	}

	public string Name {
		get {
			return name;
		}
		set {
			name = value;
		}
	}

	public string Description {
		get {
			return description;
		}
		set {
			description = value;
		}
	}

	public int CreditPerTurn {
		get {
			return creditPerTurn;
		}
		set {
			creditPerTurn = value;
		}
	}

	public int Research {
		get {
			return research;
		}
		set {
			research = value;
		}
	}

	public int BuildPoints {
		get {
			return buildPoints;
		}
		set {
			buildPoints = value;
		}
	}
}
