using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float moveSpeed;
    public Vector3 targetPosition;
    public GameObject player;
    private bool canMove;

    void Update()
    {
        if (canMove)
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);
        if (Vector2.Distance(transform.position, targetPosition) <= 0.001f)
        {
            if (player.transform.IsChildOf(transform))
            {
                player.transform.parent = null;
                player.layer = LayerMask.NameToLayer("Player");
                player.GetComponent<PlayerController>().enabled = true;
                GetComponent<Collider2D>().enabled = false;
                gameObject.layer = LayerMask.NameToLayer("Default");
            }
        }
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.Equals(player) && !canMove)
        {
            player.GetComponent<PlayerController>().enabled = false;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.transform.parent = transform;
            player.transform.position = transform.position;
            player.layer = LayerMask.NameToLayer("Elevator");
            canMove = true;
        }
    }
}
