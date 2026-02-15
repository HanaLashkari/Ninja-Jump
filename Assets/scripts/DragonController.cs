using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DragonController : MonoBehaviour
{
    public float speed = 1f;
    private SpriteRenderer backgroundRenderer;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float screenLimit;
    private int direction = 1;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameObject bgObject = GameObject.Find("Background");
        if (bgObject != null)
        {
            backgroundRenderer = bgObject.GetComponent<SpriteRenderer>();
            float dragonWidth = spriteRenderer.bounds.extents.x;
            screenLimit = backgroundRenderer.bounds.extents.x - dragonWidth;
        }
        rb.gravityScale = 0; 
    }

    void FixedUpdate()
    {   
        if (isDead) return;
        Move();
    }

    void Move()
    {
        if (backgroundRenderer == null) return;
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
        float bgCenterX = backgroundRenderer.bounds.center.x;
        if (direction == -1 && transform.position.x <= bgCenterX - screenLimit)
        {
            direction = 1;
            spriteRenderer.flipX = true;
        }
        else if (direction == 1 && transform.position.x >= bgCenterX + screenLimit)
        {
            direction = -1;
            spriteRenderer.flipX = false;
        }
    }
    
public void Die()
{
    if (isDead) return;

    isDead = true;

    // توقف کامل حرکت افقی
    rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);

    // فعال شدن گراویتی
    rb.gravityScale = 1.5f;

    // از همه چیز رد شود
    gameObject.layer = LayerMask.NameToLayer("Dead");

    Collider2D col = GetComponent<Collider2D>();
    if (col != null)
        col.enabled = false;

    Destroy(gameObject, 3f);
}


}