using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : IDamageable
{
    [SerializeField] private Door door;

    private Colors color;
    [SerializeField] private Colors.ColorChoice colorChoice;
    public Player player;
    [SerializeField] private float sightRange = 2.5f;
    [SerializeField] private float sightAngle = 30;
    [SerializeField] private bool isSeeingPlayer = false;
    [SerializeField] private float detectionTime = 3;
    private float sightTime;
	private EnemyPathfinding pathFinding;
	private bool engaged = false;
	[SerializeField] private GameObject laserPrefab;
	[SerializeField] private Transform firePoint;
	private float fireCooldownTimer = 0;
	[SerializeField] private float fireCooldown;
	private float searchCountdownTime;
	[SerializeField] private float searchCountdown;
	private bool startSearching = false;

	public enum AIMODE
	{
		None,
		Patrolling,
		Engaged,
		Searching
	}
	private AIMODE mode = AIMODE.Patrolling;
	public AIMODE Mode { get { return mode; } }

	void Start()
    {
        color = new Colors(colorChoice);
        currentHealth = maxHealth;
        player = GameManager.Instance.PlayerInstance;
		pathFinding = GetComponent<EnemyPathfinding>();
	}

	private void Update()
	{
		Vector3 playerPos = player.transform.position;
		Vector3 heading = playerPos - transform.position;
		float dot = Vector3.Dot(transform.forward, heading);
		float angle = Vector3.Angle(transform.forward, heading);


		if (mode == AIMODE.Patrolling)
		{

			if (dot > 0 && heading.magnitude < sightRange)
			{
				RaycastHit hit;
				if (angle < sightAngle && Physics.Raycast(transform.position, playerPos, out hit))
				{
					if (hit.collider.tag == "Player")
					{
						Debug.Log("Enemy detected the player");
						if (!isSeeingPlayer)
						{
							sightTime = Time.time;
						}
						isSeeingPlayer = true;
					}
				}
				else
				{
					isSeeingPlayer = false;
					engaged = false;
					sightTime = 0;
				}
			}
			else
			{
				isSeeingPlayer = false;
				engaged = false;
				sightTime = 0;
			}

			if (isSeeingPlayer)
			{
				if (Time.time - sightTime >= detectionTime)
				{
					mode = AIMODE.Engaged;
				}
			}
		}

		else if (mode == AIMODE.Engaged)
		{
			RaycastHit hit;
			if (!engaged)
			{
				engaged = true;
				StartCoroutine(FireAtPlayer());
			}
			else if (heading.magnitude > sightRange || angle > sightAngle || (!Physics.Raycast(transform.position, playerPos, out hit) || hit.collider.tag != "Player"))
			{
				mode = AIMODE.Searching;
				isSeeingPlayer = false;
				sightTime = 0;
				engaged = false;
			}
		}
		
		else if (mode == AIMODE.Searching)
		{
			if (!startSearching)
			{
				startSearching = true;
				searchCountdownTime = Time.time;
			}
			else if (dot > 0 && heading.magnitude < sightRange)
			{
				RaycastHit hit;
				if (angle < sightAngle && Physics.Raycast(transform.position, playerPos, out hit))
				{
					if (hit.collider.tag == "Player")
					{
						startSearching = false;
						searchCountdownTime = 0;
						mode = AIMODE.Engaged;
						isSeeingPlayer = true;
					}
				}
				else if (Time.time - searchCountdownTime > searchCountdown)
				{
					mode = AIMODE.Patrolling;
					startSearching = false;
					searchCountdownTime = 0;

				}
			}
		}
	}

	private IEnumerator FireAtPlayer()
	{
		while (mode == AIMODE.Engaged)
		{
			Vector3 temp = player.transform.position;
			temp.y = 0;
			transform.LookAt(temp);
			Vector3 heading = player.transform.position - transform.position;
			heading.y = 0;
			Quaternion rotateTo = Quaternion.LookRotation(heading);
			pathFinding.GetRotInfo(rotateTo);

			if (Vector3.Angle(transform.forward, heading) < sightAngle)
			{
				if (Time.time - fireCooldownTimer >= fireCooldown)
				{
					Fire();
				}
			}
			yield return null;
		}
	}
	private void Fire()
	{
		Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
		fireCooldownTimer = Time.time;
	}

    public new void TakeDamage(int damage, Colors hitColor)
    {
        if (color.Equals(hitColor))
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                if (door)
                {
                    door.OpenLock(this);
                }
                Die();
            }
        }
    }

    private void Die()
    {
        Debug.LogWarning("Someone kill me (add an animation or something)");
        gameObject.SetActive(false);
    }
}
