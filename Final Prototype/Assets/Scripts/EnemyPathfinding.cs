using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyPathfinding : MonoBehaviour {

	public Transform[] waypoints;
	public Transform testPoint;
	public Rigidbody rb;
	private int i = 0;
	private Seeker seeker;
	public Path path;
	public float speed = 2;
	public float nextWaypointDistance = 1;
	private int currentWaypoint = 0;
	public float repathRate = 0.5f;
	private float lastRepath = float.NegativeInfinity;
	public bool reachedEndOfPath;

	void Start () {
		seeker = GetComponent<Seeker> ();
		rb = GetComponent<Rigidbody> ();
	}

	void OnPathComplete (Path p) {
		/*if (i < waypoints.Length) {
			i++;
		}*/
	}

	void Update () {
		if (Time.time > lastRepath + repathRate && seeker.IsDone()) {
			lastRepath = Time.time;
			seeker.StartPath(transform.position, testPoint.position, OnPathComplete);
		}
		if (path == null) {
			Debug.Log (i);
			return;
		}
			
		reachedEndOfPath = false;

		float distanceToWaypoint;
		while (true) {
			distanceToWaypoint = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
			if (distanceToWaypoint < nextWaypointDistance) {
				if (currentWaypoint + 1 < path.vectorPath.Count) {
					currentWaypoint++;
				} else {
					reachedEndOfPath = true;
					break;
				}
			} else {
				break;
			}
		}
		var speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint/nextWaypointDistance) : 1f;
		Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
		Vector3 velocity = dir * speed * speedFactor;
		//Move rb.AddForce ();
	}
}