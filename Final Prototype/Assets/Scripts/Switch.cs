﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : IDamageable
{
    private Colors color;
    [SerializeField] private Colors.ColorChoice colorChoice;

    [SerializeField] private Door door;

	public DoorLight doorLight;

    void Start()
    {
        color = new Colors(colorChoice);
        currentHealth = maxHealth;
	}

    public new void TakeDamage(int damage, Colors hitColor)
    {
        if (currentHealth > 0 && color.Equals(hitColor))
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
				doorLight.On ();
                door.OpenLock(this);
                Debug.LogWarning("I've been hit but there's no animation yet");
            }
        }
    }
}
