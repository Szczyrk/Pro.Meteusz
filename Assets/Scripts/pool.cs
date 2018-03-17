using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pool : MonoBehaviour {
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.tag.Equals("Player"))
        {
            PlayerController playerController = collisionObject.GetComponent<PlayerController>();
            while (playerController)
                playerController.Hurt(1);
        }
    }
}
