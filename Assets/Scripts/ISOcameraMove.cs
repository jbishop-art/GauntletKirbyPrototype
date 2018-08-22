﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISOcameraMove : MonoBehaviour 
{
	public GameObject Player01;
	float playerX;
	float playerZ;
	public float cameraOffsetDepth;
	public float cameraOffsetHeight;




	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		playerX = Player01.transform.position.x;
		playerZ = Player01.transform.position.z;


		transform.position = new Vector3((playerX + cameraOffsetDepth), cameraOffsetHeight, (playerZ + cameraOffsetDepth));

	}
}
