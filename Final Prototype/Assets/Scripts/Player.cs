using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : IDamageable
{
	void Start()
    {
        currentHealth = maxHealth;
	}

    public new void TakeDamage(int damage, Colors lol)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Debug.Log("Player died, resetting scene");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
