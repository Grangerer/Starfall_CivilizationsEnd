using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Camera mainCamera;
	public int baseMoveSpeed;
	private int moveSpeed;
	public int baseScrollspeed;

	void Start(){
		moveSpeed = baseMoveSpeed + (int)mainCamera.transform.position.y / 2;
	}
	// Update is called once per frame
	void Update () {

		//perhaps increase scroll speed based on current cameraheight (y)
		if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)){
			mainCamera.transform.position = new Vector3 (mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z - moveSpeed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)){
			mainCamera.transform.position = new Vector3 (mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z + moveSpeed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)){
			mainCamera.transform.position = new Vector3 (mainCamera.transform.position.x + moveSpeed * Time.deltaTime, mainCamera.transform.position.y, mainCamera.transform.position.z);
		}
		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)){
			mainCamera.transform.position = new Vector3 (mainCamera.transform.position.x - moveSpeed * Time.deltaTime, mainCamera.transform.position.y, mainCamera.transform.position.z);
		}
		//Zoom in
		if (Input.GetAxis ("Mouse ScrollWheel") > 0f) {
			if (mainCamera.transform.position.y > 4 + baseScrollspeed) {
				mainCamera.transform.position = new Vector3 (mainCamera.transform.position.x, mainCamera.transform.position.y - baseScrollspeed, mainCamera.transform.position.z);
			} else {
				mainCamera.transform.position = new Vector3 (mainCamera.transform.position.x, 4, mainCamera.transform.position.z);
			}
			moveSpeed = baseMoveSpeed + (int)mainCamera.transform.position.y / 2;
		} 
		//Zoom out
		if(Input.GetAxis ("Mouse ScrollWheel") < 0f) {
			if (mainCamera.transform.position.y < 200 - baseScrollspeed) {
				mainCamera.transform.position = new Vector3 (mainCamera.transform.position.x, mainCamera.transform.position.y + baseScrollspeed, mainCamera.transform.position.z);
			} else {
				mainCamera.transform.position = new Vector3 (mainCamera.transform.position.x, 200, mainCamera.transform.position.z);
			}
			moveSpeed = baseMoveSpeed + (int)mainCamera.transform.position.y / 2;
		}
	}
}
