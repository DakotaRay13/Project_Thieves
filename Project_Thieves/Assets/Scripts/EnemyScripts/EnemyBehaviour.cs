using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public enum EnemyStates
    {
        PATROL,
        PURSUIT
    };
    public EnemyStates states;

    public Rigidbody2D rb;
    public Transform turnCheck;

    public LayerMask groundMask;
    public LayerMask playerMask;

    public Vector3 velocity;
    public float speed;
    public float accelTime = 0.1f;
    float velocityXsmoothing;
    public float gravity;
    private float direction = 1f;

    public Controller2D controller;

    private void Start()
    {
        states = EnemyStates.PATROL;
    }

    void FixedUpdate()
    {
        CheckForPlayer();

        switch (states)
        {
            case (EnemyStates.PATROL):
            {
                CheckForWall();
                CheckForGround();
                MoveCharacter(speed);
                break;
            }

            case (EnemyStates.PURSUIT):
            {
                FindPlayer(FindObjectOfType<Player>().transform.position);

                if(Mathf.Abs(FindObjectOfType<Player>().transform.position.x - transform.position.x) >= 0.3f)
                    MoveCharacter(speed * 1.5f);
                else
                {
                    rb.velocity = Vector2.zero;
                }
                break;
            }
        }
    }

    public void MoveCharacter(float x)
    {
        //rb.velocity = x * Vector2.right * direction;
        velocity.x = Mathf.SmoothDamp(velocity.x, x * direction, ref velocityXsmoothing, accelTime);
        velocity.y = gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void TurnEnemy()
    {
        direction *= -1f;
        transform.localScale = new Vector3(direction, transform.localScale.y, transform.localScale.z);
    }

    public void CheckForWall()
    {
        if(controller.collisions.below || controller.collisions.grounded)
        {
            RaycastHit2D hit = Physics2D.Raycast(turnCheck.position, Vector2.right, 0.2f, groundMask);
            if (hit)
            {
                TurnEnemy();
                Debug.Log("Hit a wall.");
            }
        }
    }

    public void CheckForGround()
    {
        if (controller.collisions.below || controller.collisions.grounded)
        {
            RaycastHit2D hit = Physics2D.Raycast(turnCheck.position, Vector2.down, 0.2f, groundMask);
            if (!hit)
            {
                TurnEnemy();
                Debug.Log("Hit a ledge.");
            }
        }
    }

    public void CheckForPlayer()
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * direction, 3f, playerMask);
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 6f, Vector2.right, 0f, playerMask);
        if(hit)
        {
            states = EnemyStates.PURSUIT;
        }

        else
        {
            states = EnemyStates.PATROL;
        }
    }

    public void FindPlayer(Vector3 playerPos)
    {
        if(playerPos.x < transform.position.x)
        {
            direction = -1f;
        }
        else if(playerPos.x > transform.position.x)
        {
            direction = 1f;
        }
        transform.localScale = new Vector3(direction, transform.localScale.y, transform.localScale.z);
    }
}
