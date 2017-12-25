using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	GameManager gameManager;

	public List<Material> planetMaterialsEarthlike = new List<Material>();
	public List<Material> planetMaterialsAlienesque = new List<Material>();

	public int quadrantSize;
	public GameObject planet;
	public GameObject sun;
	public int minimalGap;
	int sunGap;
	int initialExplorationRadius = 5;

	private GameObject parent;
	// Use this for initialization
	void Start ()
	{
		gameManager = GameManager.instance;

		parent = new GameObject ("PlanetsNStuff");
		//Starting Planet
		Planet startingPlanet = SpawnPlanet(0,0,1,true);
        //2xdrones
		SpawnSpaceshipOnPlanet(startingPlanet, Spaceshiptypes.DiscoveryDrone);
		SpawnSpaceshipOnPlanet(startingPlanet, Spaceshiptypes.DiscoveryDrone);
        //1xColonisationship
		SpawnSpaceshipOnPlanet(startingPlanet, Spaceshiptypes.ColonisationShip);

        sunGap = minimalGap * 3;
		for (int i = 0; i < 10; i++) {
			for (int j = 0; j < 10; j++) {
				PopulateQuadrant (i, j);
				PopulateQuadrant (-i, j);
				PopulateQuadrant (-i, -j);
				PopulateQuadrant (i, -j);
			}
		}
		Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, initialExplorationRadius);
		for (int i = 0; i < hitColliders.Length; i++) {
			if (hitColliders [i].tag == "Planet" && hitColliders [i].GetComponent<Renderer> ().enabled == false) {
				hitColliders [i].GetComponent<Renderer> ().enabled = true;
			} else if (hitColliders [i].tag == "Sun" && hitColliders [i].GetComponent<Renderer> ().enabled == false) {
				hitColliders [i].GetComponentInChildren<ParticleSystem> ().Play ();
			}
		}
	}


    void PopulateQuadrant (int x, int z)
	{
		int planetX, planetZ;
		float planetRadius;
		int minimumPlanets = 6 - (int)(0.5 * gameManager.DifficultyModifier);
		int maximumPlanets = 25 - gameManager.DifficultyModifier;

		int planetAmount = Random.Range (minimumPlanets, maximumPlanets);
		int sunAmount = CalculateSunAmount ();
		//Planets
		for (int i = 0; i <= planetAmount; i++) {
			bool spawnable = false;
			int counter = 0;
			do {
				counter++;
				planetX = Random.Range (quadrantSize*(x-1), quadrantSize*x);
				planetZ = Random.Range (quadrantSize*(z-1), quadrantSize*z);
				planetRadius = Random.Range (0.5f, 3);
				if(!CheckOverlap(planetX,planetZ, planetRadius )){
					spawnable= true;
				}
			} while (!spawnable &&counter<25);
			if (spawnable) {
				SpawnPlanet (planetX, planetZ, planetRadius);
			}
		}
		//Suns
		for (int i = 0; i < sunAmount; i++) {
			bool spawnable = false;
			int counter = 0;
			do {
				counter++;
				planetX = Random.Range (quadrantSize*(x-1), quadrantSize*x);
				planetZ = Random.Range (quadrantSize*(z-1), quadrantSize*z);
				planetRadius = Random.Range (3, 10);
				if(!CheckOverlap(planetX,planetZ, planetRadius )){
					spawnable= true;
				}
			} while (!spawnable && counter <25);
			if (spawnable) {
				SpawnSun (planetX, planetZ, planetRadius);
			}
		}
		//Obstacles
	}
	int CalculateSunAmount(){
		int decider = Random.Range (1, 100);
		if (decider <= 35) {
			return 0;
		} else if (decider <= 95) {
			return 1;
		}if (decider <= 99) {
			return 2;
		}if (decider <= 100) {
			return 3;
		}
		Debug.LogError ("CalculateSunAmount");
		return -1;

	}

	Planet SpawnPlanet (int x, int z, float radius, bool startingPlanet = false)
	{
		int planetMaterialDecider = Random.Range (0, 100);

		GameObject tmpPlanet = Instantiate (planet, new Vector3 (x, 0, z), Quaternion.identity);
		tmpPlanet.transform.localScale *= radius;
		if (planetMaterialDecider < 50 || startingPlanet) {
			tmpPlanet.GetComponent<Renderer> ().material = planetMaterialsEarthlike[Random.Range (0, planetMaterialsEarthlike.Count)];
		} else {
			tmpPlanet.GetComponent<Renderer> ().material = planetMaterialsAlienesque[Random.Range (0, planetMaterialsAlienesque.Count)];
		}
		tmpPlanet.transform.Rotate(new Vector3(0, Random.Range(0,360)));
		tmpPlanet.transform.parent = parent.transform;
		tmpPlanet.GetComponent<Planet> ().NewPlanet (tmpPlanet,startingPlanet);
		tmpPlanet.GetComponent<Planet> ().Visible = startingPlanet;
		gameManager.Planets.Add (tmpPlanet.GetComponent<Planet> ());

		return tmpPlanet.GetComponent<Planet> ();
	}
	void SpawnSun (int x, int z, float radius)
	{
		GameObject tmpSun = Instantiate (sun, new Vector3 (x, 0, z), Quaternion.identity);
		tmpSun.transform.localScale *= radius;
		tmpSun.transform.parent = parent.transform;
		tmpSun.GetComponentInChildren<ParticleSystem> ().Stop();
	}

	bool CheckOverlap(int x, int z, float radius){
		Collider[] hitColliders = Physics.OverlapSphere(new Vector3 (x, 0, z), radius + sunGap);
		if (hitColliders.Length != 0) {
			return true;
		}
		return false;			
	}
	void SpawnSpaceshipOnPlanet(Planet planet, Spaceshiptypes type){
		GameObject spaceshipModel = Instantiate(Data.instance.SpaceShipModels[(int)type], new Vector3(0, 0, 0), Quaternion.identity);
		spaceshipModel.GetComponent<Spaceship> ().BasicInitialize ();
		spaceshipModel.GetComponent<Spaceship> ().baseSpaceship.CurrentPlanet = planet;
		planet.AddSpaceship (spaceshipModel.GetComponent<Spaceship>());
		spaceshipModel.SetActive (false);
	}
}