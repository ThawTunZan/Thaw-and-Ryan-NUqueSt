using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IDataPersistence
{
    public float movespeed = 1f;

    public Rigidbody2D rb;

    public float collisionOffset = 0.05f;

    public ContactFilter2D movementFilter;

    Vector2 movementInput;

    Animator animator;

    SpriteRenderer spriteRenderer;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();    
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
    }

    public void SaveData(ref GameData data)
    {
        data.playerPosition = this.transform.position;
    }

    private void FixedUpdate()
    {
        //executed if there is a player keyboard input
        if (movementInput != Vector2.zero)
        {
            //try movement using the player's both x and y inputs
            bool success = TryMove(movementInput);
            if (!success && movementInput.x != 0)
            {
                //try movement using the player's movement only in the x direction;
                success = TryMove(new Vector2(movementInput.x, 0));
            }
            if (!success && movementInput.y != 0)
            {
                //try movement using the player's movement only in the x direction;
                success = TryMove(new Vector2(0, movementInput.y));
            }
            
            if (movementInput.x != 0 && movementInput.y !=0)
            {
                animator.SetBool("isMovingSide", true);
                animator.SetBool("isMovingUp", false);
                animator.SetBool("isMovingDown", false);
            }
            //animator.SetBool("isMoving", success);

        }
        else
        {
            animator.SetBool("isMovingSide", false);
            animator.SetBool("isMovingUp", false);
            animator.SetBool("isMovingDown", false);
        }

        //Set direction of sprite to movement direction
        if (movementInput.x < 0)
        {
            spriteRenderer.flipX = true;
            animator.SetBool("isMovingSide", true);
        }
        else if (movementInput.x > 0)
        {
            spriteRenderer.flipX = false;
            animator.SetBool("isMovingSide", true);
        }
        else
        {

        }
        if (movementInput.y > 0 && movementInput.x == 0)
        {
            animator.SetBool("isMovingUp", true);
            animator.SetBool("isMovingDown", false);
            animator.SetBool("isMovingSide", false); 

        }
        else if (movementInput.y < 0 && movementInput.x == 0)
        {
            animator.SetBool("isMovingUp", false);
            animator.SetBool("isMovingDown", true);
            animator.SetBool("isMovingSide", false);
        }
        else if (movementInput.x != 0 && movementInput.y == 0) 
        {
            animator.SetBool("isMovingSide", true);
            animator.SetBool("isMovingUp", false);
            animator.SetBool("isMovingDown", false);
        }
        
    }
    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero) { 
            int count = rb.Cast(
            direction,
            movementFilter,
            castCollisions,
            movespeed * Time.fixedDeltaTime + collisionOffset);
            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * movespeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

 
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
}

