using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour {


	public static Data instance;

	[SerializeField]
	List<GameObject> spaceShipModels = new List<GameObject>();
	public int Test;

	void Awake() {
		if (instance != null) {
			Debug.LogError("More than one Data in scene!");
			return;
		}
		instance = this;
	}

	public List<GameObject> SpaceShipModels {
		get {
			return spaceShipModels;
		}
		set {
			spaceShipModels = value;
		}
	}
}
