using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {

	public AudioClip music;
	public float musicLength;
	private AudioSource source;

	public void ExitClick () {
		Application.Quit ();
	}

	public void PlayClick () {
		SceneManager.LoadScene (1);
	}

	void Start () {
		source = GetComponent<AudioSource>().GetComponent<AudioSource> ();
		source.PlayOneShot (music, 0.7f);
		StartCoroutine (waitOnMusic());
	}

	IEnumerator waitOnMusic () {
		yield return new WaitForSeconds (musicLength);
		source.PlayOneShot (music, 0.7f);
		StartCoroutine (waitOnMusic());
	}

}