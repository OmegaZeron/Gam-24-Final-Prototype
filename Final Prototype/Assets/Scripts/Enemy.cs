using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : IDamageable
{
    [SerializeField] private Door door;

    private Colors color;
    [SerializeField] private Colors.ColorChoice colorChoice;

	void Start()
    {
        color = new Colors(colorChoice);
        currentHealth = maxHealth;
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
