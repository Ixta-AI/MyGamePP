using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceDebree : MonoBehaviour
{
    public float spaceForce = 0.5f;
    private float zDestroy = -10.0f;
    private Rigidbody spaceDebris;
    public GameObject spacePowerup;


    // Start is called before the first frame update
    void Start()
    {
        spaceDebris = GetComponent<Rigidbody>();
        //spacePowerup = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        spaceDebris.AddForce(Vector3.forward * -spaceForce);
        //spacePowerup.transform.Translate(Vector3.right * -spaceForce * Time.deltaTime);

        if(transform.position.z < zDestroy)
        {
            Destroy(gameObject);
        }
    }
}
