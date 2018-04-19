using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Research : MonoBehaviour {
    public string ResearchName { get; set; }
    public string Description { get; set; }
    private int id;
    private bool researched = false;

<<<<<<< HEAD
    protected Research()
    {
    }

    protected Research(string researchName, string description){
		this.ResearchName = researchName;
		this.Description = description;
=======
	string researchName;
	string description;
	int tier;
	int cost;
	int costMultiplier;
	bool linearProgression;

	public Research(string name, string description, int baseCost, int costMultiplier,bool linear = false, int tier = 0){
		this.researchName = name;
		this.description = description;
		this.cost = baseCost;
		this.costMultiplier = costMultiplier;
		this.tier = tier;
		this.linearProgression = linear;
>>>>>>> c7e3e4b13de2cc0d73eb1bca737b270d0904bb34
	}



    public abstract void Apply();

}
