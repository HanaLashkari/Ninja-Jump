using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DragonController : MonoBehaviour
{
    public float speed = 2f;
    public float moveDistance = 3f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private float startX;
    private float leftLimit;
    private float rightLimit;

    private int direction = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        startX = transform.position.x;

        leftLimit = startX - moveDistance;
        rightLimit = startX + moveDistance;

        // شروع حرکت به سمت چپ
        direction = 1;
        spriteRenderer.flipX = true;
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);

        if (direction == -1 && transform.position.x <= leftLimit)
        {
            direction = 1;
            spriteRenderer.flipX = true;
        }
        else if (direction == 1 && transform.position.x >= rightLimit)
        {
            direction = -1;
            spriteRenderer.flipX = false;
        }
    }
}