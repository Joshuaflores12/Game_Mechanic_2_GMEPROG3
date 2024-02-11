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
        {
            ReleaseObject();
            return;
        }

        if (Physics.Raycast(ray, out hit)
            && hit.transform.CompareTag("Object")
            && Vector3.Distance(transform.position, hit.point) <= maxDistance
            && grabbedObject == null)
        {
            grabbedObject = hit.transform;
            grabbedObject.parent = objectHolder;
        }

        if (grabbedObject != null)
        {
            Vector3 targetPosition = objectHolder.position;
            grabbedObject.position = Vector3.MoveTowards(grabbedObject.position, targetPosition, objectMoveSpeed * Time.deltaTime);
            if (Vector3.Distance(grabbedObject.position, targetPosition) <= 1)
            {
                ThrowObject();
            }
        }
    }

    private void ReleaseObject()
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
            Vector3 throwDirection = objectHolder.forward;
            Vector3 throwVelocity = throwDirection * throwForce;
            Rigidbody grabbedObjectRb = grabbedObject.GetComponent<Rigidbody>();
            grabbedObjectRb.velocity = throwVelocity;
            ReleaseObject();
        }
    }
}
