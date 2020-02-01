using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BulletSpawner : MonoBehaviour
{
    public GameObject bullet;
    public float bulletSpeed;
    public float range;
    public int bulletDamage;
    public float cooldown;
    
    float cooldownExpireTime;
    Vector3 rotation;
    bool isFiring;


    // Start is called before the first frame update
    void Start()
    {
        isFiring = false;
        cooldownExpireTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0)
        {
            cooldownExpireTime += Time.deltaTime;
        }
        if (isFiring && cooldownExpireTime >= cooldown)
        {
            cooldownExpireTime -= cooldown;
            FireBullet();
        }
    }

    void FixedUpdate()
    {
        
    }

    public void SetFiring(bool fire)
    {
        isFiring = fire;
    }

    public void SetRotation(Vector3 rotation)
    {
        this.rotation = rotation;
    }

    public void Rotate(Vector3 rotation)
    {
        this.rotation += rotation;
    }

    private void FireBullet()
    {
        GameObject g = Instantiate(bullet);
        Bullet b = g.GetComponent<Bullet>();
        b.transform.position = transform.position;
        b.transform.rotation = transform.rotation;
        b.speed = bulletSpeed;
        b.damage = bulletDamage;
        b.range = range;
        b.SetDirection(rotation);
    }
}
