 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chara_FacingCamera : MonoBehaviour {
    public Transform target;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //transform.LookAt(Camera.main.transform.position, Vector3.up);

        if (target != null)
        {
            transform.rotation = target.rotation; 
        }
	}
}
