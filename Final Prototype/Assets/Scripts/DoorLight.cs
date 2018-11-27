using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLight : MonoBehaviour {

	public GameObject redLight;
	public GameObject greenLight;

	public void On () {
		redLight.SetActive (false);
		greenLight.SetActive (true);
	}

}