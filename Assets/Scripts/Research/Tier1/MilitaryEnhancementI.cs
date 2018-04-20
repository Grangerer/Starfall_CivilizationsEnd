using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilitaryEnhancementI : Research {

    public MilitaryEnhancementI() {
        ResearchName = "Military Enhancement I";
		Description = "Military ships gain 15% increased fight and durability";
		ForSpaceship = true;
    }
	public override void ApplyResearch(ref SpaceshipResearchValues spaceshipResearchValues){
		spaceshipResearchValues.fightMultiplier += 0.15f;
		spaceshipResearchValues.durabilityMultiplier += 0.15f;
	}
}
