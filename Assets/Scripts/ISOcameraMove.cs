using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISOcameraMove : MonoBehaviour 
{
	public GameObject Player01;
    public GameObject Player02;
    float distance;
    float distancePrevious;
    public Transform player01T;
    public Transform player02T;
	float player01X;
	float player01Z;
    float player02X;
    float player02Z;
    float player02Y;
	public float cameraOffsetDepth;
	public float cameraOffsetHeight;
    float cameraAdjust;

    public float transitionDuration = 1.0f;
    Vector3 startMark;
    Vector3 endMark;

    bool cameraTrigger;


	// Use this for initialization
	void Start () 
	{
		//distancePrevious = Vector3.Distance(player01T.position, player02T.position);
        cameraOffsetDepth = -25;
        cameraOffsetHeight = 26;

        cameraTrigger = false;

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
        player02Y = Player02.transform.position.y;

        //Determines distance between Player01 & Player02.
        distance = Vector3.Distance(player01T.position, player02T.position);

        
        //Positions camera to always keep player/players centered in the camera view.
        if ((distance > 35) && (cameraOffsetDepth > (-35)))
        {
            cameraOffsetDepth = cameraOffsetDepth - 0.1f;
        }
        else if ((distance <= 35) && (cameraOffsetDepth <= (-25)))
        {
            cameraOffsetDepth = cameraOffsetDepth + 0.1f;
        }

        if (distance >= 55)
        {
            startMark = new Vector3(player02X, player02Y, player02Z);
            endMark = new Vector3((((player01X + player02X) / 2) + cameraOffsetDepth), cameraOffsetHeight, ((player01Z + player02Z) / 2) + cameraOffsetDepth);
            transform.position = Vector3.Lerp(startMark, endMark, 1);
            //Use bool switch.
        }
        else
        {
            transform.position = new Vector3((((player01X + player02X) / 2) + cameraOffsetDepth), cameraOffsetHeight, ((player01Z + player02Z) / 2) + cameraOffsetDepth);

        }



        //transform.position = new Vector3((((player01X + player02X) / 2) + cameraOffsetDepth), cameraOffsetHeight, ((player01Z + player02Z) / 2) + cameraOffsetDepth);

        //transform.position = Vector3.Lerp(new Vector3(player02X, player02Y, player02Z), new Vector3((((player01X + player02X) / 2) + cameraOffsetDepth), cameraOffsetHeight, ((player01Z + player02Z) / 2) + cameraOffsetDepth), 1);

    }

}
