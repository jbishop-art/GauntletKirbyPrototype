using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float damage = 5;

    public float speed;

    public float lifetime;
    private float currentLifetime = 0;

    private bool hasFired = false;

    [HideInInspector]public bool shotByPlayer = false;
    [HideInInspector]public Renderer theRenderer;

    // Use this for initialization
    void Start ()
    {
        // Ignore collision between bullets and other bullets
        Physics.IgnoreLayerCollision(11, 11);

        //if (shotByPlayer) Physics.IgnoreLayerCollision(11, 9);
        //else Physics.IgnoreLayerCollision(11, 10);
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (hasFired)
        {
            currentLifetime += Time.deltaTime;

            if (currentLifetime > lifetime) Kill();
        }
	}

    public void Fire(float theDamage)
    {
        // theDamage is the damage of our ranged weapon.
        damage = theDamage * damage;

        hasFired = true;

        GetComponent<Rigidbody>().AddForce(transform.forward * speed * 500);
    }

    public void SetIgnore(bool belongsToPlayer)
    {
        shotByPlayer = belongsToPlayer;
        //if (belongsToPlayer) Physics.IgnoreLayerCollision(11, 9);
        //else Physics.IgnoreLayerCollision(11, 10);
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 11) Physics.IgnoreCollision(GetComponent<Collider>(), collision);

        if (shotByPlayer && collision.gameObject.layer == 9)
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), collision);
            return;
        }
        else if (!shotByPlayer && collision.gameObject.layer == 10)
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), collision);
            return;
        }

        //Debug.Log("Processing Projectile Collision with: " + collision.gameObject.tag);

        if (collision.gameObject.CompareTag("Player") && !shotByPlayer)
        {
            Debug.Log("Hit Player");
            collision.gameObject.GetComponent<PlayerController>().ApplyDamage(damage);

            Kill();
        }
        else if (collision.gameObject.CompareTag("Enemy") && shotByPlayer)
        {
            Debug.Log("Hit Enemy");
            collision.gameObject.gameObject.GetComponent<EnemyController>().ApplyDamage(damage);

            Kill();
        }

        // For now, no matter what the bullet hits, it gets destroyed
        //if (hasFired) Kill();
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
