using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facing_Direction : MonoBehaviour {
    // Use this for initialization
    void Start () {
	}

    public void switchAnimation(string s)
    {
        GetComponent<Animator>().SetTrigger(s);
    }
    public void leftMove()
    {
        GetComponent<Animator>().SetTrigger("leftMoveTrigger");
    }

    public void rightMove()
    {
        GetComponent<Animator>().SetTrigger("rightMoveTrigger");
    }

    public void turnAround()
    {
        GetComponent<Animator>().SetTrigger("turnAroundTrigger");
    }
    
    public void moveForward()
    {
        GetComponent<Animator>().SetTrigger("walkingTrigger");
    }

    public void idle()
    {
        GetComponent<Animator>().SetTrigger("idleTrigger");
    }
    
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey("f"))
        {
            switchAnimation("leftMoveTrigger");
            //Debug.Log("it works");
            //leftMove();
        } else if (Input.GetKey("g"))
        {
            turnAround();
        } else if (Input.GetKey("h"))
        {
            rightMove();
        } else if (Input.GetKey("t"))
        {
            moveForward();
        } else
        {
            idle();
        }
    }
}
