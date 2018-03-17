using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed, maxHealth;
    public readonly float damage = 5.0f;
    public Texture healthBar;
    private bool isUpgraded;
    private float health;
    private Vector3 healthBarPosition;
    private Rigidbody2D enemyRigidbody;
    private SpriteRenderer spriteRenderer;
    private Gun[] guns;
    private Charge charge;

    public float Health
    {
        get { return health; }
    }

    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        guns = GetComponents<Gun>();
        charge = GetComponent<Charge>();
        health = maxHealth;
        isUpgraded = false;
    }

    void Update()
    {
        healthBarPosition = Camera.main.WorldToScreenPoint(transform.position);
    }

    void OnGUI()
    {
        if (gameObject.tag.Equals("Enemy"))
        {
            float rectX = healthBarPosition.x - healthBar.width * 0.5f;
            float rectY = Screen.height - healthBarPosition.y - healthBar.height * 2.5f;
            Rect rectangle = new Rect(rectX, rectY, healthBar.width, healthBar.height);
            rectangle.width *= Health / maxHealth;
            GUI.color = Color.magenta;
            GUI.DrawTexture(rectangle, healthBar);
            GUI.color = Color.white;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Projectile"))
        {
            health -= collision.gameObject.GetComponent<Projectile>().damage;
            if (Health <= 50.0f && !isUpgraded)
            {
                if(gameObject.tag.Equals("Enemy"))
                {
                    transform.localScale *= 3.0f;
                    isUpgraded = true;
                }
            }
            if (Health <= 0.0f)
                Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        GameObject otherObject = collision.gameObject;
        if (otherObject.tag.Equals("Player") && enemyRigidbody)
        {
            Vector2 directionToPlayer = otherObject.transform.position - transform.position;
            if (gameObject.tag.Equals("Enemy"))
                enemyRigidbody.velocity = new Vector2(directionToPlayer.x * moveSpeed * Time.deltaTime, enemyRigidbody.velocity.y);
            float sign = Mathf.Sign(directionToPlayer.x);
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, sign);
            if (spriteRenderer)
                spriteRenderer.flipX = (sign > 0.0f);
            foreach (Gun gun in guns)
                gun.Shoot(directionToPlayer);
            if (charge)
                charge.Attack(new Vector2(directionToPlayer.x, 0));
        }
    }
}
