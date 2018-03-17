using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce = 5f;
    public float moveSpeed = 2f;
    public float size = 32;
    public float maxSizeY = 64;


    Rigidbody2D body;
    bool isGrounded;
    bool hasKey;
    public GameObject land1;
    public GameObject land2;
    public Animator animator;


    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
  
    }

    void Update()
    {
  
        UpdateMovement();
        CheckLose();
    }

    void UpdateMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            MoveHorizontal(-1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            MoveHorizontal(1);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Up();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Down();
        }
    }

    private void Down()
    {
        if (land1.transform.position.z < land2.transform.position.z)
        {
            animator.SetTrigger("down");
        }
    }

    private void Up()
    {
       if(land1.transform.position.z<land2.transform.position.z)
        {
            animator.SetTrigger("up");
        }
    }

    void MoveHorizontal(float input)
    {
        transform.Translate(new Vector2(input * moveSpeed * Time.deltaTime, 0));
    }

    void Jump()
    {
        if (isGrounded)
        {
            body.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    void CheckLose()
    {
        if (transform.position.y < -10f)
        {
            Defeat();
        }

    }

    void Victory()
    {
        SimpleStateMachine.instance.SetState<GameOver>();
        GameOver gameOver = SimpleStateMachine.instance.currentState as GameOver;
        gameOver.SetVictory();

        GameObject.Destroy(gameObject);
    }

    void Defeat()
    {
        SimpleStateMachine.instance.SetState<GameOver>();
        GameOver gameOver = SimpleStateMachine.instance.currentState as GameOver;
        gameOver.SetDefeat();

        GameObject.Destroy(gameObject);
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (body.velocity.y == 0)
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        isGrounded = false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Finish") && hasKey)
        {
            Victory();
        }
        else if (collider.gameObject.tag.Equals("Key"))
        {
            hasKey = true;
            GameObject.Destroy(collider.gameObject);
        }
    }
}
