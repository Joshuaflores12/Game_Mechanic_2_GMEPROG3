using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAndDrop : MonoBehaviour
{
    private InputManager inputManager;
    [SerializeField] private Transform objectHolder;
    [SerializeField] private Transform grabbedObject;
    [SerializeField] private float objectMoveSpeed;
    [SerializeField] private float throwForce;
    private void Start()
    {
        inputManager = InputManager.Instance; // calls the input manager class singleton
    }

    void Update()
    {
        Grab();
    }

    private void Grab()
    {
        Ray ray = inputManager.GetCrosshairPoint(); // adds raycast on the players cursor point
        RaycastHit hit;
        float maxDistance = 100f;

        if (!inputManager.IsPlayerGrabbing()) // if the player is not grabbing the object release the object
        {
            ReleaseObject();
            return;
        }

        if (Physics.Raycast(ray, out hit) // if the ray cast hit the tag item as "Object" calculates the grabbable distance rannge, upon hit it will be child to the object holder parent
            && hit.transform.CompareTag("Object")
            && Vector3.Distance(transform.position, hit.point) <= maxDistance
            && grabbedObject == null)
        {
            grabbedObject = hit.transform;
            grabbedObject.parent = objectHolder;
        }

        if (grabbedObject != null) // if the grabbed item is not null set it's target position to the object holder, It will move towards on the object holder position based on the amount of move speed it has
        {
            Vector3 targetPosition = objectHolder.position;
            grabbedObject.position = Vector3.MoveTowards(grabbedObject.position, targetPosition, objectMoveSpeed * Time.deltaTime);
            if (Vector3.Distance(grabbedObject.position, targetPosition) <= 1)
            {
                ThrowObject();
            }
        }
    }

    private void ReleaseObject() //upon releasing the object the variables below will be null/empty
    {
        if (grabbedObject != null)
        {
            grabbedObject.parent = null;
            grabbedObject = null;
        }
    }

    private void ThrowObject() // If the item is not null, throws the object forward upon reaching the object holder and the strength of the throw force will multiplied of the object holder direction, gets it's rigid body component and throw velocity of the rigid body
    {
        if (grabbedObject != null)
        {
            Vector3 throwDirection = objectHolder.forward;
            Vector3 throwVelocity = throwDirection * throwForce;
            Rigidbody grabbedObjectRb = grabbedObject.GetComponent<Rigidbody>();
            grabbedObjectRb.velocity = throwVelocity;
            ReleaseObject();
        }
    }
}
