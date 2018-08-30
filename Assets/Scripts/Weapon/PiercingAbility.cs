using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingAbility : Weapon
{
    [Header("Ranged Attributes")]
    public Transform locator; // Where to fire our projectile from.  Z is forward
    public GameObject projectile; // A GameObject WITH a Projectile Component
    private Vector3 originalPosition;
    private Quaternion originalRotation;

	// Use this for initialization
	void Start ()
    {

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
            Shoot();

            canAttack = false;
        }
    }

    private void Shoot()
    {
        GameObject newProjectile = Instantiate(projectile, locator.position, locator.rotation, null);
        newProjectile.GetComponent<Projectile>().SetIgnore(base.belongsToPlayer);
        newProjectile.GetComponent<Projectile>().Fire(damage);
    }
}
