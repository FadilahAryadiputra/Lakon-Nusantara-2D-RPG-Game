using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingProjectileController : MonoBehaviour {

	public Transform target;

	public float lifeTime = 2;
	public float speed = 5f;
	public float rotateSpeed = 200f;
	public float damage = 1f;
	public float thrustPower = 10f;
    public float knockTime = 0.3f;


	public GameObject HitParticlePrefab;

	// PlayerController player;

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
		Destroy(gameObject, lifeTime);
		PlayerController player = gameObject.GetComponent<PlayerController>();
	}
	
	void FixedUpdate () {
		Vector2 direction = (Vector2)target.position - rb.position;

		direction.Normalize();

		float rotateAmount = Vector3.Cross(direction, transform.right).z;

		rb.angularVelocity = -rotateAmount * rotateSpeed;

		rb.velocity = transform.right * speed;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player") 
		{
			PlayerController player = other.GetComponent<PlayerController>();
			CameraController camera = FindObjectOfType<CameraController>();

			if(player) {
				Instantiate(HitParticlePrefab,transform.position, Quaternion.identity);
                camera.ShakeCamera();
                player.currentHealth -= damage;
                // AudioManager.instance.PlaySFX(HitPlayerSFX);
                player.LockMovement();
                player.rb.isKinematic = false;
                Vector2 difference = player.transform.position - transform.position;
                difference = difference.normalized * thrustPower;
                player.rb.AddForce(difference, ForceMode2D.Impulse);
                player.rb.gravityScale = 0f;

				// Start the timer to stop knockback
                player.isKnockedTime = knockTime;
				player.knockTimer = 0f;
				player.isKnocked = true;

				Destroy(gameObject);
            }
		}
		if(other.tag == "Obstacle")
		{
			Instantiate(HitParticlePrefab,transform.position, Quaternion.identity);
			// AudioManager.instance.PlaySFX(HitPlayerSFX);
			Destroy(gameObject);
		}
	}
}