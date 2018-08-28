using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    public Transform locator; // Where to fire our projectile from.  Z is forward
    public GameObject projectile; // A GameObject WITH a Projectile Component
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    //public Vector3 localOffset;
	// Use this for initialization
	void Start ()
    {
        // Let the projectile know whether this is a good or bad weapon.
        projectile.GetComponent<Projectile>().shotByPlayer = base.belongsToPlayer;
    }

    // Update is called once per frame
    public override void Update ()
    {
        base.Update();
	}

    public override void Attack()
    {
        base.Attack();

        if (canAttack)
        {
            Debug.Log("Attacking with RANGED.");

            Shoot();

            canAttack = false;
        }
    }

    private void Shoot()
    {
        GameObject newProjectile = Instantiate(projectile, locator.position, locator.rotation, null);
        newProjectile.GetComponent<Projectile>().Fire(damage);
    }
}
