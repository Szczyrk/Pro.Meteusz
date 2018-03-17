using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public Image bossHealthBar;
    private Enemy enemy;
    private float touchDamage = 7.5f, sign = 1.0f, startY, endY;
    private bool rise = true;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        startY = transform.position.y;
        endY = transform.position.y + 3.0f;
    }

    void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + Time.deltaTime * sign);
        if (transform.position.y >= endY && rise)
        {
            rise = false;
            sign = -1.0f;
        }
        if(transform.position.y<=startY && !rise)
        {
            rise = true;
            sign = 1.0f;
        }
        if(enemy)
        bossHealthBar.fillAmount = enemy.Health / enemy.maxHealth;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController)
                playerController.Hurt(touchDamage);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
            bossHealthBar.gameObject.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
            bossHealthBar.gameObject.SetActive(false);
    }
}