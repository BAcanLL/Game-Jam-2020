using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    private static Color RED_HEALTH_COLOR = new Color(231 / 255f, 117 / 255f, 117 / 255f);
    private static Color GREEN_HEALTH_COLOR = new Color(98 / 255f, 204 / 255f, 86 / 255f);
    private static Color GREY_HEALTH_COLOR = new Color(200 / 255f, 200 / 255f, 200 / 255f);

    public float maxHealth = 100;
    public float Health { get; private set; }
    public bool disabled = false;

    private GameObject healthbarObject;
    private GameObject healthbarGreenHealth;

    void Start()
    {
        Health = maxHealth;

        // Create a healthbar object
        healthbarObject = Instantiate(Resources.Load<GameObject>("Healthbar"), gameObject.transform);
        healthbarObject.layer = LayerMask.NameToLayer("UI");
        healthbarGreenHealth = healthbarObject.transform.GetChild(0).GetChild(0).gameObject;

        // Move the healthbar to the correct height
        healthbarObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 0.6f, 0);
    }

    void Update()
    {
        healthbarGreenHealth.transform.localScale = new Vector3(Health/maxHealth, 1, 1);

        if (disabled)
        {
            healthbarGreenHealth.GetComponent<SpriteRenderer>().color = GREY_HEALTH_COLOR;
        } else
        {
            healthbarGreenHealth.GetComponent<SpriteRenderer>().color = GREEN_HEALTH_COLOR;
        }
    }

    public bool takeDamage(float value) // Returns TRUE if damages causes health to reach zero
    {
        Health -= value;
        // Debug.Log("Remaining Health: " + Health);

        if (Health <= 0)
        {
            Health = 0;
            return true;
        }

        return false;
    }

    public bool heal(float value) // Returns TRUE if healing causes health to be full
    {
        Health += value;

        if (Health >= maxHealth)
        {
            Health = maxHealth;
            return true;
        }

        return false;
    }

    public bool fullHeal()
    {
        return heal(maxHealth);
    }
}
