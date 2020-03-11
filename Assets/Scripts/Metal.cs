using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metal : MonoBehaviour
{
    Rigidbody metalRigidbody;
    public GameObject[] magnets;

    void Start()
    {
        metalRigidbody = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        foreach (GameObject magnetObject in magnets)
        {
            float distance = Vector3.Distance(transform.position, magnetObject.transform.position);
            Vector3 direction = magnetObject.transform.position - transform.position;

            Vector2 magneticForce = new Vector2(direction.x * 40f / Mathf.Pow(distance, 2), direction.z * 40f / Mathf.Pow(distance, 2));
            metalRigidbody.AddForce(magneticForce.x, 0, magneticForce.y, ForceMode.Force);
        }

    }

}
