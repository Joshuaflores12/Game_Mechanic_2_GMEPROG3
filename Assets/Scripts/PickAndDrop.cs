using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PickAndDrop : MonoBehaviour
{
    private InputManager inputManager;
    [SerializeField] private Transform objectHolder;
    [SerializeField] private Transform grabbedObject;
    [SerializeField] private float objectmovespeed;
    [SerializeField] private float throwForce;
    private void Start()
    {
        inputManager = InputManager.Instance;
    }

    void Update()
    {
        Grab();
    }

    private void Grab()
    {
        Ray ray = inputManager.GetCrosshairPoint();
        RaycastHit hit;
        float maxDistance = 100f;

        if (!inputManager.IsPlayerGrabbing())
        {   ReleaseObject();
            return;
        }

        if (Physics.Raycast(ray, out hit)
            && hit.transform.CompareTag("Object") // If the item tagged as "Object" gets the distance between the cursor and the object
            && Vector3.Distance(transform.position, hit.point) <= maxDistance
            && grabbedObject == null)
        {
            grabbedObject = hit.transform; // Upon hit the item will be child to the parent Object Holder, the players hands
            grabbedObject.parent = objectHolder;
        }

        if (grabbedObject != null) // If the object is not yet grabbed it will move towards the object holder
        {
            Vector3 targetPosition = objectHolder.position;
            grabbedObject.position = Vector3.MoveTowards(grabbedObject.position, targetPosition, objectmovespeed * Time.deltaTime);
            if (Vector3.Distance(grabbedObject.position, targetPosition) <=1)
            {
                ThrowObject();
            }
        }     
    }

    private void ReleaseObject() //Upon releasing the object the values will be null/empty
    {
        if (grabbedObject != null)
        {
            grabbedObject.parent = null;
            grabbedObject = null;
        }
    }
    private void ThrowObject()
    {
        if (grabbedObject != null)
        {
            
            Vector3 throwDirection = (objectHolder.forward); //Object will be thrown forward upon reaching the object holder

            
            Vector3 throwVelocity = throwDirection * throwForce; //Force value

            
            Rigidbody grabbedObjectRb = grabbedObject.GetComponent<Rigidbody>(); //Get the rb of the Item to throw
            grabbedObjectRb.velocity = throwVelocity;

            
            ReleaseObject();
        }
    }
}
