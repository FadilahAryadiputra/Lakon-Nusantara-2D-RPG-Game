using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float lifeTime = 10;
    public float projectileForce = 5f;
    public float velocitySpeed = 5f;
    public float turnSpeed = 5f;
    public float rotationModifier = 5f;

    public bool homing = false;

    private Transform player;
    public Transform projectilePoint;
    private Vector2 target;
    Vector2 moveDirection;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
        Destroy(gameObject, lifeTime);

        // -- Projectile move direction toward the player last position using velocity --
        moveDirection = (player.transform.position - transform.position).normalized * velocitySpeed;
        rb.velocity = new Vector2 (moveDirection.x, moveDirection.y);

        // -- Projectile move direction toward the player last position using force --
        // rb.AddRelativeForce(transform.right * projectileForce);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && homing == true)
        {
            // transform.position = Vector2.MoveTowards(transform.position, player.position, velocitySpeed * Time.deltaTime);  // Projectile follow the player
            // transform.position = Vector2.MoveTowards(transform.position, target, velocitySpeed * Time.deltaTime);       // Projectile target the player's last position

            // -- Rotate projectile toward player on update --
            Vector3 vectorToTarget = player.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, turnSpeed * Time.deltaTime);

            moveDirection = (player.transform.position - transform.position).normalized * velocitySpeed;
            rb.velocity = new Vector2 (moveDirection.x, moveDirection.y);
        }

        if(transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }
    }

    void Shoot()
    {
        rb.AddForce(projectilePoint.up * projectileForce, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
