using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour, IDamageable
{
    private int currentHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private Colors color;

    [SerializeField] private Door door;

    void Start()
    {
        currentHealth = maxHealth;
	}

    public void TakeDamage(int damage, Colors hitColor)
    {
        if (currentHealth > 0 && color.Equals(hitColor))
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                door.OpenLock(this);
                Debug.LogWarning("I've been hit but there's no animation yet");
            }
        }
    }
}
