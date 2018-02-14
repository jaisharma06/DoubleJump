using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public int dir = -1;
    [SerializeField]
    private float rotationSpeed = 200f;
    [SerializeField]
    private float linearSpeed = 3f;
    [SerializeField]
    private float leftBorder = -9.4f;
    [SerializeField]
    private float rightBorder = 9.4f;

    public bool isActive { get; set; }

    private Collider2D col;
    private SpriteRenderer sr;

    public Vector2 position { get { return transform.position; } set { transform.position = value; } }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void OnEnable()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
            return;

        if (this.position.x > rightBorder || this.position.x < leftBorder)
        {
            SetActive(false);
        }
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
        var position = transform.position;
        position.x += Time.deltaTime * dir * linearSpeed;
        transform.position = position;
    }

    public void SetDirection(int dir)
    {
        Vector2 scale = transform.localScale;
        this.dir = dir;
        scale.x *= dir;
        transform.localScale = scale;
    }

    public void SetActive(bool flag)
    {
        isActive = flag;
        col.enabled = flag;
        sr.enabled = flag;
    }
}
