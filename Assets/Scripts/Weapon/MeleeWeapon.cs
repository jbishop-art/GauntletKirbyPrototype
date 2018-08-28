using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    ColliderListener listener;

    // These two will probably be replaced.  Our actual game will use sprites and not an actual 3d mesh.
    public GameObject weaponToSwing; // This is just our joint.  No collision.  At least for now
    public SkinnedMeshRenderer weaponRenderer; // This is just the renderer.

    public float swingSpeed = 1; // How long to swing from one side to the other.

    public bool attackRightToLeft;
    public float min = 20; // Angle we're at when left
    public float max = 160; // Angle we're at when right

    //Just the collider.  For now, it's a trigger and will attack everyone within it's cone.
    public Collider ourCone; // This is to make explicit.  This object SHOULD be attached to the same GameObject as our MeleeWeapon script, as a child is fine too.

    public enum AttackType
    {
        Left,
        Right,
        Alternate
    }

    public AttackType theAttackType; // The type of attack for our melee weapon.  Can be expanded later to 
    private AttackType currentAttack;
    private bool alternate;

    public void Register()
    {
        if (ourCone.GetComponent<ColliderListener>() == null) ourCone.gameObject.AddComponent<ColliderListener>();

        ColliderListener listener = ourCone.GetComponent<ColliderListener>();
        listener.ourWeapon = this;
    }

    // Use this for initialization
    void Start ()
    {
        Register();

        //weaponRenderer.enabled = false;

		if (theAttackType == AttackType.Left || theAttackType == AttackType.Right)
        {
            currentAttack = theAttackType;

            alternate = false;
        }
        else
        {
            currentAttack = AttackType.Left;

            alternate = true;
        }

        if (currentAttack == AttackType.Left)
        {
            weaponToSwing.transform.localRotation = Quaternion.Euler(weaponToSwing.transform.localRotation.x, min, weaponToSwing.transform.localRotation.z);
            attackRightToLeft = true;
        }
        else
        {
            weaponToSwing.transform.localRotation = Quaternion.Euler(weaponToSwing.transform.localRotation.x, max, weaponToSwing.transform.localRotation.z);
            attackRightToLeft = false;
        }
    }

	// Update is called once per frame
	public override void Update ()
    {
        base.Update();

        if (canAttack == false)
        {
            // These two lines are messy and should be changed later
            //if (currentCooldown > (cooldown * 0.01f)) ourCone.enabled = false;
            //if (currentCooldown > swingSpeed) weaponRenderer.enabled = false;

            if (attackRightToLeft)
            {
                float result = Mathf.Lerp(min, max, currentCooldown / swingSpeed);

                weaponToSwing.transform.localRotation = Quaternion.Euler(weaponToSwing.transform.localRotation.x, result, weaponToSwing.transform.localRotation.z);

                Debug.Log("Attacking Left to Right.");
            }
            else
            {
                float result = Mathf.Lerp(max, min, currentCooldown / swingSpeed);

                weaponToSwing.transform.localRotation = Quaternion.Euler(weaponToSwing.transform.localRotation.x, result, weaponToSwing.transform.localRotation.z);

                Debug.Log("Attacking Right to Left.");
            }
        }
	}

    public override void Attack()
    {
        base.Attack();

        if (canAttack)
        {
            Debug.Log("Attacking with Melee.");

            ExecuteAttack();

            canAttack = false;

            // We just want to flick our trigger on and off to capture anything within it.  Hopefully this is enough time but we'll see..
            ourCone.enabled = true;
            StartCoroutine("WaitForTriggerToPopulate");

            //weaponRenderer.enabled = true;
            
        }
    }

    IEnumerator WaitForTriggerToPopulate()
    {
        while (currentCooldown < cooldown/2)
        {
            yield return null;
        }

        Debug.Log("Disabling");

        ourCone.enabled = false;
    }
    private void ExecuteAttack()
    {
        if (alternate)
        {
            if (currentAttack == AttackType.Left) currentAttack = AttackType.Right;
            else if (currentAttack == AttackType.Right) currentAttack = AttackType.Left;
        }

        if (currentAttack == AttackType.Left)
        {
            AttackLeftToRight();
        }
        else if (currentAttack == AttackType.Right)
        {
            AttackRightToLeft();
        }
    }

    private void AttackRightToLeft()
    {
        attackRightToLeft = true;
    }

    private void AttackLeftToRight()
    {
        attackRightToLeft = false;
    }

    // These are called when our cone receives a collision
    public void ProcessCollision(Collision other)
    {
        if (base.belongsToPlayer && other.gameObject.CompareTag("Enemy"))
        {
            //other.gameObject.GetComponent<EnemyController>().ApplyDamage(damage);
        }
        else if (!base.belongsToPlayer && other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().ApplyDamage(damage);
        }
    }

    // These are called when our cone receives a trigger
    public void ProcessTrigger(Collider other)
    {
        Debug.Log("Entered Trigger");

        if (base.belongsToPlayer && other.CompareTag("Enemy"))
        {
            //other.GetComponent<EnemyController>().ApplyDamage(damage);
        }
        else if (!base.belongsToPlayer && other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().ApplyDamage(damage);
        }
    }
}
