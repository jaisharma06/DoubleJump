using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform groundCollisionParticles;
    [SerializeField]
    private LayerMask groundLayerMask;
    [SerializeField]
    private float jumpVelocity = 5;

    [SerializeField]
    private Transform destroyParticles;

    [SerializeField]
    private AudioClip sfxJump;
    [SerializeField]
    private AudioClip sfxBlast;
    [SerializeField]
    private AudioClip sfxSplat;

    private int jumpCount = 0;
    private bool isActive = false;
    private bool alreadyGrounded = false;

    private Vector2 initialPlayerPosition;
    private AudioManager am;

    private Rigidbody2D rb;
    private Collider2D col;
    private TrailRenderer trail;
    private SpriteRenderer sr;

    private float height { get; set; }

    void OnEnable()
    {
        EventManager.OnGameStart += OnGameStart;
        EventManager.OnInvertColor += InvertColor;
        EventManager.OnNoEnemyLeft += EnablePlayerSkin;
    }

    void Start()
    {
        InitializePlayer();
    }

    private void InitializePlayer()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        trail = GetComponent<TrailRenderer>();
        initialPlayerPosition = transform.position;
        height = col.bounds.size.y;
        am = AudioManager.instance;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var _collider = other.collider;
        if (_collider)
        {
            if (_collider.CompareTag("Ground") && isActive)
            {
                if (!alreadyGrounded)
                {
                    alreadyGrounded = true;
                    return;
                }
                var collisionPosition = new Vector3(transform.position.x, other.contacts[0].point.y, transform.position.z);
                var particles = Instantiate(groundCollisionParticles, collisionPosition, groundCollisionParticles.rotation);
                Destroy(particles.gameObject, 0.6f);
                am.PlayOneShot(sfxSplat);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var blade = other.GetComponent<Blade>();
        if (blade && isActive)
        {
            blade.SetActive(false);
            DestroyPlayer();
        }
    }

    void OnDisable()
    {
        EventManager.OnGameStart -= OnGameStart;
        EventManager.OnInvertColor -= InvertColor;
        EventManager.OnNoEnemyLeft -= EnablePlayerSkin;
    }

    public void Jump()
    {
        if (!isActive)
            return;

        var isGrounded = IsGrounded();

        if (isGrounded || jumpCount == 1)
        {
            rb.velocity = Vector2.up * jumpVelocity;
            am.PlayOneShot(sfxJump);
            jumpCount++;
        }
    }

    private bool IsGrounded()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, (height / 2.0f + 0.15f), groundLayerMask))
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
        alreadyGrounded = false;
        var dp = Instantiate(destroyParticles);
        dp.position = transform.position;
        Destroy(dp.gameObject, 0.6f);
        transform.position = initialPlayerPosition;
        am.PlayOneShot(sfxBlast);
        EventManager.CallOnGameOver();
    }

    private void EnablePlayerSkin()
    {
        sr.enabled = true;
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

    private void InvertColor()
    {
        sr.material.color = (sr.material.color == Color.black) ? Color.white : Color.black;
        var color = (trail.material.GetColor("_TintColor") == Color.black) ? Color.white : Color.black;
        trail.material.SetColor("_TintColor", color);
    }
}
