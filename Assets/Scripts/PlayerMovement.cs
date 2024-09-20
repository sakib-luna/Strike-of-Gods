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
   public float speed; 
   public float regJump; 
   public bool isFacingRight = true; 
   private CharacterController playerController; 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(Input.x * speed, rb.velocity.y); 
    }

    ///////////MOVEMENT//////////////
    public void Move(InputAction.CallbackContext context)
    {
        Input.x = context.ReadValue<Vector2>().x;
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
