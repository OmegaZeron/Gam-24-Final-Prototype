﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
	[SerializeField] private GameObject laserPrefab;

	private LineRenderer line;
	private Colors color = new Colors(Color.white);

	[SerializeField] private float laserDistance;
	[SerializeField] private float fadeSpeed;
	[SerializeField] private int damage;

	private void Start()
	{
		line = GetComponent<LineRenderer>();
		line.SetPosition(0, transform.position);

		RaycastHit hit;
		if (Physics.Raycast(line.GetPosition(0), transform.forward, out hit, laserDistance))
		{
			if (hit.collider)
			{
				line.SetPosition(1, hit.point);
				//check for bounce/color
				if (hit.collider.GetComponent<IBounceable>() != null)
				{

					float distance = laserDistance - Vector3.Distance(line.GetPosition(0), line.GetPosition(1));
					Vector3 heading = line.GetPosition(1) - line.GetPosition(0);
					Vector3 bounceAngle = Vector3.Reflect(heading.normalized, hit.normal);

					Laser newLaser = Instantiate(laserPrefab, hit.point, Quaternion.identity).GetComponent<Laser>();
					newLaser.transform.forward = bounceAngle;
					newLaser.laserDistance = distance;

					if (hit.collider.GetComponent<IColorable> () != null) {
						newLaser.color = color + hit.collider.GetComponent<IColorable> ().GetColor ();
					} else {
						newLaser.color = color;
					}
				}
				//check for switch/enemy
				Switch switchh = hit.collider.GetComponent <Switch> ();
				if (switchh != null) {
					switchh.TakeDamage (damage, color);
				} else {
					Enemy enemy = hit.collider.GetComponentInParent <Enemy> ();
					if (enemy != null) {
						enemy.TakeDamage (damage, color);
					}
					else
					{
						Player player = hit.collider.GetComponentInParent<Player>();
						if (player != null)
						{
							player.TakeDamage(damage, color);
						}
					}
				}
			}
		}
		else
		{
			line.SetPosition(1, transform.position + (transform.forward * laserDistance));
		}

		StartCoroutine(FadeLaser());
	}

	private void Update()
	{
		line.startColor = line.endColor = color.color;
	}

	private IEnumerator Test()
	{
		yield return new WaitForSeconds(1);
		color += new Colors(Color.red);
		yield return new WaitForSeconds(1);
		color += new Colors(Color.green);
		yield return new WaitForSeconds(1);
		color += new Colors(Color.blue);
	}

	private IEnumerator FadeLaser()
	{
		while (line.startWidth > 0)
		{
			line.startWidth -= fadeSpeed * Time.deltaTime;
			line.endWidth -= fadeSpeed * Time.deltaTime;
			yield return null;
		}
		Destroy(gameObject);
	}
}
