using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public List<Material> planetMaterialsEarthlike = new List<Material>();
	public List<Material> planetMaterialsAlienesque = new List<Material>();

	public int quadrantSize;
	public GameObject planet;
	public GameObject sun;
	public int minimalGap;
	int sunGap;

	private GameObject parent;
	// Use this for initialization
	void Start ()
	{
		parent = new GameObject ("PlanetsNStuff");
		sunGap = minimalGap * 3;
		PopulateQuadrant (1, 1);
		PopulateQuadrant (-1, 1);
		PopulateQuadrant (-1, -1);
		PopulateQuadrant (1, -1);

	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void PopulateQuadrant (int x, int z)
	{
		int planetX, planetZ;
		float planetRadius;
		int planetAmount = Random.Range (5, 25);
		int sunAmount = CalculateSunAmount ();
		//Planets
		for (int i = 0; i <= planetAmount; i++) {
			bool spawnable = false;
			int counter = 0;
			do {
				counter++;
				planetX = Random.Range (0, quadrantSize)*x;
				planetZ = Random.Range (0, quadrantSize)*z;
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
				planetX = Random.Range (0, quadrantSize)*x;
				planetZ = Random.Range (0, quadrantSize)*z;
				planetRadius = Random.Range (3, 10);
				if(!CheckOverlap(planetX,planetZ, planetRadius )){
					spawnable= true;
				}
			} while (!spawnable && counter <25);
			if (spawnable) {
				SpawnSun (planetX, planetZ, planetRadius);
			}
		}
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

	void SpawnPlanet (int x, int z, float radius)
	{
		int planetMaterialDecider = Random.Range (0, 100);

		GameObject tmpPlanet = Instantiate (planet, new Vector3 (x, 0, z), Quaternion.identity);
		tmpPlanet.transform.localScale *= radius;
		if (planetMaterialDecider < 50) {
			tmpPlanet.GetComponent<Renderer> ().material = planetMaterialsEarthlike[Random.Range (0, planetMaterialsEarthlike.Count)];
		} else {
			tmpPlanet.GetComponent<Renderer> ().material = planetMaterialsAlienesque[Random.Range (0, planetMaterialsAlienesque.Count)];
		}
		tmpPlanet.transform.Rotate(new Vector3(0, Random.Range(0,360)));
		tmpPlanet.transform.parent = parent.transform;
		tmpPlanet.GetComponent<Planet> ().NewPlanet (tmpPlanet);

	}
	void SpawnSun (int x, int z, float radius)
	{
		GameObject tmpSun = Instantiate (sun, new Vector3 (x, 0, z), Quaternion.identity);
		tmpSun.transform.localScale *= radius;
		tmpSun.transform.parent = parent.transform;

	}

	bool CheckOverlap(int x, int z, float radius){
		Collider[] hitColliders = Physics.OverlapSphere(new Vector3 (x, 0, z), radius + sunGap);
		if (hitColliders.Length != 0) {
			return true;
		}
		return false;			
	}
}
