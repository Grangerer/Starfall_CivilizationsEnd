using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BackgroundManager : MonoBehaviour
{
	GameObject currentlyVisiblePlanet;

	public TMP_Text menuNameText;
	List<string> menuNameList = new List<string> {"New Game", "Options"};

	public List<GameObject> menuPlanets;

	int visiblePlanetID = 0;

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
		visiblePlanetID = 0;
		//Spawn Random Planet
		currentlyVisiblePlanet = menuPlanets [visiblePlanetID];
		menuNameText.text = menuNameList [visiblePlanetID];
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
		//Inputmanager
		if(Input.GetKeyDown (KeyCode.Return)){			
			GoToScene ();
		}
		if (Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.D)) {
			NextMenuPlanet ();
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.A)) {
			PreviousMenuPlanet ();
		}
	}
	void NextMenuPlanet(){
		if (visiblePlanetID < menuPlanets.Count - 1) {
			SwitchPlanets (currentlyVisiblePlanet, menuPlanets [++visiblePlanetID], true);
		} 
	}
	void PreviousMenuPlanet(){
		if (visiblePlanetID > 0) {
			SwitchPlanets (currentlyVisiblePlanet, menuPlanets [--visiblePlanetID], false);
		} 
	}

	void GoToScene(){
		int sceneID;
		switch (visiblePlanetID) {
		case 0:
			sceneID = 1;
			break;
		case 1:
			//implement options menu
			return;
			break;
		default:
			return;
		}
		SceneManager.LoadScene (sceneID);
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
		SwitchPlanets (currentlyVisiblePlanet, menuPlanets[1], true);
	}
	public void GoToNewGame(){
		SwitchPlanets (currentlyVisiblePlanet, menuPlanets[0] , true);
	}

	IEnumerator FadeGameText(bool fadeIn){


		yield return null;
	}

	void SwitchPlanets(GameObject planetOut, GameObject planetIn, bool back = false){
		lerpIn = planetIn;
		lerpOut = planetOut;
		lerpRight = back;
		lerping = true;
		timeStartedLerping = Time.time;
		currentlyVisiblePlanet = lerpIn;

		//Temp
		menuNameText.text  = menuNameList [visiblePlanetID];
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
		}
		yield return null;
	}
}
