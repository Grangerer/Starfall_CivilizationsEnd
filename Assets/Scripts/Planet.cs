using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {


	public string name;
	int buildSpace;
	float radius;
	string size;
	public int defense = 0;
	bool ownedByPlayer = false;

	float posX;
	float posZ;

	List<Building> buildings = new List<Building>();
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
	List<string> planetNamesSciFi = new List<string>{ "Earth", "Pride", "Santeri", "York", "Babylon", "Silver", "Dawn", "Platinum", "Prime", "Rock", "Mamba", "Cersei", "Sanctuary", "Hope" };
	List<string> planetNamesAdjectives = new List<string>{ "Golden", "Cold", "Old", "New", "Black", "Lost", "Hidden", "Holy", "Second", "Sparkling", "Shiny", "Dark", "Last", "Rising" };
	//Generation
	public void NewPlanet(GameObject planet){
		name = GenerateName();
		radius = planet.transform.localScale.x;
		posX = planet.transform.position.x;
		posZ = planet.transform.position.z;
		DetermineSize();
		DetermineDefense ();
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
	}
	void DetermineDefense(){
		if (Random.value < 0.75f) {
			defense = 0;
			return;
		}
		if (radius < 1) {
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
	}
	//Ingame
	public bool Land(Spaceship spaceship){
		Debug.Log ("@" + spaceship.baseSpaceship.Name+" trying to land on "+name);

		if(defense>spaceship.baseSpaceship.combat){
			//Fight
			defense -= spaceship.baseSpaceship.combat;
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
		spaceships.Add (spaceship);
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
}
