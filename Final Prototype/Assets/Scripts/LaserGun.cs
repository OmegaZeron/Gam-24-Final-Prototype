using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
	[SerializeField] private GameObject laserPrefab;
	[SerializeField] private Transform firePoint;
	[SerializeField] private float cooldown;
	private bool canFire = true;
	
	//[SerializeField] private List<AudioClip> audioClips;

	private void Start()
	{
		
	}
	
	private void Update()
	{
		if (Input.GetButtonDown("Fire1") && canFire)
		{
			canFire = false;
			StartCoroutine(CountDown());
			Fire();
		}
	}

	private void Fire()
	{
		/*Laser laser = */Instantiate(laserPrefab, firePoint.position, firePoint.rotation).GetComponent<Laser>();
		//int rand = Random.Range(0, audioClips.Count);
		//AudioSource.PlayClipAtPoint(audioClips[rand], transform.position);
	}

	private IEnumerator CountDown()
	{
		yield return new WaitForSeconds(cooldown);
		canFire = true;
	}
}
