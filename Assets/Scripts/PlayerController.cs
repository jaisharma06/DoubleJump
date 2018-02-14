using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField]
	private Transform groundCollisionParticles;
	[SerializeField]
	private LayerMask groundLayerMask;
	[SerializeField]
	private float groundDistance = 0.35f;
	[SerializeField]
	private float jumpVelocity = 5;
	[SerializeField]
	private Rigidbody2D rb;
	[SerializeField]
	private int jumpCount = 0;

	/// <summary>
	/// OnCollisionEnter is called when this collider/rigidbody has begun
	/// touching another rigidbody/collider.
	/// </summary>
	/// <param name="other">The Collision data associated with this collision.</param>
	private void OnCollisionEnter2D(Collision2D other)
	{
		var collider = other.collider;
		if(collider)
		{
			if(collider.CompareTag("Ground"))
			{
				var collisionPosition = new Vector3(transform.position.x, other.contacts[0].point.y, transform.position.z);
				var particles = Instantiate(groundCollisionParticles, collisionPosition, groundCollisionParticles.rotation);
				Destroy(particles.gameObject, 0.6f);
			}
		}
	}

	public void Jump()
	{
		var isGrounded = IsGrounded();

		if(isGrounded || jumpCount == 1)
		{
			rb.velocity = Vector2.up * jumpVelocity;
			jumpCount++;
		}
	}

	private bool IsGrounded()
	{
		if(Physics2D.Raycast(transform.position, Vector2.down, groundDistance, groundLayerMask))
		{
			jumpCount = 0;
			return true;
		}
		return false;
	}
}
