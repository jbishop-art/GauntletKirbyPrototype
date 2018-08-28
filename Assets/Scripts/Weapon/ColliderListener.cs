using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderListener : MonoBehaviour
{
    public MeleeWeapon ourWeapon;

    private void Awake()
    {
        GetComponentInParent<MeleeWeapon>();

    }

    public void OnCollisionEnter(Collision collision)
    {
        ourWeapon.ProcessCollision(collision);
    }
    public void OnTriggerEnter(Collider collider)
    {
        ourWeapon.ProcessTrigger(collider);   
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
