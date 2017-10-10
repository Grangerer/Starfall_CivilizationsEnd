using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event {

	/* ID	Group
	 * 0	Building
	 * 1	Spaceship
	 * 2	Colonization
	 * 3	Combat
	 * 4	Research
	 */	 
	int groupID;
	string description;
	int turn;

	public Event(string description, int turn, int groupID){
		if (groupID < 0 || groupID > 4) {
			Debug.Log ("Invalid Group ID!");
		} else {
			this.groupID = groupID;
			this.description = description;
			this.turn = turn;
		}
			
	}


	//Propertystuff
	public int GroupID {
		get {
			return groupID;
		}
	}

	public string Description {
		get {
			return description;
		}
	}

	public int Turn {
		get {
			return turn;
		}
	}
}
