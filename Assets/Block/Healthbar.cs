using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    public int maxHealth = 100;
    public int Health { get; private set; }

    public void takeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0) Health = 0;
    }
}
