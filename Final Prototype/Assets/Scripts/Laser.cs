using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
	public enum LaserColor
	{
		White,
		Allan,
		Please,
		Add,
		Colors
	}
	private LaserColor laserColor = LaserColor.White;

	private LineRenderer line;
	
	[SerializeField] private float laserDistance;
	[SerializeField] private float damage;

	private void Start()
	{
		line = GetComponent<LineRenderer>();
		line.SetPosition(0, transform.parent.position);
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit, laserDistance))
		{
			if (hit.collider)
			{
				line.SetPosition(1, hit.point);
				//check for bounce/color (create new prefab, go in new direction)
				//check for switch/enemy
			}
		}
		else
		{
			line.SetPosition(1, transform.parent.forward * laserDistance);
		}

		StartCoroutine(FadeLaser());
	}

	private IEnumerator FadeLaser()
	{
		while (line.startWidth > 0)
		{
			line.startWidth -= .2f * Time.deltaTime;
			line.endWidth -= .2f * Time.deltaTime;
			yield return null;
		}
		Destroy(transform.parent.gameObject);
	}
}
