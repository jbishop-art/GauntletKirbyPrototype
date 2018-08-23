﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool Player01;
    public bool Player02;
    public float p1moveSpeed;
    public float p2moveSpeed;
    Vector3 p1forward, p1right;
    Vector3 p2forward, p2right;
    public bool controlSwitch;


    // Use this for initialization
    void Start()
    {
        //Establishes forward direction of the camera to have consistant movment based on the character.  Because the camera is at a downward angle and we want the camera to be fixed at this angle and perspective, we need to always have transforms adjust to keep this same angle.
        p1forward = Camera.main.transform.forward;
        p1forward.y = 0;
        p1forward = Vector3.Normalize(p1forward);

        p1right = Quaternion.Euler(new Vector3(0, 90, 0)) * p1forward;

        p2forward = Camera.main.transform.forward;
        p2forward.y = 0;
        p2forward = Vector3.Normalize(p1forward);

        p2right = Quaternion.Euler(new Vector3(0, 90, 0)) * p2forward;
    }

    // Update is called once per frame
    void Update()
    {
        //Dictates if Player01 moves.
        if (Player01 == true)
        {
            //Allows Player01 and the camera to move every frame.
            MoveP1();
        }
        else
        {

        }

        //Dictate if Player02 moves.
        if (Player02 == true)
        {
            //Allows Player02 and the camera to move every frame.
            MoveP2();
        }
        else
        {

        }



    }

    //Drives direction of Player01 based on key input.
    void MoveP1()
    {
        //Detects wether the control switch is TRUE or FALSE.  If true, keyboard is used for movement.  If false, the usb controller is used for movement.  Bool is public so can be used from other areas in game.
        if (controlSwitch == true)
        {
            Vector3 p1rightMovement = p1right * p1moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
            Vector3 p1upMovement = p1forward * p1moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

            Vector3 p1heading = Vector3.Normalize(p1rightMovement + p1upMovement);

            transform.forward = p1heading;
            transform.position += p1rightMovement;
            transform.position += p1upMovement;
        }
        else
        {
            Vector3 p1rightMovement = p1right * p1moveSpeed * Time.deltaTime * Input.GetAxis("P1 Left Thumb Horizontal");
            Vector3 p1upMovement = p1forward * p1moveSpeed * Time.deltaTime * Input.GetAxis("P1 Left Thumb Vertical");

            Vector3 p1heading = Vector3.Normalize(p1rightMovement + p1upMovement);

            transform.forward = p1heading;
            transform.position += p1rightMovement;
            transform.position += p1upMovement;
        }
    }

    //Drives direction of Player02 based on key input.
    void MoveP2()
    {
        //Detects wether the control switch is TRUE or FALSE.  If true, keyboard is used for movement.  If false, the usb controller is used for movement.  Bool is public so can be used from other areas in game.
        if (controlSwitch == true)
        {
            Vector3 p2rightMovement = p2right * p2moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
            Vector3 p2upMovement = p2forward * p2moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

            Vector3 p2heading = Vector3.Normalize(p2rightMovement + p2upMovement);

            transform.forward = p2heading;
            transform.position += p2rightMovement;
            transform.position += p2upMovement;
        }
        else
        {
            Vector3 p2rightMovement = p2right * p2moveSpeed * Time.deltaTime * Input.GetAxis("P2 Left Thumb Horizontal");
            Vector3 p2upMovement = p2forward * p2moveSpeed * Time.deltaTime * Input.GetAxis("P2 Left Thumb Vertical");

            Vector3 p2heading = Vector3.Normalize(p2rightMovement + p2upMovement);

            transform.forward = p2heading;
            transform.position += p2rightMovement;
            transform.position += p2upMovement;
        }
    }
}