using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public AudioClip introMusic;
	public float introMusicLength;
	public AudioClip loopMusic;
	public float loopMusicLength;
	public AudioClip lowRumbling;
	public AudioClip asteroidShower;
	public AudioClip clank1;
	public AudioClip clank2;
	public AudioClip clank3;
	public AudioClip laser1;
	public GameObject musicObject;
	private AudioSource audio;
	private bool canPlay = true;

	void Start () {
		audio = musicObject.GetComponent<AudioSource> ();
		audio.PlayOneShot (introMusic, 0.4f);
		audio.PlayOneShot (lowRumbling, 2f);
		StartCoroutine (waitOnIntro());
		StartCoroutine (waitAsteroidShower());
		StartCoroutine (waitClank());
		StartCoroutine (waitRumble());
	}

	void Update () {
		if (Input.GetMouseButtonDown (0) && canPlay == true) {
			audio.PlayOneShot (laser1, 1f);
			canPlay = false;
			StartCoroutine (waitPlay ());
		}
	}

	IEnumerator waitPlay () {
		yield return new WaitForSeconds (0.5f);
		canPlay = true;
	}

	IEnumerator waitOnIntro () {
		yield return new WaitForSeconds (introMusicLength);
		audio.PlayOneShot (loopMusic, 0.1f);
		StartCoroutine (waitOnLoop());
	}

	IEnumerator waitOnLoop () {
		yield return new WaitForSeconds (loopMusicLength);
		audio.PlayOneShot (loopMusic, 0.1f);
		StartCoroutine (waitOnLoop());
	}

	IEnumerator waitAsteroidShower () {
		yield return new WaitForSeconds (Random.Range (0f, 120f));
		audio.PlayOneShot (asteroidShower, 0.2f);
		yield return new WaitForSeconds (32f);
		StartCoroutine (waitAsteroidShower());
	}

	IEnumerator waitRumble () {
		yield return new WaitForSeconds (31.956f);
		audio.PlayOneShot (lowRumbling, 2f);
		StartCoroutine (waitRumble());
	}

	IEnumerator waitClank () {
		yield return new WaitForSeconds (Random.Range (0f, 32f));
		switch (Random.Range (1, 4)) {
		case 1:
			audio.PlayOneShot (clank1, 0.3f);
			break;
		case 2:
			audio.PlayOneShot (clank2, 0.3f);
			break;
		case 3:
			audio.PlayOneShot (clank3, 0.3f);
			break;
		}
		StartCoroutine (waitClank());
	}
}