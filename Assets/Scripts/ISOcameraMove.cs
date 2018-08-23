using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISOcameraMove : MonoBehaviour 
{
	public GameObject Player01;
    public GameObject Player02;
    public Rigidbody rbPlayer01;
    public Rigidbody rbPlayer02;
    float distance;
    float distancePrevious;
    public Transform player01T;
    public Transform player02T;
	float player01X;
	float player01Z;
    float player02X;
    float player02Z;
	public float cameraOffsetDepth;
	public float cameraOffsetHeight;
    float player01V;
    float player02V;

    
    
    public float offsetNumD;
    public float offsetNumH;




	// Use this for initialization
	void Start () 
	{
		distancePrevious = Vector3.Distance(player01T.position, player02T.position);
        cameraOffsetDepth = -25;
        cameraOffsetHeight = 26;

    }
	
	// Update is called once per frame
	void Update () 
	{
        //Finds Player01's position for x & z.
		player01X = Player01.transform.position.x;
		player01Z = Player01.transform.position.z;

        //Finds Player02's position for x & y.
        player02X = Player02.transform.position.x;
        player02Z = Player02.transform.position.z;

        //Determines distance between Player01 & Player02.
        distance = Vector3.Distance(player01T.position, player02T.position);

        /*
        player01V = (rbPlayer01.velocity.magnitude * 1000000);
        player02V = (rbPlayer02.velocity.magnitude * 1000000);
        Debug.Log("P1 Vel: " + player01V);
        Debug.Log("P2 Vel: " + player02V);
        */
        
                

        if (distancePrevious > distance)
        {
            //cameraOffsetDepth = (cameraOffsetDepth + offsetNumD);
            cameraOffsetHeight = (cameraOffsetHeight - offsetNumH);

            distancePrevious = distance;
        }
        else if (distancePrevious < distance)
        {
            //cameraOffsetDepth = (cameraOffsetDepth - offsetNumD);
            cameraOffsetHeight = (cameraOffsetHeight + offsetNumH);

            distancePrevious = distance;
        }
        else 
        {
            distancePrevious = distance;
        }


        /////////////    NEEDS WORK!!!  use the && to clarify boundries for camera.   /////////////
        //Positions camera to always keep player/players centered in the camera view.
        if (player01X > player02X)
        {
            transform.position = new Vector3(((player01X - player02X) + cameraOffsetDepth), cameraOffsetHeight, ((player01Z - player02Z) + cameraOffsetDepth));
        }
        else if (player01X < player02X)
        {
            transform.position = new Vector3(((player01X + player02X) + cameraOffsetDepth), cameraOffsetHeight, ((player01Z + player02Z) + cameraOffsetDepth));
        }
        else
        {

        }
        

    }
}
