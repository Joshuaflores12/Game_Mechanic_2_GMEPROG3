using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GrabUI : MonoBehaviour
{
    private InputManager inputManager;

    public float maxDistance = 3f;
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
        Ray ray = inputManager.GetCrosshairPoint();
        RaycastHit hit;

        bool isGrabbable = Physics.Raycast(ray, out hit, maxDistance, grabbableLayer);

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


