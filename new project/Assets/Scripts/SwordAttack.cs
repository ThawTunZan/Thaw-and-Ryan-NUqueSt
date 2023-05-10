using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public GameObject swordSideAttackObject;
    public GameObject swordUpDownAttackObject;
    //public GameObject rb;
    Collider2D swordCollider;
    Collider2D swordUpDownCollider;

    Vector2 rightAttackOffset;
    Vector2 upAttackOffset;

    public float swordDamage = 1f;

    private void Start()
    {
        swordCollider = swordSideAttackObject.GetComponent<Collider2D>();
        swordUpDownCollider = swordUpDownAttackObject.GetComponent<Collider2D>();   
        rightAttackOffset = swordCollider.transform.localPosition;
        upAttackOffset = swordUpDownCollider.transform.localPosition;
    }

    public void AttackRight()
    {
        //attack right
        //Debug.Log("attacking right");
        swordCollider.enabled = true;
        swordCollider.transform.localPosition = rightAttackOffset;

    }
    public void AttackLeft()
    {
        //attack left
        //Debug.Log("attacking left");
        swordCollider.enabled = true;
        swordCollider.transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
    }
    
    public void AttackUp()
    {
        //attack up
        //Debug.Log("attacking up");
        swordUpDownCollider.enabled = true;
        swordUpDownCollider.transform.localPosition = upAttackOffset;
    }
    public void AttackDown()
    {
        //attack down
        //Debug.Log("attacking down");
        swordUpDownCollider.enabled = true;
        swordUpDownCollider.transform.localPosition = new Vector3(upAttackOffset.x, (float)(upAttackOffset.y - 0.226));
    }

    
}

