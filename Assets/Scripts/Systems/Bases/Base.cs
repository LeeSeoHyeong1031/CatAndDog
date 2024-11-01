using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Base : MonoBehaviour, ITakeDamage
{
    public int health;
    public int maxHealth;
    public bool dead { get; set; }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
    }
}
