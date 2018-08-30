using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarLookAt : MonoBehaviour
{
    public Transform target;
    

	// Use this for initialization
	void Start ()
    {
        target = GameObject.Find("Camera").transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.LookAt(target);
	}

}
