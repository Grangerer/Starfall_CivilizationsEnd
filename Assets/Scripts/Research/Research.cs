using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SpaceshipResearchValues{
	public float speedMultiplier;
	public float sightRangeMultiplier;
	public float fightMultiplier;
	public float durabilityMultiplier;
	public int bounceCount;
	public Spaceshiptypes spaceshipType;

	public SpaceshipResearchValues(Spaceshiptypes spaceshipType){
		this.spaceshipType = spaceshipType;
		speedMultiplier = 0;
		sightRangeMultiplier = 0;
		fightMultiplier = 0;
		durabilityMultiplier = 0;
		bounceCount = 0;

	}
}

public abstract class Research : MonoBehaviour {
    public string ResearchName { get; set; }
    public string Description { get; set; }
    private int id;
	private bool researched = false;
	private bool forSpaceship = false;

	public abstract void ApplyResearch(ref SpaceshipResearchValues spaceshipResearchValues);

	public bool Researched {
		get {
			return researched;
		}
		set {
			researched = value;
		}
	}

	public bool ForSpaceship {
		get {
			return forSpaceship;
		}
		set {
			forSpaceship = value;
		}
	}
}
