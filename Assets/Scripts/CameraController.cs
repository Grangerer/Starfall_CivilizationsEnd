using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Camera mainCamera;
	public int baseScrollspeed;
	private int scrollSpeed;
	// Use this for initialization
	void Start () {
		scrollSpeed = baseScrollspeed;
	}
	
	// Update is called once per frame
	void Update () {

		//perhaps increase scroll speed based on current cameraheight (y)
		if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)){
			mainCamera.transform.position = new Vector3 (mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z - scrollSpeed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)){
			mainCamera.transform.position = new Vector3 (mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z + scrollSpeed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)){
			mainCamera.transform.position = new Vector3 (mainCamera.transform.position.x + scrollSpeed * Time.deltaTime, mainCamera.transform.position.y, mainCamera.transform.position.z);
		}
		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)){
			mainCamera.transform.position = new Vector3 (mainCamera.transform.position.x - scrollSpeed * Time.deltaTime, mainCamera.transform.position.y, mainCamera.transform.position.z);
		}
		//Zoom in
		if (Input.GetAxis ("Mouse ScrollWheel") > 0f) {
			if (mainCamera.transform.position.y > 4) {
				mainCamera.transform.position = new Vector3 (mainCamera.transform.position.x, mainCamera.transform.position.y - 1, mainCamera.transform.position.z);
				scrollSpeed = baseScrollspeed + (int)mainCamera.transform.position.y / 2;
			}
		} 
		//Zoom out
		if(Input.GetAxis ("Mouse ScrollWheel") < 0f) {
			if (mainCamera.transform.position.y < 20) {
				mainCamera.transform.position = new Vector3 (mainCamera.transform.position.x, mainCamera.transform.position.y + 1, mainCamera.transform.position.z);
				scrollSpeed = baseScrollspeed + (int)mainCamera.transform.position.y / 2;
			}
		}
	}
}
