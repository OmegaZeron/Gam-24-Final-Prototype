using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    private int currentHealth;
    [SerializeField] private int maxHealth;

    [SerializeField] private Door door;

    [SerializeField] private Colors color;

	void Start()
    {
        currentHealth = maxHealth;
	}
	
	void Update()
    {
		
	}

    public void TakeDamage(int damage, Colors hitColor)
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
