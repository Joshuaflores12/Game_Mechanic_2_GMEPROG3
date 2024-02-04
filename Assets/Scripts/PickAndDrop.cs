using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAndDrop : MonoBehaviour
{
    private InputManager inputManager;
    [SerializeField] private Transform objectHolder;
    [SerializeField] private Transform grabbedObject;

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
        float maxDistance = 3f;

        if (!inputManager.IsPlayerGrabbing())
        {
            Debug.Log("Player is not grabbing.");
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
            grabbedObject.position = objectHolder.position;
            grabbedObject.rotation = objectHolder.rotation;
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
}
