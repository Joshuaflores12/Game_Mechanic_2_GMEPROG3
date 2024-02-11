using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GrabUI : MonoBehaviour
{
    private InputManager inputManager; // calls input manager Class

    public float maxDistance = 100f;
    public LayerMask grabbableLayer;

    private TextMeshProUGUI grabbableText;

    private void Start()
    {
        inputManager = InputManager.Instance;
        grabbableText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        UpdateGrabbableText();
    }

    private void UpdateGrabbableText()
    {
        Ray ray = inputManager.GetCrosshairPoint(); // Gets Camera ray point cursor, upon hit shows text
        RaycastHit hit;

        bool isGrabbable = Physics.Raycast(ray, out hit, maxDistance, grabbableLayer); // Calls the layer that is set to the object to be grabbed

        if (isGrabbable)
        {
            if (!inputManager.IsPlayerGrabbing()) grabbableText.text = "Grabbable SA WAKAS AAAAAAAAAAAAAAAA FR FR SHEESH ISTG!";
        }
        else
        {
            grabbableText.text = "";
        }
    }
}


