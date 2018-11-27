using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamageable
{
    private int currentHealth;
    [SerializeField] private int maxHealth;

	void Start()
    {
        currentHealth = maxHealth;
	}

    public void TakeDamage(int damage, Colors lol)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Debug.Log("Player died, resetting scene");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
