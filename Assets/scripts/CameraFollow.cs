using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;   
    public float offsetY = 2f;

    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.2f;

    private Vector3 originalPosition;

    void LateUpdate()
    {
        if (player.position.y > transform.position.y)
        {
            transform.position = new Vector3(
                transform.position.x,
                player.position.y + offsetY,
                transform.position.z);
        }

        if (shakeDuration > 0)
        {
            transform.position += (Vector3)Random.insideUnitCircle * shakeMagnitude;
            shakeDuration -= Time.deltaTime;
        }
    }

    public void Shake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }
}
