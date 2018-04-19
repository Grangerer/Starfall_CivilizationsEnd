using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Research : MonoBehaviour {
    public string ResearchName { get; set; }
    public string Description { get; set; }
    private int id;
    private bool researched = false;

    protected Research()
    {
    }

    protected Research(string researchName, string description){
		this.ResearchName = researchName;
		this.Description = description;
	}



    public abstract void Apply();

}
