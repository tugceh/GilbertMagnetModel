using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public enum Polarization { Positive, Negative };

    public Polarization polarization;
    public float MagnetForce = 50;
    Rigidbody MagnetRigidBody;
    public GameObject[] magnets,metals;


    private void Start()
    {
        //Get magnet's rigidbody
        MagnetRigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        
           CheckMetals();
           CheckMagnets();
    }


    void CheckMetals()
    {
        foreach (GameObject metalObject in metals)
        {
            float distance = Vector3.Distance(transform.position, metalObject.transform.position);
            Vector3 direction = metalObject.transform.position - transform.position;
            
            Vector2 magneticForce = new Vector2(direction.x * 40f / Mathf.Pow(distance, 2), direction.z * 40f / Mathf.Pow(distance, 2));
            MagnetRigidBody.AddForce(magneticForce.x, 0, magneticForce.y, ForceMode.Force);
        }

    }

    void CheckMagnets()
    {
        foreach (GameObject magnetObject in magnets)
        {
            
            if (magnetObject.GetComponent<Magnet>() && magnetObject.GetComponent<Magnet>() != this)
            {
                
                float distance = Vector3.Distance(transform.position, magnetObject.transform.position);

                if (magnetObject.gameObject.GetComponent<Magnet>().polarization != polarization)
                {
                    Vector3 direction = magnetObject.transform.position - transform.position;
                    AddForceMagnet(distance,direction);
                }
                else
                {
                    Vector3 direction = -(magnetObject.transform.position - transform.position);
                    AddForceMagnet(distance, direction);
                }
               
            }

        }
    }

    //Add force to magnets
    void AddForceMagnet(float distance, Vector3 direction)
    {
        Vector2 magneticForce = new Vector2(direction.x * 40f / Mathf.Pow(distance, 2), direction.z * 40f / Mathf.Pow(distance, 2)) * MagnetRigidBody.mass;
        MagnetRigidBody.AddForce(magneticForce.x, 0, magneticForce.y, ForceMode.Force);
    }
}
