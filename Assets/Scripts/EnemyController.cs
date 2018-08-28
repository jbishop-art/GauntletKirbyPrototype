using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 10;
    public GameObject ability;
    


	// Use this for initialization
	void Start ()
    {
        GameObject spawnedAbility = Instantiate(ability, transform.position, Quaternion.identity);

        spawnedAbility.transform.parent = gameObject.transform; 
    }
	
	// Update is called once per frame
	void Update ()
    {
        
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
