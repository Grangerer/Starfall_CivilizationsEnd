using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLog : MonoBehaviour {

	//Buildings ID=0
	List<Event> buildEvents;
	bool buildEventsDeactivated = false;

	//Spaceships ID=1
	List<Event> constructEvents; 
	bool constructEventsDeactivated = false;

	//Planet colonization ID=2
	List<Event> collonadeEvents;
	bool collonadeEventsDeactivated = false;

	//Combat ID=3
	List<Event> combatEvents;
	bool combatEventsDeactivated = false;

	//Research ID=4
	List<Event> researchEvents;
	bool researchEventsDeactivated = false;

	public void AddEvent(Event newEvent){
		switch (newEvent.GroupID) {
		case 0:
			buildEvents.Add (newEvent);
			break;
		case 1:
			constructEvents.Add (newEvent);
			break;
		case 2:
			collonadeEvents.Add (newEvent);
			break;
		case 3:
			combatEvents.Add (newEvent);
			break;
		case 4:
			researchEvents.Add (newEvent);
			break;
		default:
			break;
		}
	}

	/*
	 * Display all currently activated events in the right order (Most recent turn first)
	 */
	public void DisplayLog(){
		//For each activated:
		//Find all events of turn 0 and add them to the Displaylog
		//Increase turn by 1 and repeat until current turn is reached.
	}
}
