using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : IDamageable
{
    [SerializeField] private Door door;

    private Colors color;
    [SerializeField] private Colors.ColorChoice colorChoice;
    private Player player;
    [SerializeField] private float sightRange = 2.5f;
    [SerializeField] private float sightAngle = 30;
    private bool isSeeingPlayer = false;
    [SerializeField] private float detectionTime = 3;
    private float sightTime;

	void Start()
    {
        color = new Colors(colorChoice);
        currentHealth = maxHealth;
        player = GameManager.Instance.PlayerInstance;
	}

    private void Update()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 heading = playerPos - transform.position;
        float dot = Vector3.Dot(transform.forward, heading);
        if (dot > 0 && heading.magnitude < sightRange)
        {
            float angle = Vector3.Angle(transform.forward, heading);
            if (angle < sightAngle)
            {
                Debug.Log("Enemy detected the player");
                if (!isSeeingPlayer)
                {
                    sightTime = Time.time;
                }
                isSeeingPlayer = true;
            }
            else
            {
                isSeeingPlayer = false;
                sightTime = 0;
            }
        }
        else
        {
            isSeeingPlayer = false;
            sightTime = 0;
        }

        if (isSeeingPlayer)
        {
            if (Time.time - sightTime >= detectionTime)
            {
                Debug.Log("Enemy is aggroing the player");
            }
        }
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
