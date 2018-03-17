using UnityEngine;

public class Projectile : MonoBehaviour
{
    public readonly float damage = 0.5f;
    private float lifeTime = 4.0f;

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0.0f)
            Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}