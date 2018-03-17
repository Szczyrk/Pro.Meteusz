using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject projectile, projectileSpawnPoint;
    public float projectileSpeed, coolDown, projectileSize;
    private float timer;
    private Rigidbody2D objectRigidbody2D;

    void Start()
    {
        objectRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (gameObject.tag.Equals("Player") && Input.GetMouseButton(0))
        {
            Shoot(new Vector2(- objectRigidbody2D.position.x + Camera.main.ScreenToWorldPoint(Input.mousePosition).x,- objectRigidbody2D.position.y + Camera.main.ScreenToWorldPoint(Input.mousePosition).y));
  
        }
	}

    public void Shoot(Vector2 input)
    {
      
        if (!input.Equals(Vector2.zero))
        {
            projectileSpawnPoint.transform.rotation = Quaternion.LookRotation(input, Vector2.up);
            Debug.Log("rotacja " + projectileSpawnPoint.transform.rotation);
            timer -= Time.deltaTime;
            if (timer < 0.0f)
            {
                GameObject spawnedProjectile = GameObject.Instantiate(projectile);
                spawnedProjectile.transform.position = projectileSpawnPoint.transform.position;
                spawnedProjectile.transform.localScale = Vector3.one * projectileSize;
                spawnedProjectile.GetComponent<Rigidbody2D>().velocity = (Vector2)(projectileSpawnPoint.transform.forward * projectileSpeed) + objectRigidbody2D.velocity;
                timer = coolDown;
            }
        }
    }
}