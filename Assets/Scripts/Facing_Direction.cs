using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facing_Direction : MonoBehaviour {
    private Vector3 _origPos;
    private float movement = 0f;
    private Rigidbody rigidBody;

    bool facingRight = true;
    public PlayerController player01;
    public PlayerController player02;



    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();

       

	}

    
	// Update is called once per frame
	void Update () {

        float move = Input.GetAxis("Horizontal");

        /*
        Vector3 moveDirection = gameObject.transform.position - _origPos;

       if (moveDirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }*/

        /*if (movement > 0f)
        {
            transform.localScale = new Vector2(3.0f, 3.0f);
        }
        else if (movement < 0f)
        {
            transform.localScale = new Vector2(-3.0f, 3.0f);
        }
        */

        if (move < 0 && facingRight) flip();
        if (move > 0 && !facingRight) flip();
    }

    void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up * 180);
    }
}
