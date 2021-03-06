﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Planet : MonoBehaviour {

	GameManager gameManager;
	ResearchManager researchManager;

	public string name;
	public int defense = 0;

	private Building planetBonusBuilding;

    int buildSpace;
	int spaceport = 3;
	float radius;
	string size;
	string defenseDescriptor;
	bool ownedByPlayer = false;
	bool visible = false;

	[SerializeField]
	GameObject defenseLight;
	[SerializeField]
	GameObject unsettledLight;
	[SerializeField]
	GameObject ownedLight;

	float posX;
	float posZ;
    [SerializeField]
    private float distanceToCenter;

    [SerializeField]
	GameObject highlightArrow;

	List<Building> buildings = new List<Building>();
	List<Building> buildingsNextTurn = new List<Building>();
	[SerializeField]
	List<Spaceship> spaceships = new List<Spaceship> ();

	List<string> planetNamesGreek = new List<string>{
		"Alpha",
		"Beta",
		"Gamma",
		"Delta",
		"Epsilon",
		"Zeta",
		"Eta",
		"Theta",
		"Iota",
		"Kappa",
		"Lambda",
		"Mu",
		"Nu",
		"Xi",
		"Omnicron",
		"Pi",
		"Rho",
		"Sigma",
		"Tau",
		"Phi",
		"Chi",
		"Psi",
		"Omega"
	};
	List<string> planetNamesSciFi = new List<string>{ "Earth", "Pride", "Santeri", "York", "Babylon", "Silver", "Dawn", "Platinum", "Prime", "Rock", "Mamba", "Cersei", "Sanctuary", "Hope", "Eurasia","Zero", "Horizon" };
	List<string> planetNamesAdjectives = new List<string>{ "Golden", "Cold", "Old", "New", "Black", "Lost", "Hidden", "Holy", "Second", "Sparkling", "Shiny", "Dark", "Last", "Rising" ,"Silver"};

	void Awake(){
		gameManager = GameManager.instance;
		researchManager = ResearchManager.instance;
	}

	//Generation
	public void NewPlanet(GameObject planet, bool startingPlanet = false){
		name = GenerateName();
		this.ownedByPlayer = startingPlanet;
		radius = planet.transform.localScale.x;
		posX = planet.transform.position.x;
		posZ = planet.transform.position.z;
	    DistanceToCenter = CalculateDistanceToCenter();
		if (startingPlanet) {
			SetupSize (3, "Medium");
		} else {
			DetermineSize ();
			CheckForPlanetBonusBuilding ();
		}
        DetermineDefense ();
		SetHighlightSize ();
		this.gameObject.GetComponent<Renderer> ().enabled = false;
	}

	string GenerateName(){
		string returnName;
		float decider = Random.value;
		//5% chance for double greek
		if (decider <= 0.05f) {
			returnName = planetNamesGreek[Random.Range (0, planetNamesGreek.Count-1)] +" "+ planetNamesGreek[Random.Range (0, planetNamesGreek.Count-1)];
		} //20% chance for adjective+greek
		else if (decider <= 0.25f){
			returnName = planetNamesAdjectives[Random.Range (0, planetNamesAdjectives.Count-1)] +" "+ planetNamesGreek[Random.Range (0, planetNamesGreek.Count-1)];
		}//35% chance for adjective+Scifi
		else if (decider <= 0.6f){
			returnName = planetNamesAdjectives[Random.Range (0, planetNamesAdjectives.Count-1)] +" "+ planetNamesSciFi[Random.Range (0, planetNamesSciFi.Count-1)];
		}//35% chance for greek+Scifi
		else if (decider <= 0.95f){
			if (Random.value > 0.5f) {
				returnName = planetNamesSciFi[Random.Range (0, planetNamesSciFi.Count-1)] + " "+ planetNamesGreek[Random.Range (0, planetNamesGreek.Count-1)];
			} else {
				returnName = planetNamesGreek[Random.Range (0, planetNamesGreek.Count-1)] + " "+planetNamesSciFi[Random.Range (0, planetNamesSciFi.Count-1)];
			}
		}//5% chance for adjective+greek+Scifi
		else {
			returnName = planetNamesAdjectives[Random.Range (0, planetNamesAdjectives.Count-1)] +" "+ planetNamesGreek[Random.Range (0, planetNamesGreek.Count-1)]+" "+ planetNamesSciFi[Random.Range (0, planetNamesSciFi.Count-1)];
		}
		return returnName;
	}
	void DetermineSize(){
		if (radius < 1) {
			SetupSize(1, "Dwarf");
		} else if (1 <= radius && radius < 1.5) {
			SetupSize(Random.Range (1, 2),"Small");
		} else if (1.5 <= radius && radius < 2) {
			SetupSize(Random.Range (1, 3),"Medium");
		} else if (2 <= radius && radius< 2.5) {
			SetupSize(Random.Range (2, 3), "Large");
		} else {
			SetupSize(3,"Giant");
		}
	}
	void SetupSize(int buildspace, string sizeDescription){
		this.buildSpace = buildspace;
		size = sizeDescription;
		for (int i = 0; i < buildSpace; i++) {
			buildings.Add (null);
			buildingsNextTurn.Add (null);
		}
	}

	void CheckForPlanetBonusBuilding()
	{
		float distanceToCenter = Mathf.Sqrt(Mathf.Pow(gameObject.transform.position.x, 2) * Mathf.Pow(gameObject.transform.position.z, 2));
		//Implement a chance to have a innate bonus
		if(HasInnateBuilding(distanceToCenter)){
			//Implement the decider for the powerlevel of the innate bonus
			CreatePlanetBonusBuilding(CalculatePlanetBonusBuildingRarity(distanceToCenter));
		}
	}

	bool HasInnateBuilding(float distanceToCenter){
		float baseChance = 0.2f;
		float maxChance = 0.8f;
		float chance;

		chance = Mathf.Min(baseChance + distanceToCenter * 0.0006f, maxChance);

		if (Random.Range (0f, 1f) < chance) {
			return true;
		}
		return false;
	}
	//0=common 1=uncommon, 2=rare, 3=epic
	int CalculatePlanetBonusBuildingRarity(float distanceToCenter){
		float rarityDecider = Random.Range (0f, 100f) + distanceToCenter / 100;

		if (rarityDecider < 40) {
			return 0;
		} else if (rarityDecider < 60) {
			return 1;
		} else if (rarityDecider < 90) {
			return 2;
		} else {
			return 3;
		}

	}

	void CreatePlanetBonusBuilding(int rarity)
	{
		int creditBonus = 0, buildpoints = 0, researchbonus = 0;
		//0=credits 1=buildpoints 2=research
		int randomBonusType = Random.Range(0, 3);
		//Decide BonusType and amount
		if (rarity > 2)
		{
			defense = Random.Range(100, 251);
			//Add a very strong bonus (40-125 credits/1-3 research/1-3 building)
			switch (randomBonusType)
			{
			case 0:
				creditBonus = Random.Range(40, 126);
				break;
			case 1:
				buildpoints = Random.Range(1, 4);
				break;
			case 2:
				researchbonus = Random.Range(1, 4);
				break;
			}
		}else if (rarity > 1)
		{
			defense = Random.Range(50, 126);
			//Add a strong bonus (25-75 credits/1-2 research/1-2 building)
			switch (randomBonusType) {
			case 0:
				creditBonus = Random.Range(25, 76);
				break;
			case 1:
				buildpoints = Random.Range(1, 3);
				break;
			case 2:
				researchbonus = Random.Range(1, 3);
				break;
			}
		} else if (rarity > 0)
		{
			defense = Random.Range(25, 75);
			//Add a medium bonus (5-50 credits/1 research/1 building)
			switch (randomBonusType) {
			case 0:
				creditBonus = Random.Range(5, 50);
				break;
			case 1:
				buildpoints = 1;
				break;
			case 2:
				researchbonus = 1;
				break;
			}
		}
		else
		{            
			//Add a small bonus (1-10 credits)
			creditBonus = Random.Range(1, 11);
		}

		//Get Random name and a fitting description based of bonus type
		string name = "Test";
		string description = "Test";

		PlanetBonusBuilding = new Building(name, description, creditBonus, buildpoints, researchbonus);
	}



	void DetermineDefense(){
		//gameManager = GameManager.instance;
		if (Random.value < 0.75f - 0.05 * gameManager.DifficultyModifier) {
			defense += 0;
		}else if (radius < 1) {
			defense += Random.Range (10 + (int)(1.1 * gameManager.DifficultyModifier), 50 + (int)(2 * gameManager.DifficultyModifier));
		} else if (1 <= radius && radius < 1.5) {
			defense += Random.Range (30 + (int)(1.6 * gameManager.DifficultyModifier), 100 + (int)(3 * gameManager.DifficultyModifier));
		} else if (1.5 <= radius && radius < 2) {
			defense += Random.Range (50 + (int)(2 * gameManager.DifficultyModifier), 160 + (int)(4.2 * gameManager.DifficultyModifier));
		} else if (2 <= radius && radius< 2.5) {
			defense += Random.Range (70 + (int)(2.4 * gameManager.DifficultyModifier), 230 + (int)(5.6 * gameManager.DifficultyModifier));
		} else {
			defense += Random.Range (90 + (int)(2.8 * gameManager.DifficultyModifier), 310 + (int)(7.2 * gameManager.DifficultyModifier));
		}	
		SetDefenseDescriptor ();
	}
	void SetDefenseDescriptor(){
		if (defense == 0) {
			defenseDescriptor = "-";
		}else if (defense<20) {
			defenseDescriptor = "Minimal";
		}else if (defense<60) {
			defenseDescriptor = "Weak";
		}else if (defense<120) {
			defenseDescriptor = "Average";
		}else if (defense<200) {
			defenseDescriptor = "Strong";
		}else {
			defenseDescriptor = "Bulwark";
		}
	}
	void SetHighlightSize(){
		float sizeMultiplier = 1.35f;
		float sizeConstant = 0.5f;
		float lightSize = radius * sizeMultiplier + sizeConstant;
		defenseLight.GetComponent<Light> ().range = lightSize;
		unsettledLight.GetComponent<Light> ().range = lightSize;
		ownedLight.GetComponent<Light> ().range = lightSize;
	}
	//Ingame
	public bool Land(Spaceship spaceship){
		Debug.Log ("@" + spaceship.baseSpaceship.ShipName+" trying to land on "+name);

		if (defense != 0 && spaceship.baseSpaceship.Combat == 0) {	
			//Destroy ship
			return false;
		}else if(defense>spaceship.baseSpaceship.Combat){
			//Fight
			defense -= spaceship.baseSpaceship.Combat;
			SetDefenseDescriptor ();
			return false;
		}else if(!spaceship.baseSpaceship.canCollonade){
			//Shatter
			return false;
		}else if(ownedByPlayer == false){
			//Collonade
			Collonade(spaceship);
			return true;
		}else{
			//Go into spaceport
			Debug.Log(spaceship.baseSpaceship.ShipName +" reached the spaceport on "+name);
			gameManager.EventLog.AddReachingSpacePortEvent(2,this,spaceship.baseSpaceship.ShipName);

			AddSpaceship (spaceship);
			return true;
		}
	}
	void Collonade(Spaceship spaceship){
		Debug.Log(spaceship.baseSpaceship.ShipName +" collonaded "+name);
		//gameManager.EventLog.AddEvent(new Event(spaceship.baseSpaceship.Name +" collonaded "+name,gameManager, gameManager.CurrentTurn,2));
		ownedByPlayer = true;
		gameManager.SettlePlanet(this);
		spaceships.Add (spaceship);
	}

	public void AddSpaceship(Spaceship spaceship){
		spaceships.Add (spaceship);
	}
	public void StartSpaceship(int spaceshipListPosition){
		//Instantiate GO
		if(spaceshipListPosition<spaceships.Count){
			spaceships[spaceshipListPosition].Spawn ();
		}

	}

	public void SetHighlightArrowRotation(bool launching = false){
		if(launching){
			
		}else{
			highlightArrow.transform.rotation = new Quaternion(0,-this.gameObject.transform.rotation.y,0,0);
		}
	}

	public void ToogleHighlightLightSphere(bool activate){
		ownedLight.SetActive (false);
		unsettledLight.SetActive (false);
		defenseLight.SetActive (false);

		GameObject currentHighlight;
		if (ownedByPlayer) {
			currentHighlight = ownedLight;
		} else {
			if (defense == 0) {
				currentHighlight = unsettledLight;
			} else {
				currentHighlight = defenseLight;
			}
		}
		currentHighlight.SetActive (activate);
	}


	public void OnTurnStart(){
		buildings = new List<Building>(buildingsNextTurn);
	}
	public void Build(Building building, int id){
		buildingsNextTurn[id] = building;
	}
	public void DestroyBuilding(int id){
		buildingsNextTurn [id] = null;
	}

    public float CalculateDistanceToCenter()
    {
        if (Math.Abs(posX) < 0.00f || Math.Abs(posZ) < 0.00f)
        {
            return posX + posZ;
        }
        return Mathf.Sqrt(Mathf.Pow(this.posX, 2) + Mathf.Pow(this.posZ, 2));
    }
	//Propertystuff
	public float Radius {
		get {
			return radius;
		}
		set {
			radius = value;
		}
	}

	public string Size {
		get {
			return size;
		}
		set {
			size = value;
		}
	}

	public int Defense {
		get {
			return defense;
		}
		set {
			defense = value;
		}
	}

	public bool OwnedByPlayer {
		get {
			return ownedByPlayer;
		}
		set {
			ownedByPlayer = value;
		}
	}

	public int BuildSpace {
		get {
			return buildSpace;
		}
		set {
			buildSpace = value;
		}
	}

	public List<Building> Buildings {
		get {
			return buildings;
		}
		set {
			buildings = value;
		}
	}

	public string DefenseDescriptor {
		get {
			return defenseDescriptor;
		}
		set {
			defenseDescriptor = value;
		}
	}

	public List<Building> BuildingsNextTurn {
		get {
			return buildingsNextTurn;
		}
		set {
			buildingsNextTurn = value;
		}
	}

	public List<Spaceship> Spaceships {
		get {
			return spaceships;
		}
		set {
			spaceships = value;
		}
	}

	public GameObject HighlightArrow {
		get {
			return highlightArrow;
		}
		set {
			highlightArrow = value;
		}
	}

	public bool Visible {
		get {
			return visible;
		}
		set {
			visible = value;
		}
	}
    public float DistanceToCenter
    {
        get
        {
            return distanceToCenter;
        }
        set
        {
            distanceToCenter = value;
        }
    }

	public Building PlanetBonusBuilding {
		get {
			return planetBonusBuilding;
		}
		set {
			planetBonusBuilding = value;
		}
	}
}
