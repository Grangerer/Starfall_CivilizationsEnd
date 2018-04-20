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

	public void AddReachingSpacePortEvent(int turn, Planet planet, string spaceshipName){
		//Perhaps add coordinates to planet
		collonadeEvents.Add (new Event(""+spaceshipName +" reached the spaceport on "+planet.name, turn,2));
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
