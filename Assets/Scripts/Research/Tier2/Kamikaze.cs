using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze : Research {

	public Kamikaze() {
		ResearchName = "Kamikaze";
		Description = "When a fighter is destroyed in a fight, it deals its fight a last time";
		ForSpaceship = true;
	}
	public override void ApplyResearch(ref SpaceshipResearchValues spaceshipResearchValues){
		
	}
}
