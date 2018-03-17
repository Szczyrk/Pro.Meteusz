using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed, jumpForce, maxHealth;
    public Image healthBar;
    private GameObject[] checkpoints;
    private float health;
    private bool isGrounded;
    private Rigidbody2D playerRigidbody;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        health = 100.0f;
    }

    void Update()
    {
        if (playerRigidbody)
        {
            Move();
            if (Input.GetButtonDown("Jump"))
                Jump();
            if (health <= 0.0f)
                Respawn();
            healthBar.fillAmount = health / maxHealth;
        }
    }
    

    void Move()
    {
        float moveVelocity = -Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float sign = Mathf.Sign(Input.GetAxis("Horizontal"));
        playerRigidbody.velocity = new Vector2(moveVelocity, playerRigidbody.velocity.y);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, sign);
        if (spriteRenderer)
            spriteRenderer.flipX = (sign < 0.0f);                                                                                                                           
    }

    void Jump()
    {
        if (isGrounded)
            playerRigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    public void Heal()
    {
        health = maxHealth;
    }

    public void Hurt(float damage)
    {
        health -= damage;
    }

    public void Kill()
    {
        health = 0.0f;
    }

    void Respawn()
    {
        float distance = float.MaxValue;
        Vector2? respawnPosition = null;
        if (checkpoints.Length > 0.0f)
        {
            foreach (GameObject checkpoint in checkpoints)
            {
                Checkpoint checkpointComponent = checkpoint.GetComponent<Checkpoint>();
                if (checkpointComponent && checkpointComponent.isActive)
                {
                    float newDistance = Vector2.Distance(transform.position, checkpoint.transform.position);
                    if (newDistance < distance)
                    {
                        distance = newDistance;
                        respawnPosition = checkpoint.transform.position;
                    }
                }
            }
            if (respawnPosition != null)
            {
                Heal();
                transform.position = respawnPosition.Value;
            }
        }
        if (respawnPosition == null)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Projectile"))
            health -= collision.gameObject.GetComponent<Projectile>().damage;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
        if (collision.gameObject.tag.Equals("Enemy"))
            health -= collision.gameObject.GetComponent<Enemy>().damage;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}