using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 10;
    private bool engaging = false;
    public float maxDistanceToUnfollow = 40;
    private GameObject engagedPlayer;

    private List<GameObject> engagedPlayers;

    public Weapon ourWeapon;
    private float maxDistanceDelta;
    public float moveSpeed = 10;
    private float distance;

    // Use this for initialization
    void Start ()
    {
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
        if (other.CompareTag("Player"))
        {
            //engagedPlayer = other.gameObject;

            if (!engagedPlayers.Contains(other.gameObject)) engagedPlayers.Add(other.gameObject);

            engaging = true;        
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //engagedPlayers.Remove(other.gameObject);

            //if (engagedPlayers.Count == 0) engaging = false;
        }
    }

    public void Engage()
    {
        engagedPlayer = FindClosestPlayer();

        if (engagedPlayer != null)
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
        }
    }

    // Determine which player is closest to us
    private GameObject FindClosestPlayer()
    {
        int index = 0;
        float minDistance = 0;

        List<int> toRemove = new List<int>();

        for (int i = 0; i < engagedPlayers.Count; i++)
        {
            // Is the player dead?
            if (engagedPlayers[i].GetComponent<PlayerController>().health <= 0)
            {
                toRemove.Add(i);
                continue;
            }

            float temp = Vector3.Distance(transform.position, engagedPlayers[i].transform.position);

            if (temp > maxDistanceToUnfollow)
            {
                if (!toRemove.Contains(i)) toRemove.Add(i);
            }

            if (i == 0)
            {
                minDistance = temp;

                continue;
            }
            else
            {
                if (distance < minDistance)
                {
                    minDistance = distance;
                    index = i;
                }
            }
        }

        if (toRemove.Count > 0)
        {
            foreach(int i in toRemove)
            {
                engagedPlayers.RemoveAt(i);
            }
        }

        if (engagedPlayers.Count > 0) return engagedPlayers[index];
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
