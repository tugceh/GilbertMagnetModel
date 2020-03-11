using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    TouchPhase touchPhase = TouchPhase.Began;
    Touch touch;

    Rigidbody draggableObjectRB = null;
    public float Speed;

    void Update()
    {
        if (Input.touchSupported)
        {
            Raycasting(Input.GetTouch(0).position);
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Raycasting(Input.mousePosition);
            }
        }

    }

    private void FixedUpdate()
    {
        if (Input.touchSupported && draggableObjectRB != null)
        {
            if (Input.touchCount > 0 )
            {
                touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved && draggableObjectRB != null)
                {
                    Raycasting(Input.GetTouch(0).position);
                    AddForce(Input.GetTouch(0).position);
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    draggableObjectRB = null;
                }

            }

        }
        else
        {
            if (Input.GetMouseButton(0) && draggableObjectRB != null)
            {
                Raycasting(Input.mousePosition);
                AddForce(Input.mousePosition);
            }
            else
            {
                draggableObjectRB = null;
            }
        }
    }

    

    void AddForce(Vector3 positionInput)
    {
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(positionInput);

        //Difference positions between target position and current position
        Vector2 positionDifference = new Vector2(targetPosition.x - draggableObjectRB.transform.position.x, targetPosition.z - draggableObjectRB.transform.position.z).normalized;

        Vector2 force = positionDifference * draggableObjectRB.mass * Speed * Time.fixedDeltaTime;

        draggableObjectRB.AddForce(force.x, 0f, force.y, ForceMode.Force);

    }

    void Raycasting(Vector3 positionInput)
    {
        try
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(positionInput);

            bool hit = Physics.Raycast(ray, out hitInfo, Mathf.Infinity, 1 << LayerMask.NameToLayer("DragTargets"));


            if (hit)
            {
                draggableObjectRB = hitInfo.collider.attachedRigidbody;
            }
        }
        catch (System.Exception)
        {

            Debug.Log("Raycast is not success");
        }
    }
}
