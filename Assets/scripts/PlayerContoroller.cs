using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerContoroller : MonoBehaviour
{
    public float Speed = 5f;
    public Rigidbody2D Rigidbody;
    public Vector2 MoveInput;
    public float ConstJump = 5f;
    public bool start = true;
    public SpriteRenderer SpriteRenderer;
    public AudioSource OnRock;
    public Animator  Animator;
    public AnimationState AS = AnimationState.IDLE; 
    public SpriteRenderer backgroundRenderer;
    private float screenLimit;

    public enum AnimationState
    {
        IDLE,
        JUMP,
        DEAD
    }

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        float playerWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        screenLimit = backgroundRenderer.bounds.extents.x - playerWidth;
    }

    void Update()
    {
        CalculateMoveInput();
        isStart();
        HandleDirection();
        SetAnimatorState();
    }

    void FixedUpdate()
    {
        Rigidbody.linearVelocity = new Vector2(MoveInput.x * Speed, Rigidbody.linearVelocity.y);
        ClampPosition();
    }

    private void ClampPosition()
    {
        Vector3 pos = transform.position;
            float bgCenterX = backgroundRenderer.bounds.center.x;
            pos.x = Mathf.Clamp(pos.x,bgCenterX - screenLimit,bgCenterX + screenLimit);
            transform.position = pos;
    }

    private void isStart()
    {
        if(start)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                start = false;
                Jump();
            }
        }
    }

    private void CalculateMoveInput()
    {
        var x = Input.GetAxisRaw("Horizontal");
        MoveInput = new Vector2(x, 0f);
    }

    private void HandleDirection()
    {
        if(MoveInput.x > 0) 
        {
            SpriteRenderer.flipX = false;
        }
        else if(MoveInput.x < 0) 
        {
            SpriteRenderer.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log($"Collision Enter with {other.gameObject.name}");
        if (!start && other.gameObject.CompareTag("Ground") && Rigidbody.linearVelocity.y <= 0)
        {
            Jump();
        }
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.CompareTag("Dragon"))
    //     {
    //         // Die();
    //     }
    // }
    
    public void Jump()
    {
        Rigidbody.linearVelocity = new Vector2(Rigidbody.linearVelocity.x, ConstJump);
        AS = AnimationState.JUMP;
    }

    private void SetAnimatorState()
    {
        if (Animator == null) return;

        switch (AS)
        {
            case AnimationState.IDLE:
                Animator.SetInteger("isJumping", 0);
                break;
            case AnimationState.JUMP:
                Animator.SetInteger("isJumping", 1);
                break;
            case AnimationState.DEAD:
                Animator.SetTrigger("Dead");
                break;
        }
    }

}

