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

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            if (!success)
            {
                //try movement using the player's movement only in the x direction;
                success = TryMove(new Vector2(movementInput.x, 0));

                if (!success)
                {
                    //try movement using the player's movement only in the x direction;
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }

        }

    }
    private bool TryMove(Vector2 direction)
    {
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

 
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
}

