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
        rb.velocity = new Vector3(0, rb.velocity.y,0); // Reset Values upon moving on different direction
        }
        Vector3 direction = new Vector3(value.x, 0, value.y); // Player movement x side to side z front and back
        Vector3 velocity = direction * movespeed * Time.deltaTime; //movement speed
        velocity.y = rb.velocity.y;
        rb.AddRelativeForce(velocity);
    }
    

}
