using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilEnhancementII : Research {

	public CivilEnhancementII()
	{
		ResearchName ="Civil Enhancement II";
		Description = "Civil ships gain 20% increased durability and reveal radius";
		ForSpaceship = true;
	}
	public override void ApplyResearch(ref SpaceshipResearchValues spaceshipResearchValues){
		spaceshipResearchValues.durabilityMultiplier += 0.20f;
		spaceshipResearchValues.sightRangeMultiplier += 0.20f;
	}
}
