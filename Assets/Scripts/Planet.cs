using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {


	public string name;
	int buildSpace;
	float radius;
	string size;

	float posX;
	float posZ;

	List<Building> buildings = new List<Building>();
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
	List<string> planetNamesSciFi = new List<string>{ "Earth", "Pride", "Santeri", "York", "Babylon", "Silver", "Gold", "Platinum", "Prime" };

	public void NewPlanet(GameObject planet){
		name = GenerateName();
		radius = planet.transform.localScale.x;
		posX = planet.transform.position.x;
		posZ = planet.transform.position.z;
		DetermineSize();
	}
	string GenerateName(){
		string returnName;
		//20% chance for double greek
		if (Random.value >= 0.8f) {
			returnName = planetNamesGreek[Random.Range (0, planetNamesGreek.Count-1)] +" "+ planetNamesGreek[Random.Range (0, planetNamesGreek.Count-1)];
		} else {
			if (Random.value > 0.5f) {
				returnName = planetNamesSciFi[Random.Range (0, planetNamesSciFi.Count-1)] + " "+ planetNamesGreek[Random.Range (0, planetNamesGreek.Count-1)];
			} else {
				returnName = planetNamesGreek[Random.Range (0, planetNamesGreek.Count-1)] + " "+planetNamesSciFi[Random.Range (0, planetNamesSciFi.Count-1)];
			}
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
	// Use this for initialization
	void Setup(){
		
	}

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
}
