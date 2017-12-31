using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
	GameObject currentlyVisiblePlanet;
	public GameObject mainMenuPlanet;
	public GameObject optionsPlanet;
	public GameObject newGamePlanet;
	bool rotating = false;

	//Lerp
	bool lerping = false;
	bool lerpRight;
	GameObject lerpOut, lerpIn;
	float timeStartedLerping;
	float xOutOfView = 20f;

	public float degreePerSecond;
	// Use this for initialization
	void Start ()
	{
		//Spawn Random Planet
		currentlyVisiblePlanet = mainMenuPlanet;
		GoToOptions ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Rotate Planet
		if(!rotating){
			//Debug.Log("Starting Rotational Process");
			rotating = true;
			StartCoroutine(RotateObject (currentlyVisiblePlanet));
		}
		if (lerping) {
			StartCoroutine (Switch ());
		}

	}

	IEnumerator RotateObject (GameObject rotationObject)
	{
		Quaternion fromAngle = rotationObject.transform.rotation;
		Quaternion toAngle = Quaternion.Euler(rotationObject.transform.eulerAngles + new Vector3 (0, 1));
		for (float t = 0f; t < 1; t += Time.deltaTime / degreePerSecond) {
			rotationObject.transform.rotation = Quaternion.Lerp (fromAngle, toAngle, t);
			yield return null;
		}
		rotating = false;
	}

	public void GoToOptions(){
		SwitchPlanets (currentlyVisiblePlanet, optionsPlanet, true);
	}
	public void GoToNewGame(){
		SwitchPlanets (currentlyVisiblePlanet, newGamePlanet , true);
	}
	public void GoToMainMenu(){
		SwitchPlanets (currentlyVisiblePlanet, mainMenuPlanet, false);
	}

	void SwitchPlanets(GameObject planetOut, GameObject planetIn, bool back = false){
		lerpIn = planetIn;
		lerpOut = planetOut;
		lerpRight = back;
		lerping = true;
		timeStartedLerping = Time.time;
	}

	IEnumerator Switch(){
		Vector3 centralPosition = new Vector3 (0, 0);
		Vector3 planetInStartPosition;
		Vector3 planetOutEndPosition;
		float timeTakenDuringLerp = 1f;
		if (lerpRight) {
			planetOutEndPosition = new Vector3 (-xOutOfView, 0);
			planetInStartPosition = new Vector3 (xOutOfView, 0);
		} else {
			planetOutEndPosition = new Vector3 (xOutOfView, 0);
			planetInStartPosition = new Vector3 (-xOutOfView, 0);
		}
		float timeSinceStarted = Time.time - timeStartedLerping;
		float percentageComplete = timeSinceStarted / timeTakenDuringLerp;

		lerpOut.transform.position = Vector3.Lerp (centralPosition, planetOutEndPosition, percentageComplete);
		lerpIn.transform.position = Vector3.Lerp (planetInStartPosition, centralPosition, percentageComplete);

		if(percentageComplete >= 1.0f)
		{
			lerping = false;
			currentlyVisiblePlanet = lerpIn;
		}
		yield return null;
	}
}
