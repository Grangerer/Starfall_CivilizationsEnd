using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchManager : MonoBehaviour {

	public static ResearchManager instance;

    private List<Research> researchesTier1 = new List<Research>();
    private List<Research> researchesTier2 = new List<Research>();
    private List<Research> researchesTier3 = new List<Research>();
    private List<Research> researchesTier4 = new List<Research>();
    private List<Research> researchesTier5 = new List<Research>();

 
    void Awake() {
		if (instance != null) {
			Debug.LogError("More than one ResearchManager in scene!");
			return;
		}
		instance = this;
        Setup();
	}


    void Setup()
    {
        //Tier 1
        ResearchesTier1.Add(new CivilEnhancementI());
        ResearchesTier1.Add(new MilitaryEnhancementI());
        ResearchesTier1.Add(new DynamicDrones());
        /*
        //Tier 2
        researchesTier2.Add(new Research("Kamikaze", "When a fighter is destroyed in a fight, it deals its fight a last time"));
        researchesTier2.Add(new Research("Recycle", "You can destroy ships on your plantes for 75% refund"));
        researchesTier2.Add(new Research("Civil Enhancement II", "Civil ships gain 20% durability and reveal radius"));
        researchesTier2.Add(new Research("Military Enhancement II", "Military ships gain 20% fight and speed"));
        //Tier 3
        researchesTier3.Add(new Research("Maximum Overkill", "Interplanetary missile destroy asteroids"));
        researchesTier3.Add(new Research("Recycle", "Construction bay generates a space flare when built"));
        researchesTier3.Add(new Research("Advanced Thrusters", "All ships gain speed"));
        researchesTier3.Add(new Research("PLowshares to Swords", "Colonisation ships and motherships gain 5 fight"));
        //Tier 4
        researchesTier4.Add(new Research("Economic Miracle", "Industry generates double credits"));
        researchesTier4.Add(new Research("Overshields", "All ships gain 1 Shield against asteroids"));
        researchesTier4.Add(new Research("Invest in Prefabs", "When civil ships colonize a planet the first building is instantly built"));
        researchesTier4.Add(new Research("Hostile Takeover", "When military ships colonize a planet, immediately gain ressources depending on the size of planet"));
        //Tier 5
        researchesTier5.Add(new Research("Hyperdrive", "Colonisation ships gain +250 speed"));
        researchesTier5.Add(new Research("Interplanetary Bombardement", "Destroyer gain +100 sight and attack everything their sight"));
        researchesTier5.Add(new Research("Space Bank", "Gain 5% of your credits each turn"));
        */
    }




    //Propertystuff
    public List<Research> ResearchesTier1
    {
        get { return researchesTier1; }
        set { researchesTier1 = value; }
    }

    public List<Research> ResearchesTier2
    {
        get { return researchesTier2; }
        set { researchesTier2 = value; }
    }

    public List<Research> ResearchesTier3
    {
        get { return researchesTier3; }
        set { researchesTier3 = value; }
    }

    public List<Research> ResearchesTier4
    {
        get { return researchesTier4; }
        set { researchesTier4 = value; }
    }

    public List<Research> ResearchesTier5
    {
        get { return researchesTier5; }
        set { researchesTier5 = value; }
    }
}
