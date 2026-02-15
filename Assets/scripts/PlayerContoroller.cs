using UnityEngine;
using UnityEngine.SceneManagement;

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
    public float deathYLimit = -10f;
    private bool isDead = false;
    private CameraFollow cam;
    public AudioSource DeathSound;
    public AudioSource AttackSound;

    public enum AnimationState
    {
        IDLE,
        JUMP,
        JUMPATTACK,
        DEAD
    }

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        float playerWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        screenLimit = backgroundRenderer.bounds.extents.x - playerWidth;
        cam = Camera.main.GetComponent<CameraFollow>();
    }

    void Update()
    {
        CalculateMoveInput();
        isStart();
        HandleDirection();
        if (Input.GetKeyDown(KeyCode.Space) && !isDead)
        {
            JumpAttack();
        }
        CheckDeath();
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

private void CheckDeath()
{
    float bottomOfScreen = Camera.main.transform.position.y - Camera.main.orthographicSize;

    if (transform.position.y < bottomOfScreen - 1f)
    {
        Die();
    }
}

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log($"Collision Enter with {other.gameObject.name}");
        if (!start && other.gameObject.CompareTag("Ground") && Rigidbody.linearVelocity.y <= 0)
        {
            if (OnRock != null)
                {
                    OnRock.Play();
                }
            Jump();
        }

        // if (other.gameObject.CompareTag("Dragon"))
        // {
        //     Die();
        // }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dragon"))
        {    
            if (AS == AnimationState.JUMPATTACK)
            {
                DragonController dragon =
                    other.GetComponent<DragonController>();

                if (dragon != null)
                {
                    dragon.Die();
                }
                
                Jump();
                
            }
            else
            {
                Die();
            }
        }
    }

    
    public void Jump()
    {
        Rigidbody.linearVelocity = new Vector2(Rigidbody.linearVelocity.x, ConstJump);
        AS = AnimationState.JUMP;
    }
    public void JumpAttack()
    {
        Rigidbody.linearVelocity =
            new Vector2(Rigidbody.linearVelocity.x, ConstJump);

        AS = AnimationState.JUMPATTACK;
        
        if (AttackSound != null)
            AttackSound.Play();
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;
        AS = AnimationState.DEAD;
        if (DeathSound != null)
            DeathSound.Play();
        gameObject.layer = LayerMask.NameToLayer("Dead");
        Rigidbody.linearVelocity = new Vector2(0f, Rigidbody.linearVelocity.y);
        Rigidbody.gravityScale = 1.2f; 
        Debug.Log("Player Died");
        if (cam != null)
        {
            cam.Shake(0.4f, 0.05f);
        }
        Invoke(nameof(LoadFailScene), 1.5f);
    }

    private void LoadFailScene()
    {
        SceneManager.LoadScene("Fail");
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
            case AnimationState.JUMPATTACK:
                Animator.SetInteger("isJumping", 2);
                break;

            case AnimationState.DEAD:
                Animator.SetTrigger("Dead");
                break;
        }
    }

}

