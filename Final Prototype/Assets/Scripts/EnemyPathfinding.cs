using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyPathfinding : MonoBehaviour {

	public Transform[] waypoints;
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
	public float errorDistance = 0.2f;

	void Start () {
		seeker = GetComponent<Seeker> ();
		rb = GetComponent<Rigidbody> ();
	}

	void OnPathComplete (Path p) {
		//Do nothing
	}

	void OnReachedDest () {
		if (i + 1 == waypoints.Length) {
			i = 0;
		} else {
			if (i < waypoints.Length) {
				i++;
			}
		}
	}

	void Update () {
		if (Time.time > lastRepath + repathRate && seeker.IsDone()) {
			lastRepath = Time.time;
			seeker.StartPath(transform.position, waypoints[i].position);
		}

		if (Vector3.Distance (transform.position, new Vector3 (waypoints[i].position.x, waypoints[i].position.y, waypoints[i].position.z)) <= errorDistance) {
			OnReachedDest ();
		}

		if (path == null) {
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
	}
}