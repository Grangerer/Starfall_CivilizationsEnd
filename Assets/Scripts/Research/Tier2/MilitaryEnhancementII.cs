using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilitaryEnhancementII : Research {

	public MilitaryEnhancementII() {
		ResearchName = "Military Enhancement II";
		Description = "Military ships gain 20% increased fight and speed";
		ForSpaceship = true;
	}
	public override void ApplyResearch(ref SpaceshipResearchValues spaceshipResearchValues){
		spaceshipResearchValues.fightMultiplier += 0.20f;
		spaceshipResearchValues.speedMultiplier += 0.20f;
	}
}
