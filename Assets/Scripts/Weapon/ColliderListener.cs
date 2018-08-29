using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderListener : MonoBehaviour
{
    public bool isWeapon = false;

    public MeleeWeapon ourWeapon;
    public DashAbility dash;

    private void Awake()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (isWeapon) ourWeapon.ProcessCollision(collision);
        else dash.ProcessCollision(collision);

        Debug.Log("Listening.");
        //ourWeapon.ProcessCollision(collision);
    }
    public void OnTriggerEnter(Collider collider)
    {
        if (isWeapon) ourWeapon.ProcessTrigger(collider);
        else dash.ProcessTrigger(collider);

        //ourWeapon.ProcessTrigger(collider);   
    }

    public void OnCollisionStay(Collision collision)
    {
        Debug.Log("Listening.");
        ourWeapon.ProcessCollision(collision);
    }
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
