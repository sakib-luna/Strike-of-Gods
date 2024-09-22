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
   public float regJump; 
   public bool isFacingRight = true; 
    bool isCrouching; 
    bool isGrounded = false;
   private CharacterController playerController; 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       MovementFunction();

       if(Input.y > 0 && isGrounded == true)
       {
         Debug.Log("Player can jump"); 
       }

    }

    ///////////MOVEMENT//////////////
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

       if(isFacingRight == true && Input.x < 0)
       {
         speed = movespeed2; 
       }
       else if(isFacingRight == false && Input.x > 0)
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

    ///////ATTACKING/////////
    
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
