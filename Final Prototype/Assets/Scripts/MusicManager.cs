using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	public AudioClip introMusic;
	public float introMusicLength;
	public AudioClip loopMusic;
	public float loopMusicLength;
	public GameObject musicObject;
	private AudioSource audio;

	void Start () {
		audio = musicObject.GetComponent<AudioSource> ();
		audio.PlayOneShot (introMusic, 1f);
		StartCoroutine (waitOnIntro());
	}

	IEnumerator waitOnIntro () {
		yield return new WaitForSeconds (introMusicLength);
		audio.PlayOneShot (loopMusic, 1f);
		StartCoroutine (waitOnLoop());
	}

	IEnumerator waitOnLoop () {
		yield return new WaitForSeconds (loopMusicLength);
		audio.PlayOneShot (loopMusic, 1f);
		StartCoroutine (waitOnLoop());
	}
}