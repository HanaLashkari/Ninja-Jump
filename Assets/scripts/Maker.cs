using UnityEngine;

public class Maker : MonoBehaviour
{
    public GameObject rockPrefab;
    public GameObject dragonPrefab;
    public PlayerContoroller playerController;
    public SpriteRenderer backgroundRenderer;
    public GameObject StartingRock;
    private Vector3 lastRockPos;
    private float screenWidthLimit;
    private int direction = -1;
    private int rocksSpawnedSinceLastDragon = 0;
    private int nextDragonAt;

    void Start()
    {
        float rockWidth = rockPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        screenWidthLimit = backgroundRenderer.bounds.extents.x - (rockWidth / 2);
        lastRockPos = StartingRock.transform.position;
        SetNextDragonThreshold();
        for (int i = 0; i < 15; i++)
        {
            SpawnRock();
        }
    }

    void Update()
    {
        if (playerController.transform.position.y > lastRockPos.y - 10f)
        {
            SpawnRock();
        }
    }

    void SpawnRock()
    {
        float jumpStep = 1.725f; 
        float spawnY = lastRockPos.y + jumpStep;
        direction *= -1; 
        float horizontalOffset = Random.Range(1f, 3f) * direction;
        float spawnX = lastRockPos.x + horizontalOffset;
        spawnX = Mathf.Clamp(spawnX, -screenWidthLimit, screenWidthLimit);
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);
        Instantiate(rockPrefab, spawnPosition, Quaternion.identity);
        lastRockPos = spawnPosition;

        rocksSpawnedSinceLastDragon++;
        if (rocksSpawnedSinceLastDragon >= nextDragonAt)
        {
            SpawnDragon(spawnY + 0.5f); 
            rocksSpawnedSinceLastDragon = 0;
            SetNextDragonThreshold();
        }
    }

    void SpawnDragon(float yPos)
    {
        Vector3 dragonPos = new Vector3(0, yPos, 0);
        Instantiate(dragonPrefab, dragonPos, Quaternion.identity);
    }

    void SetNextDragonThreshold()
    {
        nextDragonAt = Random.Range(5, 16); 
    }

}