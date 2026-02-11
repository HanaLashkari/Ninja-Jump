using System;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class cotoroller : MonoBehaviour
{
    
    public float Speed;
    public Rigidbody2D Rigidbody;
    public Vector2 MoveInput;
    public int JumpVelocity;
    // public bool isGrounded = false;
    public Transform GroundCheck;
    public float GroundCheckRadius;
    public LayerMask GroundLayers;
    public int MaxJumpCount = 2;
    private int _jumpCount = 0;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroundCheck.position, GroundCheckRadius);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, GroundLayers);
    }

    public void Start()
    {
        JumpVelocity = 300;
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        CalculateMoveInput();
        HandleJump();
    }

    private void CalculateMoveInput()
    {
        var x = Input.GetAxisRaw("Horizontal");
        MoveInput = new Vector2(x, 0f);
    }

    private void HandleJump()
    {
        if (IsGrounded())
        {
            _jumpCount = MaxJumpCount;
        }
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        if(_jumpCount <= 0)  return;
        _jumpCount--;
        Rigidbody.AddForce(new Vector2(0f, JumpVelocity));
        
    }

    public void FixedUpdate()
    {
        Rigidbody.linearVelocity = new Vector2(MoveInput.x, Rigidbody.linearVelocity.y) * Speed;
    }

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     Debug.Log($"Collision Enter with {other.gameObject.name}");
    //     if (other.gameObject.CompareTag("Ground"))
    //     {
    //         isGrounded = true;
    //     }
    // }
    //
    // private void OnCollisionStay2D(Collision2D other)
    // {
    //     Debug.Log($"Collision Stay with {other.gameObject.name}");
    // }
    //
    // private void OnCollisionExit2D(Collision2D other)
    // {
    //     Debug.Log($"Collision Exit with {other.gameObject.name}");
    //     if (other.gameObject.CompareTag("Ground"))
    //     {
    //         isGrounded = false;
    //     }
    // }
}
