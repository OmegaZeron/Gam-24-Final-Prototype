using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
	// [SerializeField] private GameObject laserPrefab;
	[SerializeField] private List<AudioClip> clips;

	private void Start()
	{
		
	}
	
	private void Update()
	{
		
	}

	private void Fire()
	{
		// Laser laser = Instantiate(laserPrefab, transform.position, transform.rotation).GetComponent<Laser>();
		int rand = Random.Range(0, clips.Count);
		AudioSource.PlayClipAtPoint(clips[rand], transform.position);
	}
}
