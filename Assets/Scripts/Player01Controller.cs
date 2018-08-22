using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player01Controller : MonoBehaviour 
{
	public float moveSpeed;
	Vector3 forward, right;
    public bool controlSwitch;


	// Use this for initialization
	void Start () 
	{
        //Establishes forward direction of the camera to have consistant movment based on the character.  Because the camera is at a downward angle and we want the camera to be fixed at this angle and perspective, we need to always have transforms adjust to keep this same angle.
		forward = Camera.main.transform.forward;
		forward.y = 0;
		forward = Vector3.Normalize(forward);

		right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Allows character and the camera to move every frame.
		Move();

	}

	//Drives direction of object based on key input.
	void Move()
	{
        //Detects wether the control switch is TRUE or FALSE.  If true, keyboard is used for movement.  If false, the usb controller is used for movement.
        if (controlSwitch == true)
        {
            Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
            Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

            Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

            transform.forward = heading;
            transform.position += rightMovement;
            transform.position += upMovement;
        }
        else
        {
            Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("Left Thumb Horizontal");
            Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("Left Thumb Vertical");

            Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

            transform.forward = heading;
            transform.position += rightMovement;
            transform.position += upMovement;
        }
        

	}
}
