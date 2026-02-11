using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;   
    public float offsetY = 2f; 
    void LateUpdate()
    {
        if(player.position.y > transform.position.y)
        {
            transform.position = new Vector3(
                transform.position.x,
                player.position.y + offsetY,
                transform.position.z
            );
        }
    }
}
