﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

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
    private Collider2D col;
    [SerializeField]
    private TrailRenderer trail;
    [SerializeField]
    private SpriteRenderer sr;
    [SerializeField]
    private Transform destroyParticles;
    [SerializeField]
    private int jumpCount = 0;

    private bool isActive = false;

	private Vector2 initialPlayerPosition;

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()
	{
		EventManager.OnGameStart += OnGameStart;
	}

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		initialPlayerPosition = transform.position;
	}

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        var collider = other.collider;
        if (collider)
        {
            if (collider.CompareTag("Ground") && isActive)
            {
                var collisionPosition = new Vector3(transform.position.x, other.contacts[0].point.y, transform.position.z);
                var particles = Instantiate(groundCollisionParticles, collisionPosition, groundCollisionParticles.rotation);
                Destroy(particles.gameObject, 0.6f);
            }
        }
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        var blade = other.GetComponent<Blade>();
        if (blade && isActive)
        {
			blade.SetActive(false);
            DestroyPlayer();
        }
    }

	/// <summary>
	/// This function is called when the behaviour becomes disabled or inactive.
	/// </summary>
	void OnDisable()
	{
		EventManager.OnGameStart -= OnGameStart;
	}

    public void Jump()
    {
        if (!isActive)
            return;

        var isGrounded = IsGrounded();

        if (isGrounded || jumpCount == 1)
        {
            rb.velocity = Vector2.up * jumpVelocity;
            jumpCount++;
        }
    }

    private bool IsGrounded()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, groundDistance, groundLayerMask))
        {
            jumpCount = 0;
            return true;
        }
        return false;
    }

    private void DestroyPlayer()
    {
		isActive = false;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector2.zero;
        col.enabled = false;
        sr.enabled = false;
        trail.enabled = false;
        var dp = Instantiate(destroyParticles);
        dp.position = transform.position;
        Destroy(dp.gameObject, 0.6f);
		transform.position = initialPlayerPosition;
		EventManager.CallOnGameOver();
    }

	private void OnGameStart()
	{
		ActivatePlayer();
	}

	public void ActivatePlayer()
	{
		isActive = true;
        trail.enabled = true;
		rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = Vector2.zero;
        col.enabled = true;
        sr.enabled = true;
	}
}
