using UnityEngine;

public class FirstPersonView : MonoBehaviour
{
    private InputManager inputManager;

    private Vector3 playerOrientation;
    [SerializeField] private Transform cameraHolder;
    [Header("Sensitivity and Smoothing")]
    [SerializeField, Range(0, 2)] private float sensitivity;
    [SerializeField, Range(0, 100)] private float smoothing;

    private Vector2 smoothedDelta = Vector2.zero;

    private void Start()
    {
        inputManager = InputManager.Instance;
        HideCursor();
    }

    private void Update()
    {
        Look();
    }

    private void Look()
    {
        Vector3 mouseDelta = inputManager.GetMouseDelta();

        mouseDelta *= sensitivity;

        smoothedDelta = Vector2.Lerp(smoothedDelta, mouseDelta, 1f / smoothing);

        playerOrientation.x += mouseDelta.x;
        playerOrientation.y -= mouseDelta.y;

        playerOrientation.x = Mathf.Repeat(playerOrientation.x + 180f, 360f) - 180f;
        playerOrientation.y = Mathf.Clamp(playerOrientation.y, -30f, 30f);

        cameraHolder.rotation = Quaternion.Euler(new Vector3(playerOrientation.y, cameraHolder.rotation.eulerAngles.y, 0));

        transform.rotation = Quaternion.Euler(new Vector3(0, playerOrientation.x, 0));
    }

    private void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
