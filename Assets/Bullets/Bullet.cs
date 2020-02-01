using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float speed;
    public float range;

    public Vector3 direction;
    float distanceTravelled;

    // Start is called before the first frame update
    void Start()
    {
        distanceTravelled = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        Vector3 positionDelta = direction * speed * Time.fixedDeltaTime;
        distanceTravelled += positionDelta.magnitude;
        transform.position += positionDelta;

        if (distanceTravelled >= range)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
    }

    public void SetDirection(Vector3 rotation)
    {
        direction = Utilities.ConvertRotationToDirection(rotation);
    }

    public void SetDirection(Quaternion rotation)
    {
        direction = Utilities.ConvertRotationToDirection(rotation);
    }
}
