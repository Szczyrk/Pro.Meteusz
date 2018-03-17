using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed, time;
    private float timer;
    private Vector2 direction;

    void Start()
    {
        direction = Vector2.right;
        timer = time * 0.5f;
    }

    void Update()
    {
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            direction.x *= -1.0f;
            timer = time;
        }
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
            collision.transform.parent = transform;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
            collision.gameObject.transform.parent = null;
    }
}
