using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAudioManager : MonoBehaviour {

	public AudioClip introMusic;
	public float introMusicLength;
	public AudioClip loopMusic;
	public float loopMusicLength;
	public AudioClip lowRumbling;
	public AudioClip asteroidShower;
	public AudioClip clank1;
	public AudioClip clank2;
	public AudioClip clank3;
	public GameObject musicObject;
	private AudioSource audio;

	void Start () {
		audio = musicObject.GetComponent<AudioSource> ();
		audio.PlayOneShot (introMusic, 0.1f);
		audio.PlayOneShot (lowRumbling, 2f);
		StartCoroutine (waitOnIntro());
		StartCoroutine (waitAsteroidShower());
		StartCoroutine (waitClank());
		StartCoroutine (waitRumble());
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