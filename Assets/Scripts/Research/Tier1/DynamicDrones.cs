using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicDrones : Research {

    public DynamicDrones() {
        ResearchName = "Dynamic Drones";
        Description = "Drone bounces off planets (once)";
    }
	public override void ApplyResearch(ref SpaceshipResearchValues spaceshipResearchValues){
		if (spaceshipResearchValues.spaceshipType == Spaceshiptypes.DiscoveryDrone) {
			spaceshipResearchValues.bounceCount += 1;
		}
	}
}
