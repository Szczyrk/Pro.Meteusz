using UnityEngine;

public class Charge : MonoBehaviour
{
    public float chargeTime, chargeForce;
    private float timer;
    private bool canAttack;
    private Rigidbody2D objectRigidbody2D;

    void Start()
    {
        objectRigidbody2D = GetComponent<Rigidbody2D>();
        timer = chargeTime;
        canAttack = true;
    }

    void Update()
    {
        if (!canAttack)
            timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            canAttack = true;
            timer = chargeTime;
        }
	}

    public void Attack(Vector2 direction)
    {
        if (objectRigidbody2D && canAttack)
        {
            objectRigidbody2D.AddForce(direction * chargeForce, ForceMode2D.Impulse);
            canAttack = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            Rigidbody2D otherRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            if(otherRigidbody)
            {
                Vector2 direction = (otherRigidbody.worldCenterOfMass - objectRigidbody2D.worldCenterOfMass).normalized;
                otherRigidbody.AddForce(direction * chargeForce, ForceMode2D.Impulse);
            }
            /*foreach (ContactPoint2D contactPoint2D in collision.contacts)
            {
                if (playerRigidbody)
                    playerRigidbody.;
            }*/
        }
    }
}
