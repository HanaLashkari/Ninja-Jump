using UnityEngine;

public class FootCollision : MonoBehaviour
{
    private PlayerContoroller player;

    private void Awake()
    {
        player = GetComponentInParent<PlayerContoroller>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ground"))
            return;

        if (player.start)
            return;

        if (player.Rigidbody.linearVelocity.y > 0)
            return;

        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                if (player.OnRock != null)
                {
                    player.OnRock.Play();
                    Debug.Log($"Collision Enter with {gameObject.name} Feet");
                }
                break;
            }
        }
    }
}
