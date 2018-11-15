using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
	[SerializeField] private GameObject laserPrefab;

	private LineRenderer line;
	private Colors color = new Colors(Color.white);

	[SerializeField] private float laserDistance;
	[SerializeField] private int damage;

	private Vector3 heading;
	private Vector3 bounceAngle;
	private RaycastHit hit;

	private void Awake()
	{
		line = GetComponent<LineRenderer>();
		line.SetPosition(0, transform.position);

		if (Physics.Raycast(transform.position, transform.forward, out hit, laserDistance))
		{
			if (hit.collider)
			{
				line.SetPosition(1, hit.point);
				//check for bounce/color
				if (hit.collider.GetComponent<IBounceable>() != null)
				{

					Debug.Log(hit.point);
					// float distance = laserDistance - Vector3.Distance(line.GetPosition(0), line.GetPosition(1));
					heading = line.GetPosition(1) - line.GetPosition(0);
					bounceAngle = Vector3.Reflect(heading.normalized, hit.normal);
					Debug.Log(bounceAngle);

					// Laser newLaser = Instantiate(laserPrefab, hit.point, Quaternion.identity).GetComponentInChildren<Laser>();
					// newLaser.transform.parent.forward = bounceAngle;
					// newLaser.transform.parent.position = newLaser.transform.parent.position + (newLaser.transform.parent.forward * .05f); // shouldn't need this
					// newLaser.laserDistance = distance;

					if (hit.collider.GetComponent<IColorable>() != null)
					{
						// newLaser.color = color + hit.collider.GetComponent<IColorable>().GetColor();
					}
				}
				//check for switch/enemy
				if (hit.collider.GetComponent<IDamageable>() != null)
				{
					hit.collider.GetComponent<IDamageable>().TakeDamage(damage);
				}
			}
		}
		else
		{
			line.SetPosition(1, transform.forward * laserDistance);
		}

		// StartCoroutine(FadeLaser());
	}

	private void Update()
	{
		// line.startColor = line.endColor = color.color;
		Debug.DrawRay(hit.point, bounceAngle * 4, Color.red);
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
			line.startWidth -= .1f * Time.deltaTime;
			line.endWidth -= .1f * Time.deltaTime;
			yield return null;
		}
		Destroy(transform.parent.gameObject);
	}
}
