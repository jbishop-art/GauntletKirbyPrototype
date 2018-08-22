using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player01Controller : MonoBehaviour 
{
	public float moveSpeed;
	Vector3 forward, right;

	// Use this for initialization
	void Start () 
	{
		forward = Camera.main.transform.forward;
		forward.y = 0;
		forward = Vector3.Normalize(forward);

		right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

	

	}
	
	// Update is called once per frame
	void Update () 
	{
		//Detect Input.
		if (Input.anyKey)
			Move();
	}

	//Drives direction of object based on key input.
	void Move()
	{
		//Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
		//Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

		Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("Left Thumb Horizontal");
		Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("Left Thumb Vertical");

		Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

		transform.forward = heading;
		transform.position += rightMovement;
		transform.position += upMovement;

	}
}
