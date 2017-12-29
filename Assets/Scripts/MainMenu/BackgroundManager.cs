using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
	[SerializeField]
	GameObject planet;
	bool rotating = false;

	public float degreePerSecond;
	// Use this for initialization
	void Start ()
	{
		//Spawn Random Planet

	}
	
	// Update is called once per frame
	void Update ()
	{
		//Rotate Planet
		if(!rotating){
			//Debug.Log("Starting Rotational Process");
			rotating = true;
			StartCoroutine(RotateObject (planet));
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
		
}
