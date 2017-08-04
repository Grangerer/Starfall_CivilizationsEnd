using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

	GameManager gameManager;
	ResearchManager researchManager;

	public string name;
	int buildSpace;
	int spaceport = 3;
	float radius;
	string size;
	string defenseDescriptor;
	public int defense = 0;
	bool ownedByPlayer = false;

	float posX;
	float posZ;

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
	List<string> planetNamesSciFi = new List<string>{ "Earth", "Pride", "Santeri", "York", "Babylon", "Silver", "Dawn", "Platinum", "Prime", "Rock", "Mamba", "Cersei", "Sanctuary", "Hope", "Eurasia","Zero" };
	List<string> planetNamesAdjectives = new List<string>{ "Golden", "Cold", "Old", "New", "Black", "Lost", "Hidden", "Holy", "Second", "Sparkling", "Shiny", "Dark", "Last", "Rising" };

	void Start(){
		gameManager = GameManager.instance;
		researchManager = ResearchManager.instance;
	}

	//Generation
	public void NewPlanet(GameObject planet, bool ownedByPlayer = false){
		name = GenerateName();
		this.ownedByPlayer = ownedByPlayer;
		radius = planet.transform.localScale.x;
		posX = planet.transform.position.x;
		posZ = planet.transform.position.z;
		DetermineSize();
		DetermineDefense ();
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
			buildSpace = 1;
			size = "Dwarf";
		} else if (1 <= radius && radius < 1.5) {
			buildSpace = Random.Range (1, 2);
			size = "Small";
		} else if (1.5 <= radius && radius < 2) {
			buildSpace = Random.Range (1, 3);
			size = "Medium";
		} else if (2 <= radius && radius< 2.5) {
			buildSpace = Random.Range (2, 3);
			size = "Large";
		} else {
			buildSpace = 3;
			size = "Giant";
		}
		for (int i = 0; i < buildSpace; i++) {
			buildings.Add (null);
			buildingsNextTurn.Add (null);
		}
	}
	void DetermineDefense(){
		if (Random.value < 0.75f) {
			defense = 0;
		}else if (radius < 1) {
			defense = Random.Range (10, 50);
		} else if (1 <= radius && radius < 1.5) {
				defense = Random.Range (30, 100);
		} else if (1.5 <= radius && radius < 2) {
			defense = Random.Range (50, 160);
		} else if (2 <= radius && radius< 2.5) {
			defense = Random.Range (70, 230);
		} else {
			defense = Random.Range (90, 310);
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
	//Ingame
	public bool Land(Spaceship spaceship){
		Debug.Log ("@" + spaceship.baseSpaceship.Name+" trying to land on "+name);

		if (defense != 0 && spaceship.baseSpaceship.Combat == 0) {
			return false;
			//Destroy ship
		}else if(defense>spaceship.baseSpaceship.Combat){
			//Fight
			defense -= spaceship.baseSpaceship.Combat;
			SetDefenseDescriptor ();
			return false;
		}else if(ownedByPlayer == false){
			//or collonade
			Collonade(spaceship);
			return true;
		}else{
			//or Go into spaceport
			Debug.Log(spaceship.baseSpaceship.Name +" reached the spaceport on "+name);
			spaceships.Add (spaceship);
			return true;
		}

	}
	void Collonade(Spaceship spaceship){
		Debug.Log(spaceship.baseSpaceship.Name +" collonaded "+name);
		ownedByPlayer = true;
		gameManager.SettlePlanet(this);
		spaceships.Add (spaceship);
	}

	public void StartSpaceship(int spaceshipListPosition){
		//Instantiate GO
		if(spaceshipListPosition<spaceships.Count){
			spaceships[spaceshipListPosition].Spawn ();
		}

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
}
