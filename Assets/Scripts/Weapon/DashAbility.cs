using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : Weapon
{
    [Header("Dash Attributes")]

    public float speed = 1;

    ColliderListener listener;

    //Just the collider.  For now, it's a trigger and will attack everyone within it's cone.
    public Collider ourCollider; // This is to make explicit.  This object SHOULD be attached to the same GameObject as our MeleeWeapon script, as a child is fine too.
    public float modifier = 10000;
    public float activeTime;

    public void Register()
    {
        if (ourCollider.GetComponent<ColliderListener>() == null) ourCollider.gameObject.AddComponent<ColliderListener>();

        ColliderListener listener = ourCollider.GetComponent<ColliderListener>();
        listener.isWeapon = false;
        listener.dash = this;
    }

    // Use this for initialization
    void Start ()
    {
        Register();

        //weaponRenderer.enabled = false;
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
            //Debug.Log("Attacking with Melee.");
            canAttack = false;

            // We just want to flick our trigger on and off to capture anything within it.  Hopefully this is enough time but we'll see..
            ourCollider.enabled = true;
            StartCoroutine("WaitForTriggerToPopulate");

            GetComponentInParent<Rigidbody>().AddForce(transform.forward * speed * modifier);

            //weaponRenderer.enabled = true;
        }
    }

    IEnumerator WaitForTriggerToPopulate()
    {
        while (currentCooldown < activeTime)
        {
            yield return null;
        }

        Debug.Log("Disabling");

        CollisionActive(false);
    }
    
    public void CollisionActive(bool active)
    {
        ourCollider.enabled = active;
    }

    // These are called when our cone receives a collision
    public void ProcessCollision(Collision other)
    {
        // Ignore collision between other players or enemies.
        Debug.Log("Processing");

        // Damage other players or enemies
        /*if (base.belongsToPlayer && other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit Enemy");
            other.gameObject.GetComponent<EnemyController>().ApplyDamage(damage);

            CollisionActive(false);
        }
        else if (!base.belongsToPlayer && other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit Player");

            other.gameObject.GetComponent<PlayerController>().ApplyDamage(damage);

            CollisionActive(false);
        }*/
    }

    // These are called when our cone receives a trigger
    public void ProcessTrigger(Collider other)
    {
        // Ignore collision between other players or enemies.
        Debug.Log("Processing Trigger");

        if (base.belongsToPlayer && other.gameObject.CompareTag("Player")) Physics.IgnoreCollision(GetComponentInParent<PlayerController>().GetComponent<Collider>(), other);
        if (!base.belongsToPlayer && other.gameObject.CompareTag("Enemy")) Physics.IgnoreCollision(GetComponentInParent<EnemyController>().GetComponent<Collider>(), other);

        // Damage other players or enemies
        if (base.belongsToPlayer && other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit Enemy");
            other.gameObject.GetComponent<EnemyController>().ApplyDamage(damage);

            CollisionActive(false);
        }
        else if (!base.belongsToPlayer && other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit Player");

            other.gameObject.GetComponent<PlayerController>().ApplyDamage(damage);

            CollisionActive(false);
        }
    }
}
