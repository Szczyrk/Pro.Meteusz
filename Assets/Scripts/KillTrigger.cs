using UnityEngine;

public class KillTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.tag.Equals("Player"))
        {
            PlayerController playerController = collisionObject.GetComponent<PlayerController>();
            if (playerController)
                playerController.Kill();
        }
    }
}
