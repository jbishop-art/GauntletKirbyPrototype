using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    GameObject parent;
    float intHP;
    float curHPper;
    float origX;
    public bool isPlayer;
    string charType;

    // Use this for initialization
    void Start()
    {
        if (isPlayer == true)
        {
            intHP = transform.root.GetComponent<PlayerController>().health;
        }
        else if (isPlayer == false)
        {
            intHP = transform.root.GetComponent<EnemyController>().health;
        }
                            
        origX = gameObject.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayer == true)
        {
            curHPper = transform.root.GetComponent<PlayerController>().health / intHP;
            transform.localScale = new Vector3(origX * curHPper, 0.2f, 0.05f);
        }
        else if (isPlayer == false)
        {
            curHPper = transform.root.GetComponent<EnemyController>().health / intHP;
            transform.localScale = new Vector3(origX * curHPper, 0.2f, 0.05f);
        }


    }
}
