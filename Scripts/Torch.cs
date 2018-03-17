using UnityEngine;

public class Torch : MonoBehaviour
{
    private ParticleSystem flames;

	void Start()
    {
        flames = GetComponent<ParticleSystem>();
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (flames && flames.isStopped && collision.gameObject.tag.Equals("Player"))
            flames.Play();
    }
}
