using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

[RequireComponent(typeof(CharacterController))] // auto adds a character controller to anything with this script
public class PlayerMovement : MonoBehaviour
{
   [SerializeField] public Vector2 Input; // think of this as a horziontal and vertical input method mushed together
   // Note that this type of way to get the x and y inputs for your player is good when using unity new input system as far as I know
   public Rigidbody2D  rb; 
   float speed; 
   public float movespeed1; // normal movement speed
   public float movespeed2; // moving back movement speed
   public float movespeed3; // running movment speed 
   public float regJump; // How high the character will jump
   public int jsFrame; 
   public int jsFrameStart;
   public int doubleJumps; 
   public bool isFacingRight = true; 
   bool isCrouching; 
   public bool isGrounded = false;
   bool jumpSquat; 
   private CharacterController playerController; 
   public PlayerTemplate playerTemplate; 
    private LayerMask yourLayer;
   private LayerMask opsLayer;
 
   // public GameObject cube; 


    // Start is called before the first frame update
    void Start()
    {

       yourLayer = playerTemplate.yourLayer; 
       opsLayer = playerTemplate.opsLayer;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
    void FixedUpdate()
    {
      MovementFunction();
       
       /////////////////////////// JUMP FUNCTION START //////////////////////////
       if(Input.y > 0 && isGrounded == true)
       {
          jumpSquat = true;
       }

       if(jumpSquat == true && jsFrame != 0)
       {
          jsFrame -= 1;
       }
       else if (jsFrame == 0) 
       {
         //animation.SetTrigger("jump");
         rb.velocity = new Vector2(rb.velocity.x, regJump);
         //rb.AddForce(Vector2.up * regJump, ForceMode2D.Impulse); // this could be use as one of the ways to jump 
         // Debug.Log("Player has jumped");
         jumpSquat = false; 
         jsFrame = jsFrameStart;
         isGrounded = false; 
       }
        ////////////////////////// JUMP FUNCTION END ///////////////////

      //////////////////////////CHARACTER FLIP FUNCTION///////////////////////////////////////
       
       // Define the size and direction for the BoxCast
    
       Vector2 boxSize = new Vector2(0.5f, 5);
    
      float distance = 0.1f; // Small distance to check for collisions
      // Check for collisions on the right side
      
      RaycastHit2D rightSideDetector = Physics2D.BoxCast(rb.position + new Vector2(2, 0), boxSize, 0, Vector2.right, distance, opsLayer);
      
      // Check for collisions on the left side
      
      RaycastHit2D leftSideDetector = Physics2D.BoxCast(rb.position + new Vector2(-2, 0), boxSize, 0, Vector2.left, distance, opsLayer);
      
      // Flip the GameObject based on the collision
      
      if (rightSideDetector.collider != null && rightSideDetector.collider.gameObject != this.gameObject)
    {
        transform.localScale = new Vector3(1, 1, 1); // Flip to face right
        isFacingRight = true;
    }
    
    else if (leftSideDetector.collider != null && leftSideDetector.collider.gameObject != this.gameObject)
    {
        transform.localScale = new Vector3(-1, 1, 1); // Flip to face left
        isFacingRight = false;
    }
    
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue; 
        Gizmos.DrawCube(rb.position + new Vector2(2,0), new Vector2(0.5f,5));
        Gizmos.DrawCube(rb.position + new Vector2(-2,0), new Vector2(0.5f,5));
    }
    
////////////////////////////////   MOVEMENT     //////////////////////////////////////
    public void Move(InputAction.CallbackContext context)
    {
        Input.x = context.ReadValue<Vector2>().x;
        Input.y = context.ReadValue<Vector2>().y; 
    }

    private void MovementFunction()
    {
       // allows the player to move left and right only if they not crouching 
       if(Input.y < 0 && isGrounded == true)
       {
         isCrouching = true; 
       }
       else
       {
        transform.Translate(Vector2.right * Input.x * Time.deltaTime * speed);
        isCrouching = false; 
       }

       if((isFacingRight == true && Input.x < 0 && isGrounded == true) || (isFacingRight == false && Input.x > 0 && isGrounded ==true))
       {
         speed = movespeed2; 
       }
       else
       {
         speed = movespeed1; 
       }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.gameObject.CompareTag("Ground"))
        {
          isGrounded = true;
        }
    }

//////////////////ATTACKING/////////////////////////
    
    //fun fact: a slight tap on a button or probaly any button last for 3 frames.
    
    public void LightAttack(InputAction.CallbackContext context)
    {
      Debug.Log("Light attack had been pressed");
    }

    public void MediumAttack(InputAction.CallbackContext context)
    {
      Debug.Log("Medium attack had been pressed");
    }

    public void HeavyAttack(InputAction.CallbackContext context)
    {
      Debug.Log("Heavy attack had been pressed");
    }

    public void SpecialAttack(InputAction.CallbackContext context)
    {
     Debug.Log("Special Attack had been pressed");
    }
}
