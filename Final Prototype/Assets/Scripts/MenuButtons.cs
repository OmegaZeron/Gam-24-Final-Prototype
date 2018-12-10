using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {

	public void ExitClick () {
		Application.Quit ();
	}

	public void PlayClick () {
		SceneManager.LoadScene (1);
	}

}