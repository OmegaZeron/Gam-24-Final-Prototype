using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IDamageable : MonoBehaviour
{
    protected int currentHealth;
    [SerializeField] protected int maxHealth;

    public void TakeDamage(int damage, Colors hitColor)
    {
        Debug.Log("Override this to use");
    }
}
