using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject ability;

    public float health = 10;

    public bool engaging = false;
    public float maxDistanceToUnfollow = 40;
    public GameObject engagedPlayer;

    public List<GameObject> engagedPlayers;

    public Weapon ourWeapon;
    private float maxDistanceDelta;
    public float moveSpeed = 10;
    private float distance;

	// Use this for initialization
	void Start ()
    {
        GameObject spawnedAbility = Instantiate(ability, transform.position, Quaternion.identity);

        spawnedAbility.transform.parent = gameObject.transform;

        spawnedAbility.GetComponent<Weapon>().belongsToPlayer = false;
        spawnedAbility.GetComponent<Weapon>().charging = false;

        ability = spawnedAbility;

        engagedPlayers = new List<GameObject>();

        maxDistanceDelta = moveSpeed / 50;
        //Physics.IgnoreLayerCollision(10, 10);	
    }

    // Update is called once per frame
    void Update ()
    {
        if (engaging) Engage();
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            engagedPlayer = other.gameObject;

            if (!engagedPlayers.Contains(other.gameObject)) engagedPlayers.Add(other.gameObject);

            engaging = true;        
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            engagedPlayers.Remove(other.gameObject);

            if (engagedPlayers.Count == 0) engaging = false;
        }
    }

    public void Engage()
    {
        // Not using right now.  If players are close the enemy will flip.
        //engagedPlayer = FindClosestPlayer();

        if (engagedPlayer != null)
        {
            //AttemptAbilityAttack();
            //AttemptRegularAttack();

            //bool canAttack = AttemptAbilityAttack();
            //if (canAttack == false) AttemptRegularAttack();


            // Rotate Towards our engaged player
            transform.LookAt(engagedPlayer.transform);

            distance = Vector3.Distance(transform.position, engagedPlayer.transform.position);

            // Determine whether we attack or move forward
            if (distance <= ourWeapon.attackDistance)
            {
                ourWeapon.DelayedAttack();
            }
            else if (!ourWeapon.charging)
            {
                ourWeapon.Moved();

                transform.position = Vector3.MoveTowards(transform.position, engagedPlayer.transform.position, maxDistanceDelta);
            }

            /*
            bool useWeapon = true;
            Weapon abilityScript = ability.GetComponent<Weapon>();

            //if (ability.GetComponent<Weapon>().canAttack) useWeapon = false;
            if (lastAttackBasic && abilityScript.canAttack)
            {
                AbilityAttack();
            }
            else if (!lastAttackBasic && ourWeapon.canAttack)
            {
                BasicAttack();
            }
            else if (abilityScript.canAttack)
            {
                AbilityAttack();
            }
            else
            {
                BasicAttack();
            }

            if (abilityScript.canAttack && ourWeapon.canAttack)

            
            if (!useWeapon)
            {
                if (distance <= ability.GetComponent<Weapon>().attackDistance)
                {
                    ability.GetComponent<Weapon>().DelayedAttack();
                }
                else if (!ability.GetComponent<Weapon>().charging)
                {
                    ability.GetComponent<Weapon>().Moved();

                    transform.position = Vector3.MoveTowards(transform.position, engagedPlayer.transform.position, maxDistanceDelta);
                }
            }
            else
            {
                // Determine whether we attack or move forward
                if (distance <= ourWeapon.attackDistance)
                {
                    ourWeapon.DelayedAttack();
                }
                else if (!ourWeapon.charging)
                {
                    ourWeapon.Moved();

                    transform.position = Vector3.MoveTowards(transform.position, engagedPlayer.transform.position, maxDistanceDelta);
                }
            }
            */
        }
    }

    private void AbilityAttack()
    {
        throw new NotImplementedException();
    }

    private bool AttemptRegularAttack()
    {
        // Rotate Towards our engaged player
        transform.LookAt(engagedPlayer.transform);

        distance = Vector3.Distance(transform.position, engagedPlayer.transform.position);

        // Determine whether we attack or move forward
        if (distance <= ourWeapon.attackDistance)
        {
            ourWeapon.DelayedAttack();
        }
        else if (!ourWeapon.charging)
        {
            ourWeapon.Moved();

            transform.position = Vector3.MoveTowards(transform.position, engagedPlayer.transform.position, maxDistanceDelta);
        }

        return ourWeapon.canAttack;
    }

    private bool AttemptAbilityAttack()
    {
        Weapon abilityWeaponScript = ability.GetComponent<Weapon>();
        // Rotate Towards our engaged player
        transform.LookAt(engagedPlayer.transform);

        distance = Vector3.Distance(transform.position, engagedPlayer.transform.position);

        // Determine whether we attack or move forward
        if (distance <= ourWeapon.attackDistance)
        {
            abilityWeaponScript.DelayedAttack();
        }
        else if (!abilityWeaponScript.charging)
        {
            abilityWeaponScript.Moved();

            transform.position = Vector3.MoveTowards(transform.position, engagedPlayer.transform.position, maxDistanceDelta);
        }

        return abilityWeaponScript.canAttack;
    }


    // Determine which player is closest to us
    private GameObject FindClosestPlayer()
    {
        GameObject index = null;
        float minDistance = 0;

        List<GameObject> toRemove = new List<GameObject>();

        if (engagedPlayers.Count == 0) return null;
        
        for (int i = 0; i < engagedPlayers.Count; i++)
        {
            // Is the player dead?
            if (engagedPlayers[i].GetComponent<PlayerController>().health <= 0)
            {
                toRemove.Add(engagedPlayers[i]);
                continue;
            }

            float temp = Vector3.Distance(transform.position, engagedPlayers[i].transform.position);

            if (temp > maxDistanceToUnfollow)
            {
                if (!toRemove.Contains(engagedPlayers[i])) toRemove.Add(engagedPlayers[i]);
            }

            if (i == 0)
            {
                minDistance = temp;
                index = engagedPlayers[i];

                continue;
            }
            else
            {
                if (distance < minDistance)
                {
                    minDistance = distance;
                    index = engagedPlayers[i];
                }
            }
        }

        if (toRemove.Count > 0)
        {
            foreach(GameObject i in toRemove)
            {
                engagedPlayers.Remove(i);
            }
        }

        if (engagedPlayers.Count > 0) return index;
        else
        {
            engaging = false;
            return null;
        }
    }

    public void ApplyDamage(float damage)
    {
        health = health - damage;

        if (health <= 0) Kill();
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

}
