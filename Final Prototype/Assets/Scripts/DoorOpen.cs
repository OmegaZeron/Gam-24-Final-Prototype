using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour {

	public int timeToOpenDoorInFrames = 120;
	public bool isLeft;
	private int framesPassed = 0;
	private bool openDoor = false;


	void Update () {
		if (openDoor == true && framesPassed < timeToOpenDoorInFrames) {
			if (isLeft == false) {
				transform.Translate (Vector3.right * Time.deltaTime);
			} else {
				transform.Translate (-Vector3.right * Time.deltaTime);
			}
			framesPassed++;
		}
	}

	public void OpenDoor () {
		openDoor = true;
	}
}