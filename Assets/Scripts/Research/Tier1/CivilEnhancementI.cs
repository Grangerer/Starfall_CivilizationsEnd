using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilEnhancementI : Research {

    public CivilEnhancementI()
    {
        ResearchName ="Civil Enhancement I";
        Description = "Civil ships gain 15% increased speed and reveal radius";
		ForSpaceship = true;
    }
	public override void ApplyResearch(ref SpaceshipResearchValues spaceshipResearchValues){
		spaceshipResearchValues.speedMultiplier += 0.15f;
		spaceshipResearchValues.sightRangeMultiplier += 0.15f;
	}

}
