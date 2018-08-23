 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animat_walk : MonoBehaviour {
   // public Transform target;
   // public float speed;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //float step = speed * Time.deltaTime;
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, step);
        transform.LookAt(Camera.main.transform.position, Vector3.up);

        //transform.rotation = 
	}
}
