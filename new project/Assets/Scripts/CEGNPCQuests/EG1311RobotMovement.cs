using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EG1311RobotMovement : RobotMovement
{
    public GameObject rock;
    public Vector3 directionToThrow;
    public rockTrajectory eg1311rockTrajectory;

    public override void Start()
    {
        base.Start();
        directionToThrow = Vector3.zero;
        eg1311rockTrajectory = rock.GetComponent<rockTrajectory>();
    }
    public override void Update ()
    {
        base.Update();
        if (spriteRenderer.sprite.name == "Robot_0")
        {
            directionToThrow = new Vector3(0, -0.2f, 0);
        }
        if (spriteRenderer.sprite.name == "Robot_1")
        {
            directionToThrow = new Vector3(0, 0.2f, 0);
        }
        if (gameObject.transform.localScale.x < 0)
        {
            directionToThrow = new Vector3(0, -0.2f, 0).normalized;
        }
        if (gameObject.transform.localScale.x > 0)
        {
            directionToThrow = new Vector3(0, 0.2f, 0).normalized;
        }
        Shoot();
    }
    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            eg1311rockTrajectory.rb.velocity = directionToThrow;
            Instantiate(rock, gameObject.transform.position, Quaternion.identity);
        }
    }
}
