using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilitaryEnhancementI : Research {

    public MilitaryEnhancementI() {
        ResearchName = "Military Enhancement I";
        Description = "Military ships gain 15 % fight and durability";
    }

    public override void Apply() {
        throw new System.NotImplementedException();
    }
}
