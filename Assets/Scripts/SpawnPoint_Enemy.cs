using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint_Enemy : MonoBehaviour
{
    public bool Enable01;
    public GameObject enemy01;
    public int enemy01SpawnTimer = 1;
    public bool enemy01Wait;
    public bool Enable02;
    public GameObject enemy02;
    public int enemy02SpawnTimer = 1;
    public bool enemy02Wait;
        
	// Use this for initialization
	void Start ()
    {
        enemy01Wait = false;
        enemy02Wait = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        SpawnEnemy01();
        SpawnEnemy02();
    }

    void SpawnEnemy01()
    {
        if ((enemy01Wait == false) && (Enable01 == true))
        {
            Instantiate(enemy01, transform.position, Quaternion.identity);
            enemy01Wait = true;
            StartCoroutine(WaitEnemy01());
        }
    }

    IEnumerator WaitEnemy01()
    {
        Debug.Log("WAIT!");
        yield return new WaitForSeconds(enemy01SpawnTimer);
        Debug.Log("FLIP");
        enemy01Wait = false;
        SpawnEnemy01();
    }

    void SpawnEnemy02()
    {
        if ((enemy02Wait == false) && (Enable02 == true))
        {
            Instantiate(enemy02, transform.position, Quaternion.identity);
            enemy02Wait = true;
            StartCoroutine(WaitEnemy02());
        }
    }

    IEnumerator WaitEnemy02()
    {
        yield return new WaitForSeconds(enemy02SpawnTimer);
        enemy02Wait = false;
        SpawnEnemy02();
    }
}
