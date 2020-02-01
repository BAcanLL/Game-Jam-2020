using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    private static Color RED_HEALTH_COLOR = new Color(231, 117, 117);
    private static Color GREEN_HEALTH_COLOR = new Color(98, 204, 86);
    private static Color GREY_HEALTH_COLOR = new Color(145, 145, 145);

    public float maxHealth = 100;
    public float Health { get; private set; }

    private GameObject healthbarObject;
    private GameObject healthbarGreenHealth;

    void Start()
    {
        Health = maxHealth;

        // Create a healthbar object
        healthbarObject = Instantiate(Resources.Load<GameObject>("Healthbar"), gameObject.transform);
        healthbarGreenHealth = healthbarObject.transform.GetChild(0).GetChild(0).gameObject;

        // Move the healthbar to the correct height
        healthbarObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 0.6f, 0);
    }

    void Update()
    {
        healthbarGreenHealth.transform.localScale = new Vector3(Health/maxHealth, 1, 1);
    }

    public bool takeDamage(float value) // Returns TRUE if damages causes health to reach zero
    {
        Health -= value;
        Debug.Log("Remaining Health: " + Health);

        if (Health <= 0)
        {
            Health = 0;
            return true;
        }

        return false;
    }

    public void heal(float value)
    {
        Health += value;

        if (Health >= maxHealth)
        {
            Health = maxHealth;
        }
    }

    public void fullHeal()
    {
        heal(maxHealth);
    }
}
