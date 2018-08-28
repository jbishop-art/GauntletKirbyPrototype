using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //public Weapon ourWeapon; // Also here for now.  Should be moved to PlayerController.

    public bool belongsToPlayer; // Does this weapon belong to a player?
    public float damage;

    public float cooldown = 0;
    public float currentCooldown = 0; // Once this is equal to cooldown we can attack again.

    public bool canAttack = true;

    //For AI
    [Header("AI Attributes")]
    public float attackDistance; // For AI this is how close they need to be to attack with their given weapon/ability

    public float chargeTime; // Upon initiating an attack, how long before the attack actually executes.  AI only.
    public float currentChargeTime;
    public bool charged = true;


	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	public virtual void Update ()
    {
        if (!charged)
        {
            currentChargeTime += Time.deltaTime;

            if (currentChargeTime > chargeTime)
            {
                currentChargeTime = 0;
                charged = true;

                Attack();
            }
        }

        if (canAttack == false)
        {
            currentCooldown += Time.deltaTime;

            if (currentCooldown > cooldown)
            {
                currentCooldown = 0;
                canAttack = true;
            }
        }

        // Here for now.. This will live inside the PlayerController script and will be called when the appropriate button is pressed.  When in the PlayerController script, this will be ourWeapon.Attack();  Not just Attack();
        if (Input.GetKey(KeyCode.Mouse0)) // If we change to GetKeyDown, we have to click each time to attack.  If not, we can just attack by holding down the mousebutton
        {
            Attack();
        }
    }

    public void DelayedAttack()
    {
        if (canAttack) charged = false;
    }

    public virtual void Attack()
    {

    }
}
