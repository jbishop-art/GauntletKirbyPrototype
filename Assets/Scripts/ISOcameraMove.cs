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
	public float cameraOffsetDepth;
	public float cameraOffsetHeight;
    
    Vector3 endMark;
    Vector3 center;
    public float transitionDuration = 1.0f;

    public bool cameraTrigger;
    public float triggerWaitTime = 1.0f;

    public float maxRange = 55.0f;

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

        //
        //if Player02 moves to far away from Player01, teleport Player02 to Player01 with SMOOTH camera transition.
        //
        if (distance >= maxRange) 
        {
            cameraTrigger = true;

            StartCoroutine(WaitTrigger()); 
        }

        CameraTransition();

    }

    //Waits triggerWaitTime amount until it turns off the cameraTrigger.
    IEnumerator WaitTrigger()
    {
        yield return new WaitForSeconds(triggerWaitTime);
        cameraTrigger = false;
    }

    //Handles camera movement and transitions for teleports.
    void CameraTransition()
    {        
        if (cameraTrigger == true)
        {
            //Sets endMark as center of player01 & player02 after player02 teleport.  Then lerps the camera form player02 old location to endMark.
            endMark = new Vector3((((player01X + player02X) / 2) + cameraOffsetDepth), cameraOffsetHeight, ((player01Z + player02Z) / 2) + cameraOffsetDepth);
            transform.position = Vector3.Lerp(transform.position, endMark, Time.deltaTime * transitionDuration);
        }
        else if (cameraTrigger == false)
        {
            //transform.position = new Vector3((((player01X + player02X) / 2) + cameraOffsetDepth), cameraOffsetHeight, ((player01Z + player02Z) / 2) + cameraOffsetDepth);
            center = new Vector3((((player01X + player02X) / 2) + cameraOffsetDepth), cameraOffsetHeight, ((player01Z + player02Z) / 2) + cameraOffsetDepth);
            transform.position = Vector3.Lerp(transform.position, center, Time.deltaTime * transitionDuration);
        }
        else
        {

        }

    }
    
}
