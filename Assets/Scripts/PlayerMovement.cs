using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float movespeed;
    private Rigidbody rb;
    InputManager managerinputs;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        managerinputs = InputManager.Instance;
    }

    // Update is called once per frame
   private void Update()
    {
        Move();
    }
    private void Move() 
    {
        Vector3 value = managerinputs.GetMovementInput();
        Debug.Log("Input Value" + value);
        if (value == Vector3.zero) 
        {
        rb.velocity = new Vector3(0, rb.velocity.y,0);
        }
        Vector3 direction = new Vector3(value.x, 0, value.y);
        Vector3 velocity = direction * movespeed * Time.deltaTime;
        velocity.y = rb.velocity.y;
        rb.AddRelativeForce(velocity);
    }
    

}
