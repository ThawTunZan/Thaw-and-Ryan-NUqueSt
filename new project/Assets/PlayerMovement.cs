using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IDataPersistence
{
    //for movement
    public Rigidbody2D rb;
    public ContactFilter2D movementFilter;
    Vector2 movementInput;
    public float movespeed = 1f;
    public float collisionOffset = 0.05f;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>(); //to check for collision

    //To animate player
    Animator animator;
    SpriteRenderer spriteRenderer;
    private SwordAttack swordAttack;

    //Scriptable Objects
    public PlayerPositionSO startingPosition;

    //misc
    bool canMove = true;

    public bool facingUp;

    public SpriteRenderer weaponDarkColour;
    public SpriteRenderer weaponLightColour;
    public string itemRarity;
    private int currFrame;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();    
        spriteRenderer = GetComponent<SpriteRenderer>();
        swordAttack = GetComponent<SwordAttack>();

        if (startingPosition.transittedScene == true)
        {
            transform.position = startingPosition.InitialValue;
            startingPosition.transittedScene = false;
            DataPersistenceManager.instance.sceneTransitted = false;
        }
        else if (startingPosition.playerDead == true)
        {
            transform.position = startingPosition.InitialValue;
            startingPosition.playerDead = false;
        }
    }

    public void LoadData(GameData data)
    {
        transform.position = data.playerPosition;
    }

    public void SaveData(GameData data)
    {
        data.playerPosition = new Vector2 ((float)0.561, (float)0.476);
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            //executed if there is a player keyboard input, the subsequent ifs are to slide along an obstacle
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
                if (movementInput.x != 0 && movementInput.y != 0)
                {
                    animator.SetBool("isMovingSide", true);
                    animator.SetBool("isMovingUp", false);
                    animator.SetBool("isMovingDown", false);
                }
            }
            else
            {
                animator.SetBool("isMovingSide", false);
                animator.SetBool("isMovingUp", false);
                animator.SetBool("isMovingDown", false);
            }

            //Set the direction sprite faces based on movement direction
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
            //to move up animation
            if (movementInput.y > 0 && movementInput.x == 0)
            {
                facingUp = true;
                animator.SetBool("isMovingUp", true);
                animator.SetBool("isMovingDown", false);
                animator.SetBool("isMovingSide", false);

            }
            //to move down animation
            else if (movementInput.y < 0 && movementInput.x == 0)
            {
                facingUp = false;
                animator.SetBool("isMovingUp", false);
                animator.SetBool("isMovingDown", true);
                animator.SetBool("isMovingSide", false);
            }
            //to move side animation
            else if (movementInput.x != 0 && movementInput.y == 0)
            {
                animator.SetBool("isMovingSide", true);
                animator.SetBool("isMovingUp", false);
                animator.SetBool("isMovingDown", false);
            }
        }
        
    }

    /*
     * To check for collision
     * Takes in a Vector2 parameter to test for directions
     * returns true if it is a valid route and moves in that direction(e.g not blocked by obstacle in that particular direction)
     */
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

    public void AnimateToolAttack(string toolType, string toolRarity)
    {
        itemRarity = toolRarity;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("player_idle_down") || animator.GetCurrentAnimatorStateInfo(0).IsName("player_walk_down"))
        {
            if (toolType == "Sword")
            {
                animator.SetTrigger("swordAttackDown");
            }
            else if (toolType == "Pickaxe")
            {
                animator.SetTrigger("pickaxeAttackDown");
            }
            else if (toolType == "Hoe")
            {
                animator.SetTrigger("hoeAttackDown");
            }
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("player_idle_up") || animator.GetCurrentAnimatorStateInfo(0).IsName("player_walk_up"))
        {
            if (toolType == "Sword")
            {
                animator.SetTrigger("swordAttackUp");
            }
            else if (toolType == "Pickaxe")
            {
                animator.SetTrigger("pickaxeAttackUp");
            }
            else if (toolType == "Hoe")
            {
                animator.SetTrigger("hoeAttackUp");
            }
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("player_idle_side") || animator.GetCurrentAnimatorStateInfo(0).IsName("player_walk_side"))
        {
            if (toolType == "Sword")
            {
                animator.SetTrigger("swordAttackSide");
            }
            else if (toolType == "Pickaxe")
            {
                animator.SetTrigger("pickaxeAttackSide");
            }
            else if (toolType == "Hoe")
            {
                animator.SetTrigger("hoeAttackSide");
            }
        }
    }

    public void AnimateToolColour()
    {
        if (currFrame == 0)
        {
            weaponDarkColour.sprite = Resources.Load<Sprite>("Colour/DarkPickaxeSide_0");
            weaponLightColour.sprite = Resources.Load<Sprite>("Colour/LightPickaxeSide_0");
            Debug.Log(itemRarity);
            if (itemRarity == "Diamond")
            {
                weaponDarkColour.color = new Color32(29, 204, 210, 255);
                weaponLightColour.color = new Color32(117, 226, 205, 255);
            }
            currFrame++;
        }
        else if (currFrame == 1)
        {
            weaponDarkColour.sprite = Resources.Load<Sprite>("Colour/DarkPickaxeSide_1");
            weaponLightColour.sprite = Resources.Load<Sprite>("Colour/LightPickaxeSide_1");
            currFrame++;
        }
        else if (currFrame == 2)
        {
            weaponDarkColour.sprite = Resources.Load<Sprite>("Colour/DarkPickaxeSide_2");
            weaponLightColour.sprite = Resources.Load<Sprite>("Colour/LightPickaxeSide_2");
            currFrame++;
        }
        else if (currFrame == 3)
        {
            weaponDarkColour.sprite = Resources.Load<Sprite>("Colour/DarkPickaxeSide_3");
            weaponLightColour.sprite = Resources.Load<Sprite>("Colour/LightPickaxeSide_3");
            currFrame++;
        }
        else if (currFrame == 4)
        {
            weaponDarkColour.sprite = null;
            weaponLightColour.sprite = null;
            currFrame = 0;
        }
    }

    /*
     * execute the corresponding function in the script swordAttack bsaed on the state of current animation and the x direction the player is facing
     */
    public void PerformToolAttack()
    {
        LockMovement();
        if ((animator.GetCurrentAnimatorStateInfo(0).IsName("player_attack_side") 
            || animator.GetCurrentAnimatorStateInfo(0).IsName("player_pickaxe_side") 
            || animator.GetCurrentAnimatorStateInfo(0).IsName("player_hoe_side"))
            && spriteRenderer.flipX == true)
        {
            swordAttack.AttackLeft();
        }
        else if ((animator.GetCurrentAnimatorStateInfo(0).IsName("player_attack_side") 
            || animator.GetCurrentAnimatorStateInfo(0).IsName("player_pickaxe_side") 
            || animator.GetCurrentAnimatorStateInfo(0).IsName("player_hoe_side"))
            && spriteRenderer.flipX == false)
        {
            swordAttack.AttackRight();
        }
        else if ((animator.GetCurrentAnimatorStateInfo(0).IsName("player_attack_up") 
            || animator.GetCurrentAnimatorStateInfo(0).IsName("player_pickaxe_up") 
            || animator.GetCurrentAnimatorStateInfo(0).IsName("player_hoe_up")))
        {
            swordAttack.AttackUp();
        }
        else if ((animator.GetCurrentAnimatorStateInfo(0).IsName("player_attack_down") 
            || animator.GetCurrentAnimatorStateInfo(0).IsName("player_pickaxe_down") 
            || animator.GetCurrentAnimatorStateInfo(0).IsName("player_hoe_down")))
        {
            swordAttack.AttackDown();
        }
    }

    //To lock movement when attacking;
    public void LockMovement()
    {
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }
}

